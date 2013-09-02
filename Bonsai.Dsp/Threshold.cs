﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCV.Net;
using System.ComponentModel;
using System.Drawing.Design;

namespace Bonsai.Dsp
{
    [Description("Applies a fixed threshold to the input signal.")]
    public class Threshold : Selector<Mat, Mat>
    {
        [Editor(DesignTypes.NumericUpDownEditor, typeof(UITypeEditor))]
        [Description("The threshold value used to test individual samples.")]
        public double ThresholdValue { get; set; }

        [Description("The value assigned to samples determined to be above the threshold.")]
        public double MaxValue { get; set; }

        [Description("The type of threshold to apply to individual samples.")]
        public ThresholdTypes ThresholdType { get; set; }

        public override Mat Process(Mat input)
        {
            var output = new Mat(input.Rows, input.Cols, input.Depth, input.Channels);
            CV.Threshold(input, output, ThresholdValue, MaxValue, ThresholdType);
            return output;
        }
    }
}
