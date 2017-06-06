﻿using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Shaders.Input
{
    [Description("Produces a sequence of characters whenever a key is pressed while the shader window has focus.")]
    [Editor("Bonsai.Shaders.Configuration.Design.ShaderConfigurationComponentEditor, Bonsai.Shaders.Design", typeof(ComponentEditor))]
    public class KeyPress : Source<EventPattern<INativeWindow, KeyPressEventArgs>>
    {
        public override IObservable<EventPattern<INativeWindow, KeyPressEventArgs>> Generate()
        {
            return ShaderManager.WindowSource.SelectMany(window => window.EventPattern<KeyPressEventArgs>(
                handler => window.KeyPress += handler,
                handler => window.KeyPress -= handler));
        }
    }
}
