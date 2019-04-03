﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Bonsai.Expressions;

namespace Bonsai.Design
{
    class GraphNode
    {
        static readonly Range<int> EmptyRange = Range.Create(0, 0);
        static readonly Brush DisabledBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Black, Color.Transparent);
        static readonly Brush ObsoleteBrush = new HatchBrush(HatchStyle.OutlinedDiamond, Color.Black, Color.Transparent);
        static readonly Pen DashPen = new Pen(Brushes.DarkGray) { DashPattern = new[] { 4f, 2f } };
        static readonly Pen SolidPen = Pens.DarkGray;

        public GraphNode(ExpressionBuilder value, int layer, IEnumerable<GraphEdge> successors)
        {
            Value = value;
            Layer = layer;
            Successors = successors;

            Pen = SolidPen;
            if (value != null)
            {
                var expressionBuilder = ExpressionBuilder.Unwrap(value);
                var elementAttributes = TypeDescriptor.GetAttributes(expressionBuilder);
                var elementCategoryAttribute = (WorkflowElementCategoryAttribute)elementAttributes[typeof(WorkflowElementCategoryAttribute)];
                var obsolete = (ObsoleteAttribute)elementAttributes[typeof(ObsoleteAttribute)] != null;
                if (expressionBuilder is DisableBuilder) Flags |= NodeFlags.Disabled;

                var workflowElement = ExpressionBuilder.GetWorkflowElement(expressionBuilder);
                if (workflowElement != expressionBuilder)
                {
                    var builderCategoryAttribute = elementCategoryAttribute;
                    elementAttributes = TypeDescriptor.GetAttributes(workflowElement);
                    elementCategoryAttribute = (WorkflowElementCategoryAttribute)elementAttributes[typeof(WorkflowElementCategoryAttribute)];
                    obsolete |= (ObsoleteAttribute)elementAttributes[typeof(ObsoleteAttribute)] != null;
                    if (elementCategoryAttribute == WorkflowElementCategoryAttribute.Default)
                    {
                        elementCategoryAttribute = builderCategoryAttribute;
                    }
                }

                if (obsolete) Flags |= NodeFlags.Obsolete;
                Category = elementCategoryAttribute.Category;
                Pen = expressionBuilder.IsBuildDependency() ? DashPen : SolidPen;
                Icon = new ElementIcon(workflowElement);
                if (workflowElement is IWorkflowExpressionBuilder)
                {
                    if (Category == ElementCategory.Workflow)
                    {
                        Category = ElementCategory.Combinator;
                        Flags |= NodeFlags.NestedGroup;
                    }
                    else Flags |= NodeFlags.NestedScope;
                }
            }

            InitializeDummySuccessors();
        }

        void InitializeDummySuccessors()
        {
            foreach (var successor in Successors)
            {
                if (successor.Node.Value == null)
                {
                    successor.Node.Pen = Pen;
                    successor.Node.InitializeDummySuccessors();
                }
            }
        }

        private NodeFlags Flags { get; set; }

        public int Layer { get; internal set; }

        public int LayerIndex { get; internal set; }

        public int ArgumentCount { get; internal set; }

        public Range<int> ArgumentRange
        {
            get { return (Flags & NodeFlags.Disabled) != 0 || Value == null ? EmptyRange : Value.ArgumentRange; }
        }

        public ExpressionBuilder Value { get; private set; }

        public IEnumerable<GraphEdge> Successors { get; private set; }

        public object Tag { get; set; }

        public Brush Brush
        {
            get
            {
                switch (Category)
                {
                    case ElementCategory.Source: return CategoryBrushes.Source;
                    case ElementCategory.Condition: return CategoryBrushes.Combinator;
                    case ElementCategory.Transform: return CategoryBrushes.Transform;
                    case ElementCategory.Sink: return CategoryBrushes.Sink;
                    case ElementCategory.Nested:
                    case ElementCategory.Workflow: return CategoryBrushes.Combinator;
                    case ElementCategory.Property: return CategoryBrushes.Property;
                    case ElementCategory.Combinator:
                    default: return CategoryBrushes.Combinator;
                }
            }
        }

        public Brush ModifierBrush
        {
            get
            {
                if ((Flags & NodeFlags.Disabled) != 0) return DisabledBrush;
                else if ((Flags & NodeFlags.Obsolete) != 0) return ObsoleteBrush;
                else return null;
            }
        }

        public ElementCategory? NestedCategory
        {
            get
            {
                if ((Flags & NodeFlags.NestedScope) != 0) return ElementCategory.Nested;
                else if ((Flags & NodeFlags.NestedGroup) != 0) return ElementCategory.Workflow;
                else return null;
            }
        }

        public ElementCategory Category { get; private set; }

        public ElementIcon Icon { get; private set; }

        public Pen Pen { get; private set; }

        public string Text
        {
            get { return Value != null ? ExpressionBuilder.GetElementDisplayName(Value) : string.Empty; }
        }

        public bool Highlight
        {
            get { return (Flags & NodeFlags.Highlight) != 0; }
            set
            {
                if (value) Flags |= NodeFlags.Highlight;
                else Flags &= ~NodeFlags.Highlight;
            }
        }


        /// <summary>
        /// Returns a string that represents the value of this <see cref="GraphNode"/> instance.
        /// </summary>
        /// <returns>
        /// The string representation of this <see cref="GraphNode"/> object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{{{0}}}", Text);
        }

        [Flags]
        enum NodeFlags
        {
            None = 0x0,
            Highlight = 0x1,
            Obsolete = 0x2,
            Disabled = 0x4,
            NestedScope = 0x8,
            NestedGroup = 0x10
        }

        static class CategoryBrushes
        {
            public static readonly Brush Source = new SolidBrush(Color.FromArgb(91, 178, 126));
            public static readonly Brush Transform = new SolidBrush(Color.FromArgb(68, 154, 223));
            public static readonly Brush Sink = new SolidBrush(Color.FromArgb(155, 91, 179));
            public static readonly Brush Combinator = new SolidBrush(Color.FromArgb(238, 192, 75));
            public static readonly Brush Property = Brushes.Gray;
        }
    }
}
