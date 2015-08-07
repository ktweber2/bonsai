﻿using OpenCV.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Vision
{
    [Description("Performs forward or inverse linear-polar image transform. This transform \"emulates\" human foveal vision.")]
    public class LinearPolar : PolarTransform
    {
        public override IObservable<IplImage> Process(IObservable<IplImage> source)
        {
            return source.Select(input =>
            {
                var output = new IplImage(input.Size, input.Depth, input.Channels);
                CV.LinearPolar(input, output, Center, Magnitude, Flags);
                return output;
            });
        }
    }
}
