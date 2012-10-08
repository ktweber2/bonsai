﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Microsoft.Scripting.Hosting;
using System.Drawing.Design;
using Bonsai.Expressions;
using System.Reactive.Linq;
using System.Linq.Expressions;

namespace Bonsai.Scripting
{
    [Description("A Python script used to operate on individual elements of the input sequence.")]
    public class PythonSink : CombinatorExpressionBuilder
    {
        public PythonSink()
        {
            Script = "def process(input):\n";
        }

        [Editor(typeof(PythonScriptEditor), typeof(UITypeEditor))]
        [Description("The script that determines the operation of the sink.")]
        public string Script { get; set; }

        public override Expression Build()
        {
            var engine = IronPython.Hosting.Python.CreateEngine();
            var scope = engine.CreateScope();
            var scriptSource = engine.CreateScriptSourceFromString(Script);
            scriptSource.Execute(scope);

            var observableType = Source.Type.GetGenericArguments()[0];
            var scopeExpression = Expression.Constant(scope);
            var actionType = Expression.GetActionType(observableType);
            var processExpression = Expression.Call(scopeExpression, "GetVariable", new[] { actionType }, Expression.Constant("process"));

            var combinatorExpression = Expression.Constant(this);
            return Expression.Call(combinatorExpression, "Combine", new[] { observableType }, Source, processExpression, scopeExpression);
        }

        protected virtual IObservable<TSource> Combine<TSource>(IObservable<TSource> source, Action<TSource> action, ScriptScope scope)
        {
            return source.Do(action);
        }
    }
}
