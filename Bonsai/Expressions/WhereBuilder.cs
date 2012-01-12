﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Bonsai.Expressions
{
    [XmlType("Where")]
    public class WhereBuilder : CombinatorBuilder
    {
        static readonly MethodInfo whereMethod = typeof(Observable).GetMethods()
                                                                   .First(m => m.Name == "Where" &&
                                                                          m.GetParameters().Length == 2);

        [Browsable(false)]
        public LoadableElement Filter { get; set; }

        public override Expression Build()
        {
            var filterGenericArgument = ExpressionBuilder.GetFilterGenericArgument(Filter);
            var predicateType = Expression.GetFuncType(new[] { filterGenericArgument, typeof(bool) });

            var processMethod = Filter.GetType().GetMethod("Process");
            var predicateDelegate = Delegate.CreateDelegate(predicateType, Filter, processMethod);
            var predicate = Expression.Constant(predicateDelegate);
            return Expression.Call(whereMethod.MakeGenericMethod(filterGenericArgument), Source, predicate);
        }
    }
}
