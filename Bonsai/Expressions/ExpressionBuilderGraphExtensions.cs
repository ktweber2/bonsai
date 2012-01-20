﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bonsai.Dag;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reflection;

namespace Bonsai.Expressions
{
    public static class ExpressionBuilderGraphExtensions
    {
        static readonly ConstructorInfo compositeDisposableConstructor = typeof(CompositeDisposable).GetConstructor(new[] { typeof(IEnumerable<IDisposable>) });
        static readonly MethodInfo subscribeMethod = typeof(ObservableExtensions).GetMethods().First(m => m.Name == "Subscribe" && m.GetParameters().Length == 3);

        public static Type ExpressionType(this ExpressionBuilderGraph source, Node<ExpressionBuilder, ExpressionBuilderParameter> node)
        {
            if (!source.Contains(node))
            {
                throw new ArgumentException("The specified node is not a member of the graph.", "node");
            }

            foreach (var expressionNode in source.TopologicalSort())
            {
                var expression = expressionNode.Value.Build();
                if (expressionNode == node) return expression.Type;
                else
                {
                    foreach (var successor in expressionNode.Successors)
                    {
                        var target = successor.Node.Value.GetType().GetProperty(successor.Label.Value);
                        target.SetValue(successor.Node.Value, expression, null);
                    }
                }
            }

            throw new ArgumentException("Cannot infer expression type on cyclic graphs.", "source");
        }

        public static IEnumerable<Expression> Build(this ExpressionBuilderGraph source)
        {
            foreach (var node in source.TopologicalSort())
            {
                var expression = node.Value.Build();
                if (node.Successors.Count > 1)
                {
                    // Publish workflow result to avoid repeating operations
                    var publishBuilder = new PublishBuilder { Source = expression };
                    var publish = Expression.Lambda(publishBuilder.Build()).Compile();
                    expression = Expression.Constant(publish.DynamicInvoke());
                }

                foreach (var successor in node.Successors)
                {
                    var target = successor.Node.Value.GetType().GetProperty(successor.Label.Value);
                    target.SetValue(successor.Node.Value, expression, null);
                }

                if (node.Successors.Count == 0)
                {
                    yield return expression;
                }
            }
        }

        public static Expression<Func<IDisposable>> BuildSubscribe(this ExpressionBuilderGraph source, Action<Exception> onError)
        {
            var onErrorExpression = Expression.Constant(onError);
            var subscribeActions = from expression in source.Build()
                                   let observableType = expression.Type.GetGenericArguments()[0]
                                   let onNextParameter = Expression.Parameter(observableType)
                                   let onNext = Expression.Lambda(Expression.Empty(), onNextParameter)
                                   select Expression.Call(subscribeMethod.MakeGenericMethod(observableType), expression, onNext, onErrorExpression);

            var subscriptions = Expression.NewArrayInit(typeof(IDisposable), subscribeActions);
            var disposable = Expression.New(compositeDisposableConstructor, subscriptions);
            return Expression.Lambda<Func<IDisposable>>(disposable);
        }

        public static ExpressionBuilderGraph ToInspectableGraph(this ExpressionBuilderGraph source)
        {
            var observableMapping = new Dictionary<Node<ExpressionBuilder, ExpressionBuilderParameter>, Tuple<Node<ExpressionBuilder, ExpressionBuilderParameter>, Node<ExpressionBuilder, ExpressionBuilderParameter>>>();
            var observableGraph = new ExpressionBuilderGraph();
            foreach (var node in source)
            {
                var observableExpression = new InspectBuilder();
                var observableNode = new Node<ExpressionBuilder, ExpressionBuilderParameter>(observableExpression);
                var expressionNode = new Node<ExpressionBuilder, ExpressionBuilderParameter>(node.Value);
                observableGraph.Add(expressionNode);
                observableGraph.Add(observableNode);
                observableGraph.AddEdge(expressionNode, observableNode, new ExpressionBuilderParameter("Source"));
                observableMapping.Add(node, Tuple.Create(expressionNode, observableNode));
            }

            foreach (var node in source)
            {
                var observableNode = observableMapping[node].Item2;
                foreach (var successor in node.Successors)
                {
                    var successorNode = observableMapping[successor.Node].Item1;
                    var parameter = new ExpressionBuilderParameter(successor.Label.Value);
                    observableGraph.AddEdge(observableNode, successorNode, parameter);
                }
            }

            return observableGraph;
        }
    }
}
