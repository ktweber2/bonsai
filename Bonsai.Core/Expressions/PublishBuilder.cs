﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Bonsai.Expressions
{
    /// <summary>
    /// Represents an expression builder that shares a single subscription to an observable
    /// sequence across the encapsulated workflow.
    /// </summary>
    [XmlType("Publish", Namespace = Constants.XmlNamespace)]
    [Description("Shares a single subscription to an observable sequence across the encapsulated workflow.")]
    public class PublishBuilder : MulticastBuilder
    {
        internal override IObservable<TResult> Multicast<TSource, TResult>(IObservable<TSource> source, Func<IObservable<TSource>, IObservable<TResult>> selector)
        {
            return source.Publish(selector);
        }
    }
}
