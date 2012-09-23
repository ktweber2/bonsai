﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCV.Net;
using System.ComponentModel;

namespace Bonsai.Vision
{
    [Description("Converts a BGR color image to grayscale.")]
    public class Grayscale : Projection<IplImage, IplImage>
    {
        public override IplImage Process(IplImage input)
        {
            var output = new IplImage(input.Size, 8, 1);
            ImgProc.cvCvtColor(input, output, ColorConversion.BGR2GRAY);
            return output;
        }
    }
}
