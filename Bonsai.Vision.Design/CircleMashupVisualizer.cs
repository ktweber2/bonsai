﻿using Bonsai;
using Bonsai.Design;
using Bonsai.Vision.Design;
using System;

[assembly: TypeVisualizer(typeof(CircleMashupVisualizer), Target = typeof(VisualizerMashup<ImageMashupVisualizer, CircleVisualizer>))]

namespace Bonsai.Vision.Design
{
    /// <summary>
    /// Provides a type visualizer that overlays the visual representation of a
    /// circle over an existing image visualizer.
    /// </summary>
    public class CircleMashupVisualizer : MashupTypeVisualizer
    {
        ImageMashupVisualizer visualizer;

        /// <inheritdoc/>
        public override void Show(object value)
        {
            CircleVisualizer.Draw(visualizer.VisualizerImage, value);
        }

        /// <inheritdoc/>
        public override void Load(IServiceProvider provider)
        {
            visualizer = (ImageMashupVisualizer)provider.GetService(typeof(DialogMashupVisualizer));
        }

        /// <inheritdoc/>
        public override void Unload()
        {
        }
    }
}
