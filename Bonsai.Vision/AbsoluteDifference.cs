﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCV.Net;

namespace Bonsai.Vision
{
    public class AbsoluteDifference : Projection<IplImage, IplImage, IplImage>
    {
        public override IplImage Process(IplImage first, IplImage second)
        {
            var output = new IplImage(first.Size, first.Depth, first.NumChannels);
            Core.cvAbsDiff(first, second, output);
            return output;
        }
    }
}
