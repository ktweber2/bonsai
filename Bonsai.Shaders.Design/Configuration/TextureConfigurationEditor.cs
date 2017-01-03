﻿using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonsai.Shaders.Configuration.Design
{
    class TextureConfigurationEditor : ShaderConfigurationEditor
    {
        protected override ShaderConfigurationControl CreateEditorControl()
        {
            return new TextureConfigurationControl();
        }

        class TextureConfigurationControl : ShaderConfigurationControl
        {
            public TextureConfigurationControl()
            {
                Text = "Manage Textures";
            }

            protected override IEnumerable<string> GetConfigurationNames()
            {
                return ShaderManager.LoadConfiguration().Textures.Select(configuration => configuration.Name);
            }

            protected override UITypeEditor CreateConfigurationEditor(Type type)
            {
                return new ShaderWindowEditor
                {
                    SelectedPage = ShaderConfigurationEditorPage.Textures
                };
            }
        }
    }
}
