﻿using Bonsai;
using Bonsai.Design;
using Bonsai.Vision.Design;
using OpenCV.Net;
using System;

[assembly: TypeVisualizer(typeof(PolygonVisualizer), Target = typeof(Point[][]))]

namespace Bonsai.Vision.Design
{
    /// <summary>
    /// Provides a type visualizer for a collection of polygonal regions. If the input
    /// is a sequence of images, the visualizer will overlay each rectangle on top of
    /// the original image.
    /// </summary>
    public class PolygonVisualizer : IplImageVisualizer
    {
        const float DefaultHeight = 480;
        const int DefaultThickness = 2;
        ObjectTextVisualizer textVisualizer;
        IDisposable inputHandle;
        IplImage input;
        IplImage canvas;

        internal static void Draw(IplImage image, object value)
        {
            if (image != null && value is Point[][] points)
            {
                var color = image.Channels == 1 ? Scalar.Real(255) : Scalar.Rgb(255, 0, 0);
                var thickness = DefaultThickness * (int)Math.Ceiling(image.Height / DefaultHeight);
                CV.PolyLine(image, points, true, color, thickness);
            }
        }

        /// <inheritdoc/>
        public override void Show(object value)
        {
            if (textVisualizer != null) textVisualizer.Show(value);
            else
            {
                if (input != null)
                {
                    canvas = IplImageHelper.EnsureColorCopy(canvas, input);
                    Draw(canvas, value);
                    base.Show(canvas);
                }
            }
        }

        /// <inheritdoc/>
        public override void Load(IServiceProvider provider)
        {
            var imageInput = VisualizerHelper.ImageInput(provider);
            if (imageInput != null)
            {
                inputHandle = imageInput.Subscribe(value => input = (IplImage)value);
                base.Load(provider);
            }
            else
            {
                textVisualizer = new ObjectTextVisualizer();
                textVisualizer.Load(provider);
            }
        }

        /// <inheritdoc/>
        public override IObservable<object> Visualize(IObservable<IObservable<object>> source, IServiceProvider provider)
        {
            if (textVisualizer != null) return textVisualizer.Visualize(source, provider);
            else return base.Visualize(source, provider);
        }

        /// <inheritdoc/>
        public override void Unload()
        {
            if (canvas != null)
            {
                canvas.Close();
                canvas = null;
            }

            if (inputHandle != null)
            {
                inputHandle.Dispose();
                inputHandle = null;
            }

            if (textVisualizer != null)
            {
                textVisualizer.Unload();
                textVisualizer = null;
            }
            else base.Unload();
        }
    }
}
