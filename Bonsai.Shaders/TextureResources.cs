﻿using Bonsai.Shaders.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bonsai.Shaders
{
    [DefaultProperty("Textures")]
    [Description("Creates a collection of texture resources to be loaded into the resource manager.")]
    public class TextureResources : ResourceLoader
    {
        readonly TextureConfigurationCollection textures = new TextureConfigurationCollection();

        [XmlArrayItem(typeof(Texture2D))]
        [XmlArrayItem(typeof(ImageTexture))]
        [Editor("Bonsai.Shaders.Configuration.Design.TextureConfigurationCollectionEditor, Bonsai.Shaders.Design", typeof(UITypeEditor))]
        [Description("The collection of texture resources to be loaded into the resource manager.")]
        public TextureConfigurationCollection Textures
        {
            get { return textures; }
        }

        protected internal override IEnumerable<IResourceConfiguration> GetResources()
        {
            return textures;
        }
    }
}
