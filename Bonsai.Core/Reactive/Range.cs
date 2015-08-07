﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Bonsai.Reactive
{
    /// <summary>
    /// Represents an observable sequence of integral numbers within a specified range.
    /// </summary>
    [XmlType(Namespace = Constants.XmlNamespace)]
    [Description("Generates an observable sequence of integral numbers within a specified range.")]
    public class Range : Source<int>
    {
        /// <summary>
        /// Gets or sets the value of the first integer in the sequence.
        /// </summary>
        [Description("The value of the first integer in the sequence.")]
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the number of sequential integers to generate.
        /// </summary>
        [Description("The number of sequential integers to generate.")]
        public int Count { get; set; }

        /// <summary>
        /// Generates an observable sequence of integral numbers within a specified range.
        /// </summary>
        /// <returns>
        /// An observable sequence that contains a range of sequential integral numbers.
        /// </returns>
        public override IObservable<int> Generate()
        {
            return Observable.Range(Start, Count);
        }

        /// <summary>
        /// Generates an observable sequence of integral numbers within a specified range
        /// whenever the source sequence emits a new element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The source sequence used to advance the range.</param>
        /// <returns>
        /// An observable sequence that contains a range of sequential integral numbers.
        /// </returns>
        public IObservable<int> Generate<TSource>(IObservable<TSource> source)
        {
            return source.Zip(Enumerable.Range(Start, Count), (x, number) => number);
        }
    }
}
