﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Bonsai.Expressions
{
    [XmlType("DistinctUntilChanged", Namespace = Constants.XmlNamespace)]
    [Description("Ensures that only distinct contiguous values are propagated.")]
    public class DistinctUntilChangedBuilder : CombinatorBuilder
    {
        protected override IObservable<TSource> Combine<TSource>(IObservable<TSource> source)
        {
            return source.DistinctUntilChanged();
        }
    }
}
