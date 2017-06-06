﻿using Bonsai.Design;
using Bonsai.Shaders.Design;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bonsai.Shaders.Configuration.Design
{
    public class ShaderConfigurationComponentEditor : WorkflowComponentEditor
    {
        static ShaderConfigurationEditorDialog editorDialog;

        internal void EditConfiguration(IWin32Window owner)
        {
            if (editorDialog == null)
            {
                RefreshEventHandler editorRefreshed;
                editorDialog = new ShaderConfigurationEditorDialog();
                editorRefreshed = e => editorDialog.Close();
                TypeDescriptor.Refreshed += editorRefreshed;
                editorDialog.FormClosed += (sender, e) =>
                {
                    TypeDescriptor.Refreshed -= editorRefreshed;
                    editorDialog = null;
                };
                editorDialog.SelectedPage = ShaderConfigurationEditorPage.Window;
                foreach (var example in GetShaderExamples())
                {
                    editorDialog.ScriptExamples.Add(example);
                }
                editorDialog.Show(owner);
            }
            else
            {
                if (editorDialog.WindowState == FormWindowState.Minimized)
                {
                    editorDialog.WindowState = FormWindowState.Normal;
                }
                editorDialog.Activate();
            }
        }

        internal void EditConfiguration(ShaderConfigurationEditorPage selectedPage, IWin32Window owner)
        {
            EditConfiguration(owner);
            editorDialog.SelectedPage = selectedPage;
        }

        public override bool EditComponent(ITypeDescriptorContext context, object component, IServiceProvider provider, IWin32Window owner)
        {
            EditConfiguration(owner);
            return false;
        }

        protected virtual GlslScriptExample[] GetShaderExamples()
        {
            return new[]
            {
                new GlslScriptExample
                {
                    Name = "Clip-space Textured",
                    Type = ShaderType.VertexShader,
                    Source = @"#version 400
uniform vec2 scale = vec2(1, 1);
uniform vec2 shift;
layout(location = 0) in vec2 vp;
layout(location = 1) in vec2 vt;
out vec2 texCoord;

void main()
{
  gl_Position = vec4(vp * scale + shift, 0.0, 1.0);
  texCoord = vt;
}
"
                },

                new GlslScriptExample
                {
                    Name = "Textured Model",
                    Type = ShaderType.VertexShader,
                    Source = @"#version 400
uniform mat4 modelview;
uniform mat4 projection;
layout(location = 0) in vec3 vp;
layout(location = 1) in vec2 vt;
layout(location = 2) in vec3 vn;
out vec3 position;
out vec2 texCoord;
out vec3 normal;

void main()
{
  mat4 normalmat = transpose(inverse(modelview));
  vec4 v = modelview * vec4(vp, 1.0);
  gl_Position = projection * v;
  position = vec3(v);
  texCoord = vt;
  normal = normalize(vec3(normalmat * vec4(vn, 0.0)));
}
"
                },

                new GlslScriptExample
                {
                    Name = "Viewport Effect",
                    Type = ShaderType.FragmentShader,
                    Source = @"#version 400
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  fragColor = vec4(texCoord, 0.0, 1.0);
}
"
                },

                new GlslScriptExample
                {
                    Name = "Diffuse Texture",
                    Type = ShaderType.FragmentShader,
                    Source = @"#version 400
uniform sampler2D tex;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  vec4 texel = texture(tex, texCoord);
  fragColor = texel;
}
"
                },

                new GlslScriptExample
                {
                    Name = "Phong Shading",
                    Type = ShaderType.FragmentShader,
                    Source = @"#version 400
uniform vec3 Ka;
uniform vec3 Kd;
uniform vec3 Ks;
uniform float Ns = 1.0;
uniform sampler2D mapKd;
uniform vec3 light;
in vec3 position;
in vec2 texCoord;
in vec3 normal;
out vec4 fragColor;

void main()
{
  vec3 L = normalize(light - position);
  vec3 R = normalize(-reflect(L, normal));
  vec3 V = normalize(-position);

  vec3 Iamb = Ka;
  vec3 Idiff = Kd * texture(mapKd, texCoord).rgb * max(dot(normal, L), 0.0);
  vec3 Ispec = Ks * pow(max(dot(R, V), 0.0), Ns);

  fragColor = vec4(Iamb + Idiff + Ispec, 1.0);
}
"
                }
            };
        }
    }
}
