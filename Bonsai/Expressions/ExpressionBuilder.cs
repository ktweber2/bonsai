﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Reflection;
using System.Reactive;

namespace Bonsai.Expressions
{
    [XmlInclude(typeof(WorkflowExpressionBuilder))]
    [XmlInclude(typeof(WorkflowInputBuilder))]
    [XmlInclude(typeof(WorkflowOutputBuilder))]
    [XmlInclude(typeof(SourceBuilder))]
    [XmlInclude(typeof(SelectBuilder))]
    [XmlInclude(typeof(WhereBuilder))]
    [XmlInclude(typeof(CombinatorBuilder))]
    [XmlInclude(typeof(NullSinkBuilder))]
    [XmlInclude(typeof(SelectManyBuilder))]
    [XmlInclude(typeof(WindowWorkflowBuilder))]
    [XmlType("Expression", Namespace = Constants.XmlNamespace)]
    [TypeConverter("Bonsai.Design.ExpressionBuilderTypeConverter, Bonsai.Design")]
    public abstract class ExpressionBuilder
    {
        readonly SortedList<string, Expression> arguments = new SortedList<string, Expression>();

        [XmlIgnore]
        [Browsable(false)]
        public abstract Range<int> ArgumentRange { get; }

        protected internal IDictionary<string, Expression> Arguments
        {
            get { return arguments; }
        }

        public abstract Expression Build();

        public static object GetWorkflowElement(ExpressionBuilder builder)
        {
            var sourceBuilder = builder as SourceBuilder;
            if (sourceBuilder != null) return sourceBuilder.Generator;

            var selectBuilder = builder as SelectBuilder;
            if (selectBuilder != null) return selectBuilder.Selector;

            var whereBuilder = builder as WhereBuilder;
            if (whereBuilder != null) return whereBuilder.Predicate;

            var combinatorBuilder = builder as CombinatorBuilder;
            if (combinatorBuilder != null) return combinatorBuilder.Combinator;

            return builder;
        }

        public static ExpressionBuilder FromWorkflowElement(object element, ElementCategory elementCategory)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var elementType = element.GetType();
            if (elementType.IsDefined(typeof(CombinatorAttribute), true))
            {
                return new CombinatorBuilder { Combinator = element };
            }

            if (elementCategory == ElementCategory.Source) return new SourceBuilder { Generator = element };
            if (elementCategory == ElementCategory.Condition) return new WhereBuilder { Predicate = element };
            if (elementCategory == ElementCategory.Transform) return new SelectBuilder { Selector = element };
            throw new InvalidOperationException("Invalid loadable element type.");
        }

        protected static Type GetObservableType(object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.GetType()
                         .FindInterfaces((t, m) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IObservable<>), null)
                         .First()
                         .GetGenericArguments()[0];
        }

        #region Type Inference

        internal static Type[] GetMethodBindings(MethodInfo methodInfo, params Type[] arguments)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            var methodParameters = methodInfo.GetParameters();
            var methodGenericArguments = methodInfo.GetGenericArguments();

            // The binding candidates are the distinct results from matching parameters with input
            // Matches for the same generic parameter position should be identical
            var bindingCandidates = (from bindings in methodParameters.Zip(arguments, (methodParameter, parameter) => GetParameterBindings(methodParameter.ParameterType, parameter))
                                     from binding in bindings
                                     group binding by binding.Item2 into matches
                                     orderby matches.Key ascending
                                     select matches.Distinct().Single().Item1)
                                     .ToArray();

            return methodGenericArguments.Zip(bindingCandidates, (argument, match) => match).Concat(methodGenericArguments.Skip(bindingCandidates.Length)).ToArray();
        }

        internal static IEnumerable<Tuple<Type, int>> GetParameterBindings(Type parameterType, Type argumentType)
        {
            // If parameter is a generic parameter, just bind it to the input type
            if (parameterType.IsGenericParameter)
            {
                return Enumerable.Repeat(Tuple.Create(argumentType, parameterType.GenericParameterPosition), 1);
            }
            // If parameter contains generic parameters, we may have possible bindings
            else if (parameterType.ContainsGenericParameters)
            {
                // Check if we have a straight type match
                var bindings = MatchTypeBindings(parameterType, argumentType).ToArray();
                if (bindings.Length > 0) return bindings;

                // Direct match didn't produce any bindings, so we need to check inheritance chain
                Type currentType = argumentType;
                while (currentType != typeof(object))
                {
                    currentType = currentType.BaseType;
                    bindings = MatchTypeBindings(parameterType, currentType).ToArray();
                    if (bindings.Length > 0) return bindings;
                }

                // Inheritance chain match didn't produce any bindings, so we need to check interface set
                var interfaces = argumentType.GetInterfaces();
                foreach (var interfaceType in interfaces)
                {
                    bindings = MatchTypeBindings(parameterType, interfaceType).ToArray();
                    if (bindings.Length > 0) return bindings;
                }
            }

            // If parameter does not contain generic parameters, there's nothing to bind to (check for error?)
            return Enumerable.Empty<Tuple<Type, int>>();
        }

        internal static IEnumerable<Tuple<Type, int>> MatchTypeBindings(Type parameterType, Type argumentType)
        {
            // If both types have element types, try to recurse into them
            if (parameterType.HasElementType && argumentType.HasElementType)
            {
                if (parameterType.IsArray && !argumentType.IsArray ||
                    parameterType.IsPointer && !argumentType.IsPointer ||
                    parameterType.IsByRef && !argumentType.IsByRef)
                {
                    return Enumerable.Empty<Tuple<Type, int>>();
                }

                var parameterElementType = parameterType.GetElementType();
                var argumentElementType = argumentType.GetElementType();
                return MatchTypeBindings(parameterElementType, argumentElementType);
            }

            // Match bindings can only be obtained if both types are generic types
            if (parameterType.IsGenericType && argumentType.IsGenericType)
            {
                var parameterTypeDefinition = parameterType.GetGenericTypeDefinition();
                var argumentTypeDefinition = parameterType.GetGenericTypeDefinition();
                // Match bindings can only be obtained if both types share the same type definition
                if (parameterTypeDefinition == argumentTypeDefinition)
                {
                    var parameterGenericArguments = parameterType.GetGenericArguments();
                    var argumentGenericArguments = argumentType.GetGenericArguments();
                    return parameterGenericArguments
                        .Zip(argumentGenericArguments, (parameter, argument) => GetParameterBindings(parameter, argument))
                        .SelectMany(xs => xs);
                }
            }

            return Enumerable.Empty<Tuple<Type, int>>();
        }

        internal static bool MatchParamArrayTypeReferences(Type parameter, Type argument)
        {
            if (parameter.HasElementType && argument.HasElementType)
            {
                return MatchParamArrayTypeReferences(parameter.GetElementType(), argument.GetElementType());
            }

            return parameter.HasElementType == argument.HasElementType;
        }

        internal static bool ParamExpansionRequired(ParameterInfo[] parameters, Type[] arguments)
        {
            var offset = parameters.Length - 1;
            var paramArray = parameters.Length > 0 &&
                parameters[offset].ParameterType.IsArray &&
                Attribute.IsDefined(parameters[offset], typeof(ParamArrayAttribute));

            return paramArray &&
                (parameters.Length != arguments.Length ||
                 !MatchParamArrayTypeReferences(parameters[offset].ParameterType, arguments[arguments.Length - 1]));
        }

        internal static MethodCallExpression BuildCall(Expression instance, MethodInfo method, params Expression[] arguments)
        {
            var argumentTypes = Array.ConvertAll(arguments, xs => xs.Type);
            if (method.IsGenericMethodDefinition)
            {
                var methodCallArgumentTypes = argumentTypes;
                var methodParameters = method.GetParameters();
                if (ParamExpansionRequired(methodParameters, methodCallArgumentTypes))
                {
                    var arrayType = methodCallArgumentTypes[methodCallArgumentTypes.Length - 1].MakeArrayType();
                    Array.Resize(ref methodCallArgumentTypes, methodParameters.Length);
                    methodCallArgumentTypes[methodCallArgumentTypes.Length - 1] = arrayType;
                }

                var typeArguments = GetMethodBindings(method, methodCallArgumentTypes);
                method = method.MakeGenericMethod(typeArguments);
            }

            var parameters = method.GetParameters();
            if (ParamExpansionRequired(parameters, argumentTypes))
            {
                var offset = parameters.Length - 1;
                var arrayType = parameters[offset].ParameterType.GetElementType();
                var initializers = new Expression[arguments.Length - offset];
                for (int k = 0; k < initializers.Length; k++)
                {
                    if (arguments[k + offset].Type != arrayType)
                    {
                        throw new InvalidOperationException(string.Format("The type arguments for method '{0}' cannot be inferred from the usage.", method));
                    }
                    initializers[k] = arguments[k + offset];
                }
                var paramArray = Expression.NewArrayInit(arrayType, initializers);
                Array.Resize(ref arguments, parameters.Length);
                arguments[arguments.Length - 1] = paramArray;
            }

            int i = 0;
            arguments = Array.ConvertAll(arguments, argument =>
            {
                var parameterType = parameters[i++].ParameterType;
                if (argument.Type != parameterType)
                {
                    return Expression.Convert(argument, parameterType);
                }
                return argument;
            });

            return Expression.Call(instance, method, arguments);
        }

        #endregion

        #region Overload Resolution

        static readonly Dictionary<Type, Type[]> ImplicitNumericConversions = new Dictionary<Type, Type[]>
        {
            { typeof(sbyte), new[] { typeof(short), typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(byte), new[] { typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(short), new[] { typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(ushort), new[] { typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(int), new[] { typeof(long), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(uint), new[] { typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(long), new[] { typeof(float), typeof(double), typeof(decimal) } },
            { typeof(char), new[] { typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal) } },
            { typeof(float), new[] { typeof(double) } },
            { typeof(ulong), new[] { typeof(float), typeof(double), typeof(decimal) } }
        };

        static bool HasImplicitConversion(Type from, Type to)
        {
            if (to.IsAssignableFrom(from)) return true;

            Type[] conversions;
            if (ImplicitNumericConversions.TryGetValue(from, out conversions))
            {
                return Array.Exists(conversions, type => type == to);
            }

            return from.GetMethods(BindingFlags.Public | BindingFlags.Static)
                       .Any(m => m.ReturnType == to && m.Name == "op_Implicit");
        }

        static int CompareConversion(Type t1, Type t2, Type s)
        {
            if (t1 == t2) return 0;
            if (s == t1) return -1;
            if (s == t2) return 1;

            var implicitT1T2 = HasImplicitConversion(t1, t2);
            var implicitT2T1 = HasImplicitConversion(t2, t1);
            if (implicitT1T2 && !implicitT2T1) return -1;
            if (implicitT2T1 && !implicitT1T2) return 1;

            var t1Code = Type.GetTypeCode(t1);
            var t2Code = Type.GetTypeCode(t2);
            if (t1Code == TypeCode.SByte &&
                (t2Code == TypeCode.Byte || t2Code == TypeCode.UInt16 ||
                 t2Code == TypeCode.UInt32 || t2Code == TypeCode.UInt64)) return -1;

            if (t2Code == TypeCode.SByte &&
                (t1Code == TypeCode.Byte || t1Code == TypeCode.UInt16 ||
                 t1Code == TypeCode.UInt32 || t1Code == TypeCode.UInt64)) return 1;

            if (t1Code == TypeCode.Int16 &&
                (t2Code == TypeCode.UInt16 || t2Code == TypeCode.UInt32 || t2Code == TypeCode.UInt64)) return -1;
            if (t2Code == TypeCode.Int16 &&
                (t1Code == TypeCode.UInt16 || t1Code == TypeCode.UInt32 || t1Code == TypeCode.UInt64)) return 1;

            if (t1Code == TypeCode.Int32 && (t2Code == TypeCode.UInt32 || t2Code == TypeCode.UInt64)) return -1;
            if (t2Code == TypeCode.Int32 && (t1Code == TypeCode.UInt32 || t1Code == TypeCode.UInt64)) return 1;
            if (t1Code == TypeCode.Int64 && t2Code == TypeCode.UInt64) return -1;
            if (t2Code == TypeCode.Int64 && t1Code == TypeCode.UInt64) return 1;
            return 0;
        }

        static int CompareFunctionMember(Type[] parametersA, Type[] parametersB, Type[] arguments)
        {
            bool? betterA = null;
            bool? betterB = null;
            for (int i = 0; i < arguments.Length; i++)
            {
                var comparison = CompareConversion(parametersA[i], parametersB[i], arguments[i]);
                if (comparison < 0)
                {
                    if (!betterA.HasValue) betterA = true;
                    betterB = false;
                }
                else if (comparison > 0)
                {
                    if (!betterB.HasValue) betterB = true;
                    betterA = false;
                }
            }

            if (betterA.GetValueOrDefault()) return -1;
            if (betterB.GetValueOrDefault()) return 1;
            return 0;
        }

        internal static Type[] ExpandCallParameterTypes(ParameterInfo[] parameters, Type[] arguments, bool expansion)
        {
            var expandedParameters = new Type[arguments.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                expandedParameters[i] = parameters[i].ParameterType;
            }

            if (expansion)
            {
                for (int i = parameters.Length-1; i < expandedParameters.Length; i++)
                {
                    expandedParameters[i] = parameters[parameters.Length - 1].ParameterType.GetElementType();
                }
            }

            return expandedParameters;
        }

        internal static Expression BuildCall(Expression instance, IEnumerable<MethodInfo> methods, params Expression[] arguments)
        {
            var argumentTypes = Array.ConvertAll(arguments, argument => argument.Type);
            var candidates = methods
                .Where(method =>
                {
                    var parameters = method.GetParameters();
                    return parameters.Length == arguments.Length ||
                        parameters.Length > 0 && arguments.Length >= (parameters.Length - 1) &&
                        Attribute.IsDefined(parameters[parameters.Length - 1], typeof(ParamArrayAttribute));
                })
                .Select(method =>
                {
                    MethodCallExpression call;
                    try { call = BuildCall(instance, method, arguments); }
                    catch (InvalidOperationException) { return null; }
                    return new
                    {
                        call,
                        generic = call.Method != method,
                        expansion = ParamExpansionRequired(call.Method.GetParameters(), argumentTypes)
                    };
                })
                .Where(candidate => candidate != null)
                .ToArray();

            if (candidates.Length == 0)
            {
                throw new InvalidOperationException("No method overload found for the given arguments.");
            }

            if (candidates.Length == 1) return candidates[0].call;

            int best = -1;
            var candidateParameters = Array.ConvertAll(
                candidates,
                candidate => ExpandCallParameterTypes(candidate.call.Method.GetParameters(), argumentTypes, candidate.expansion));

            for (int i = 0; i < candidateParameters.Length;)
            {
                for (int j = 0; j < candidateParameters.Length; j++)
                {
                    if (i == j) continue;
                    var comparison = CompareFunctionMember(
                        candidateParameters[i],
                        candidateParameters[j],
                        argumentTypes);

                    int oldBest = -1;
                    if (best >= 0) oldBest = best;
                    if (comparison < 0) best = i;
                    if (comparison > 0) best = j;
                    if (comparison == 0)
                    {
                        var tie = true;
                        if (!candidates[i].generic && candidates[j].generic) { best = i; tie = false; }
                        if (!candidates[j].generic && candidates[i].generic) { best = j; tie = false; }

                        if (tie)
                        {
                            if (!candidates[i].expansion && candidates[j].expansion) best = i;
                            if (!candidates[j].expansion && candidates[i].expansion) best = j;
                        }
                    }

                    if (best != oldBest && oldBest > 0)
                    {
                        best = -1;
                        break;
                    }

                    if (best == j) break;
                }

                if (best < 0) break;
                if (best == i) break;
                i = best;
            }

            if (best < 0) throw new InvalidOperationException("The method overload call is ambiguous.");
            return candidates[best].call;
        }

        #endregion

        #region Nested Workflow Output

        static IObservable<Unit> IgnoreConnection<TSource>(IObservable<TSource> source)
        {
            return source.IgnoreElements().Select(xs => Unit.Default);
        }

        static IObservable<Unit> MergeOutput(params IObservable<Unit>[] connections)
        {
            return Observable.Merge(connections);
        }

        static IObservable<TSource> MergeOutput<TSource>(IObservable<TSource> source, params IObservable<Unit>[] connections)
        {
            return source.Publish(ps => ps.Merge(Observable.Merge(connections).Select(xs => default(TSource)).TakeUntil(ps.TakeLast(1))));
        }

        internal static Expression BuildOutput(WorkflowOutputBuilder workflowOutput, IEnumerable<Expression> connections)
        {
            var output = workflowOutput != null ? connections.FirstOrDefault(connection => connection == workflowOutput.Output) : null;
            var ignoredConnections = from connection in connections
                                     where connection != output
                                     let observableType = connection.Type.GetGenericArguments()[0]
                                     select Expression.Call(typeof(ExpressionBuilder), "IgnoreConnection", new[] { observableType }, connection);

            var connectionArrayExpression = Expression.NewArrayInit(typeof(IObservable<Unit>), ignoredConnections.ToArray());
            if (output != null)
            {
                var outputType = output.Type.GetGenericArguments()[0];
                return Expression.Call(typeof(ExpressionBuilder), "MergeOutput", new[] { outputType }, output, connectionArrayExpression);
            }
            else return Expression.Call(typeof(ExpressionBuilder), "MergeOutput", null, connectionArrayExpression);
        }

        #endregion

        #region Error Handling

        static readonly ConstructorInfo buildExceptionConstructor = typeof(WorkflowBuildException).GetConstructor(new[] { typeof(string), typeof(ExpressionBuilder), typeof(Exception) });
        static readonly MethodInfo throwMethod = typeof(Observable).GetMethods()
                                                                   .Where(m => m.Name == "Throw")
                                                                   .Single(m => m.GetParameters().Length == 1);

        internal static Expression HandleBuildException(Expression expression, ExpressionBuilder builder)
        {
            var exceptionVariable = Expression.Variable(typeof(Exception));
            var observableType = expression.Type.GetGenericArguments()[0];
            return Expression.TryCatch(
                expression,
                Expression.Catch(
                    exceptionVariable,
                    Expression.Call(
                        throwMethod.MakeGenericMethod(observableType),
                        Expression.New(
                            buildExceptionConstructor,
                            Expression.Property(exceptionVariable, "Message"),
                            Expression.Constant(builder),
                            exceptionVariable))));
        }

        #endregion

        #region Dynamic Properties

        static readonly MethodInfo selectMethod = typeof(Observable).GetMethods()
                                                            .Single(m => m.Name == "Select" &&
                                                                    m.GetParameters().Length == 2 &&
                                                                    m.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(Func<,>));
        static readonly MethodInfo deferMethod = typeof(Observable).GetMethods()
                                                                   .Single(m => m.Name == "Defer" &&
                                                                           m.GetParameters().Length == 1 &&
                                                                           m.GetParameters()[0].ParameterType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(IObservable<>));

        internal static Expression BuildCallRemapping(Expression combinator, Func<Expression, Expression, Expression> callFactory, Expression source, string memberSelector, PropertyMappingCollection propertyMappings, bool hot = false)
        {
            var sourceSelect = source;
            var combinatorType = combinator.Type;

            Expression initializer = null;
            ParameterExpression combinatorCopy = null;

            // If there is no input, there is nothing to do but call the process method
            if (source != null)
            {
                var sourceType = source.Type.GetGenericArguments()[0];

                // If there is a property map, we need to define a closure for
                // dynamic property assignments
                if (propertyMappings != null && propertyMappings.Count > 0)
                {
                    // If the observable is cold, we need to copy the node parameters to allow for reentrant subscriptions;
                    // if it is hot, we keep the same instance and hope for the best
                    combinatorCopy = Expression.Variable(combinatorType);
                    initializer = Expression.Assign(combinatorCopy, hot ? combinator : Expression.New(combinatorType));
                }

                // Remapping input and properties only makes sense if there is either a property or
                // an input reassignment
                if (!string.IsNullOrEmpty(memberSelector) || combinatorCopy != null)
                {
                    var selectorParameter = Expression.Parameter(sourceType);
                    var selectorExpression = Enumerable.Repeat(string.IsNullOrEmpty(memberSelector) ? selectorParameter : ExpressionHelper.MemberAccess(selectorParameter, memberSelector), 1);

                    // Only specify dynamic assignments if necessary
                    if (combinatorCopy != null)
                    {
                        // For each property, lookup the mapping if there is an assignment selector;
                        // if true, evaluate and convert the selector, otherwise, just pick the original property value
                        var propertyAssignments = combinatorType.GetProperties()
                            .Where(propertyInfo => propertyInfo.CanWrite && !propertyInfo.IsDefined(typeof(XmlIgnoreAttribute), true))
                            .Select(propertyInfo =>
                            {
                                Expression propertyValue;
                                var property = Expression.Property(combinatorCopy, propertyInfo);
                                if (propertyMappings.Contains(propertyInfo.Name))
                                {
                                    propertyValue = ExpressionHelper.MemberAccess(selectorParameter, propertyMappings[propertyInfo.Name].Selector);
                                    if (propertyValue.Type != property.Type)
                                    {
                                        propertyValue = Expression.Convert(propertyValue, property.Type);
                                    }
                                }
                                else propertyValue = Expression.Property(combinator, propertyInfo);

                                return Expression.Assign(property, propertyValue);
                            });
                        selectorExpression = propertyAssignments.Concat(selectorExpression);
                    }

                    var selectorBlock = Expression.Block(selectorExpression);
                    var selector = Expression.Lambda(selectorBlock, selectorParameter);
                    sourceSelect = Expression.Call(selectMethod.MakeGenericMethod(selectorParameter.Type, selectorBlock.Type), source, selector);
                }
            }

            // Generate decorator expression using the current parameter assignment selector
            var decoratedSource = callFactory(combinatorCopy ?? combinator, sourceSelect);

            // If a closure was created, enforce "cold" subscription side-effects
            if (combinatorCopy != null)
            {
                var deferBlock = Expression.Block(new[] { combinatorCopy }, initializer, decoratedSource);
                var observableFactory = Expression.Lambda(deferBlock);
                return Expression.Call(deferMethod.MakeGenericMethod(decoratedSource.Type.GetGenericArguments()), observableFactory);
            }
            else return decoratedSource;
        }

        #endregion
    }
}
