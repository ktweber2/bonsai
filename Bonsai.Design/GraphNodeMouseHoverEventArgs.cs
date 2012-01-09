﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bonsai.Design
{
    public class GraphNodeMouseHoverEventArgs : EventArgs
    {
        public GraphNodeMouseHoverEventArgs(GraphNode node)
        {
            Node = node;
        }

        public GraphNode Node { get; private set; }
    }
}
