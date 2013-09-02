﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCV.Net;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace Bonsai.Dsp
{
    public class Delay : Transform<Mat, Mat>
    {
        public int Count { get; set; }

        public override IObservable<Mat> Process(IObservable<Mat> source)
        {
            return Observable.Defer(() =>
            {
                int bufferIndex = 0;
                Queue<Mat> buffer = null;
                return source.Select(input =>
                {
                    if (buffer == null)
                    {
                        buffer = new Queue<Mat>();
                        var delayBuffer = new Mat(input.Rows, Count, input.Depth, input.Channels);
                        buffer.Enqueue(delayBuffer);
                        delayBuffer.SetZero();
                    }

                    buffer.Enqueue(input);
                    var output = new Mat(input.Rows, input.Cols, input.Depth, input.Channels);
                    var outputRemainder = output.Cols;
                    var outputIndex = 0;

                    while (outputRemainder > 0)
                    {
                        var currentBuffer = buffer.Peek();
                        var sampleCount = Math.Min(currentBuffer.Cols - bufferIndex, outputRemainder);
                        using (var bufferRoi = currentBuffer.GetSubRect(new Rect(bufferIndex, 0, sampleCount, currentBuffer.Rows)))
                        using (var outputRoi = output.GetSubRect(new Rect(outputIndex, 0, sampleCount, currentBuffer.Rows)))
                        {
                            CV.Copy(bufferRoi, outputRoi);
                            outputRemainder -= sampleCount;
                            outputIndex += sampleCount;
                            bufferIndex = (bufferIndex + sampleCount) % currentBuffer.Cols;
                            if (bufferIndex == 0) buffer.Dequeue();
                        }
                    }

                    return output;
                });
            });
        }
    }
}
