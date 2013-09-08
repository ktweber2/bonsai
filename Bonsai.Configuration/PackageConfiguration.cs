﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Bonsai.Configuration
{
    public class PackageConfiguration
    {
        readonly PackageReferenceCollection packages = new PackageReferenceCollection();
        readonly AssemblyLocationCollection assemblyLocations = new AssemblyLocationCollection();
        readonly LibraryFolderCollection libraryFolders = new LibraryFolderCollection();

        [XmlIgnore]
        internal string ConfigurationFile { get; set; }

        public PackageReferenceCollection Packages
        {
            get { return packages; }
        }

        public AssemblyLocationCollection AssemblyLocations
        {
            get { return assemblyLocations; }
        }

        public LibraryFolderCollection LibraryFolders
        {
            get { return libraryFolders; }
        }
    }
}
