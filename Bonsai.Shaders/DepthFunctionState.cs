﻿using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Shaders
{
    public class DepthFunctionState : StateConfiguration
    {
        public DepthFunction Function { get; set; }

        public override void Execute()
        {
            GL.DepthFunc(Function);
        }

        public override string ToString()
        {
            return string.Format("DepthFunc({0})", Function);
        }
    }
}
