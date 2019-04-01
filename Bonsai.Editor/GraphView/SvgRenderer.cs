﻿using SvgNet;
using SvgNet.SvgElements;
using SvgNet.SvgGdi;
using SvgNet.SvgTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bonsai.Design
{
    delegate void SvgRenderer(SvgRendererState state, IGraphics graphics);

    class SvgRendererState
    {
        public float Scale;
        public PointF Translation;
        public Pen Outlining;
        public Brush Foreground;
    }

    class SvgRendererContext
    {
        readonly ParameterExpression state;
        readonly ParameterExpression graphics;
        readonly Dictionary<string, Brush> gradients;
        readonly List<Expression> expressions;

        public SvgRendererContext()
        {
            state = Expression.Parameter(typeof(SvgRendererState), "state");
            graphics = Expression.Parameter(typeof(IGraphics), "graphics");
            gradients = new Dictionary<string, Brush>();
            expressions = new List<Expression>();
        }

        public ParameterExpression State
        {
            get { return state; }
        }

        public ParameterExpression Graphics
        {
            get { return graphics; }
        }

        public IDictionary<string, Brush> Gradients
        {
            get { return gradients; }
        }

        public ICollection<Expression> Expressions
        {
            get { return expressions; }
        }
    }

    class SvgRendererFactory : IDisposable
    {
        bool disposed;
        readonly List<IDisposable> disposableResources = new List<IDisposable>();
        readonly Dictionary<string, SvgRenderer> rendererCache = new Dictionary<string, SvgRenderer>();

        static float ParseFloat(SvgElement element, string attribute)
        {
            return float.Parse((string)element.Attributes[attribute], CultureInfo.InvariantCulture);
        }

        static SvgPath ParsePath(SvgElement element, string attribute)
        {
            return (SvgPath)(string)element.Attributes[attribute];
        }

        static PointF? ParsePoint(SvgElement element, string x, string y)
        {
            var valX = (string)element.Attributes[x];
            var valY = (string)element.Attributes[y];
            if (valX != null && valY != null)
            {
                return new PointF(((SvgLength)valX).Value, ((SvgLength)valY).Value);
            }
            else return null;
        }

        [DebuggerDisplay("Fill = {Fill}, Stroke = {Stroke}")]
        class SvgDrawingStyle
        {
            public Expression Fill;
            public Expression Stroke;

            public SvgDrawingStyle(Expression fill, Expression stroke)
            {
                Fill = fill;
                Stroke = stroke;
            }
        }

        SvgDrawingStyle CreateStyle(SvgElement element, SvgRendererContext context)
        {
            Expression fill, stroke;
            var value = element.Attributes["style"];
            if (value != null)
            {
                var style = value as SvgStyle;
                if (style == null)
                {
                    var rawStyle = value as string;
                    if (rawStyle == null) return null;
                    style = (SvgStyle)rawStyle;
                }

                fill = CreateFill(style, context);
                stroke = CreateStroke(style, context);
            }
            else
            {
                fill = CreateFill(element, context);
                stroke = CreateStroke(element, context);
            }

            if (fill == null && stroke == null) return null;
            else return new SvgDrawingStyle(fill, stroke);
        }

        Matrix ParseTransform(SvgElement element, Matrix parent)
        {
            return ParseTransform(element, parent, "transform");
        }

        Matrix ParseTransform(SvgElement element, Matrix parent, string attribute)
        {
            var result = new Matrix();
            disposableResources.Add(result);
            var transformAttribute = element.Attributes[attribute];
            if (transformAttribute != null)
            {
                var transformList = transformAttribute as SvgTransformList;
                if (transformList == null) transformList = (SvgTransformList)(string)transformAttribute;
                for (int i = 0; i < transformList.Count; i++)
                {
                    var transform = transformList[i];
                    result.Multiply(transform.Matrix);
                }
            }
            if (parent != null) result.Multiply(parent, MatrixOrder.Append);
            return result;
        }

        static Expression CreateFloat(SvgElement element, string attribute)
        {
            var value = ParseFloat(element, attribute);
            return Expression.Constant(value, typeof(float));
        }

        Expression CreateTransform(SvgElement element, Matrix parent)
        {
            var transform = ParseTransform(element, parent);
            return Expression.Constant(transform);
        }

        Expression CreateFill(SvgStyle style, SvgRendererContext context)
        {
            var fill = (string)style.Get("fill");
            var opacity = (string)style.Get("fill-opacity");
            return CreateFill(fill, opacity, context);
        }

        Expression CreateFill(SvgElement element, SvgRendererContext context)
        {
            var fill = (string)element["fill"];
            var opacity = (string)element["fill-opacity"];
            return CreateFill(fill, opacity, context);
        }

        Expression CreateFill(string fill, string opacity, SvgRendererContext context)
        {
            const string UrlPrefix = "url(";
            if (fill == null) return Expression.PropertyOrField(context.State, "Foreground");
            if (fill == "none") return null;
            if (fill.StartsWith(UrlPrefix))
            {
                Brush brush;
                var href = fill.Substring(UrlPrefix.Length, fill.Length - UrlPrefix.Length - 1);
                if (!context.Gradients.TryGetValue(href, out brush)) return null;
                else return Expression.Constant(brush);
            }
            else
            {
                var color = ((SvgColor)fill).Color;
                if (opacity != null)
                {
                    var opacityValue = ((SvgLength)opacity).Value;
                    color = Color.FromArgb((int)(opacityValue * 255), color);
                }

                var brush = new SolidBrush(color);
                disposableResources.Add(brush);
                return Expression.Constant(brush);
            }
        }

        Expression CreateStroke(SvgStyle style, SvgRendererContext context)
        {
            var stroke = (string)style.Get("stroke");
            var strokeWidth = (string)style.Get("stroke-width");
            var strokeOpacity = (string)style.Get("stroke-opacity");
            return CreateStroke(stroke, strokeWidth, strokeOpacity, context);
        }

        Expression CreateStroke(SvgElement element, SvgRendererContext context)
        {
            var stroke = (string)element["stroke"];
            var strokeWidth = (string)element["stroke-width"];
            var strokeOpacity = (string)element["stroke-opacity"];
            return CreateStroke(stroke, strokeWidth, strokeOpacity, context);
        }

        Expression CreateStroke(string stroke, string strokeWidth, string opacity, SvgRendererContext context)
        {
            if (stroke == null) return Expression.PropertyOrField(context.State, "Outlining");
            if (stroke == "none") return null;
            var width = strokeWidth == null ? 1 : ((SvgLength)strokeWidth);
            var color = ((SvgColor)stroke).Color;
            if (opacity != null)
            {
                var opacityValue = ((SvgLength)opacity).Value;
                color = Color.FromArgb((int)(opacityValue * 255), color);
            }
            var pen = new Pen(color, width.Value);
            disposableResources.Add(pen);
            return Expression.Constant(pen);
        }

        void CreateDrawTransform(SvgElement element, Matrix transform, SvgRendererContext context)
        {
            var localTransform = CreateTransform(element, transform);
            var scale = Expression.PropertyOrField(context.State, "Scale");
            var translation = Expression.PropertyOrField(context.State, "Translation");
            var offsetX = Expression.PropertyOrField(translation, "X");
            var offsetY = Expression.PropertyOrField(translation, "Y");
            context.Expressions.Add(Expression.Call(context.Graphics, "TranslateTransform", null, offsetX, offsetY));
            context.Expressions.Add(Expression.Call(context.Graphics, "ScaleTransform", null, scale, scale));
            context.Expressions.Add(Expression.Call(context.Graphics, "MultiplyTransform", null, localTransform));
        }

        void CreateResetTransform(SvgRendererContext context)
        {
            context.Expressions.Add(Expression.Call(context.Graphics, "ResetTransform", null));
        }

        void CreateDrawRectangle(SvgElement element, Matrix transform, SvgRendererContext context)
        {
            var style = CreateStyle(element, context);
            if (style != null)
            {
                var x = CreateFloat(element, "x");
                var y = CreateFloat(element, "y");
                var width = CreateFloat(element, "width");
                var height = CreateFloat(element, "height");
                CreateDrawTransform(element, transform, context);
                if (style.Fill != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "FillRectangle", null, style.Fill, x, y, width, height));
                }
                if (style.Stroke != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "DrawRectangle", null, style.Stroke, x, y, width, height));
                }
                CreateResetTransform(context);
            }
        }

        void CreateDrawCircle(SvgElement element, Matrix transform, SvgRendererContext context)
        {
            var style = CreateStyle(element, context);
            if (style != null)
            {
                var cx = CreateFloat(element, "cx");
                var cy = CreateFloat(element, "cy");
                var r = CreateFloat(element, "r");
                var x = Expression.Subtract(cx, r);
                var y = Expression.Subtract(cy, r);
                var d = Expression.Multiply(Expression.Constant(2f), r);
                CreateDrawTransform(element, transform, context);
                if (style.Fill != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "FillEllipse", null, style.Fill, x, y, d, d));
                }
                if (style.Stroke != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "DrawEllipse", null, style.Stroke, x, y, d, d));
                }
                CreateResetTransform(context);
            }
        }

        void CreateDrawEllipse(SvgElement element, Matrix transform, SvgRendererContext context)
        {
            var style = CreateStyle(element, context);
            if (style != null)
            {
                var cx = CreateFloat(element, "cx");
                var cy = CreateFloat(element, "cy");
                var rx = CreateFloat(element, "rx");
                var ry = CreateFloat(element, "ry");
                var x = Expression.Subtract(cx, rx);
                var y = Expression.Subtract(cy, ry);
                var dx = Expression.Multiply(Expression.Constant(2f), rx);
                var dy = Expression.Multiply(Expression.Constant(2f), ry);
                CreateDrawTransform(element, transform, context);
                if (style.Fill != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "FillEllipse", null, style.Fill, x, y, dx, dy));
                }
                if (style.Stroke != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "DrawEllipse", null, style.Stroke, x, y, dx, dy));
                }
                CreateResetTransform(context);
            }

        }

        static void AddLines(GraphicsPath path, List<PointF> points)
        {
            if (points.Count > 1)
            {
                path.AddLines(points.ToArray());
            }
            points.Clear();
        }

        static void AddBeziers(GraphicsPath path, List<PointF> points)
        {
            if (points.Count > 3)
            {
                path.AddBeziers(points.ToArray());
            }
            points.Clear();
        }

        static void StartLine(List<PointF> bezierPoints, List<PointF> linePoints, PathSeg segment, ref PointF point)
        {
            if (bezierPoints.Count > 0)
            {
                linePoints.Add(bezierPoints[bezierPoints.Count - 1]);
                point = segment.Abs ? default(PointF) : linePoints[0];
            }
        }

        static void AddBezierData(List<PointF> points, PathSeg segment)
        {
            var data = segment.Data;
            var offset = !segment.Abs && points.Count > 0 ? points[points.Count - 1] : PointF.Empty;
            for (int i = 0; i < data.Length / 2; i++)
            {
                points.Add(new PointF(
                    data[i * 2 + 0] + offset.X,
                    data[i * 2 + 1] + offset.Y));
            }
        }

        static void AddPathData(GraphicsPath path, SvgPath pathData)
        {
            var bezierPoints = new List<PointF>(pathData.Count);
            var linePoints = new List<PointF>(pathData.Count);
            for (int i = 0; i < pathData.Count; i++)
            {
                PointF point;
                var segment = pathData[i];
                if (linePoints.Count > 0 && (segment.Type == SvgPathSegType.SVG_SEGTYPE_CURVETO || !segment.Abs))
                {
                    point = linePoints[linePoints.Count - 1];
                }
                else point = default(PointF);
                switch (segment.Type)
                {
                    case SvgPathSegType.SVG_SEGTYPE_CURVETO:
                        AddLines(path, linePoints);
                        if (bezierPoints.Count == 0) bezierPoints.Add(point);
                        AddBezierData(bezierPoints, segment);
                        break;
                    case SvgPathSegType.SVG_SEGTYPE_MOVETO:
                        AddLines(path, linePoints);
                        AddBeziers(path, bezierPoints);
                        path.StartFigure();
                        point.X += segment.Data[0];
                        point.Y += segment.Data[1];
                        linePoints.Add(point);
                        break;
                    case SvgPathSegType.SVG_SEGTYPE_LINETO:
                        StartLine(bezierPoints, linePoints, segment, ref point);
                        AddBeziers(path, bezierPoints);
                        point.X += segment.Data[0];
                        point.Y += segment.Data[1];
                        linePoints.Add(point);
                        break;
                    case SvgPathSegType.SVG_SEGTYPE_HLINETO:
                        StartLine(bezierPoints, linePoints, segment, ref point);
                        AddBeziers(path, bezierPoints);
                        point.X += segment.Data[0];
                        if (segment.Abs) point.Y = linePoints[linePoints.Count - 1].Y;
                        linePoints.Add(point);
                        break;
                    case SvgPathSegType.SVG_SEGTYPE_VLINETO:
                        StartLine(bezierPoints, linePoints, segment, ref point);
                        AddBeziers(path, bezierPoints);
                        if (segment.Abs) point.X = linePoints[linePoints.Count - 1].X;
                        point.Y += segment.Data[0];
                        linePoints.Add(point);
                        break;
                    case SvgPathSegType.SVG_SEGTYPE_CLOSEPATH:
                        AddLines(path, linePoints);
                        AddBeziers(path, bezierPoints);
                        path.CloseFigure();
                        break;
                }
            }

            AddLines(path, linePoints);
            AddBeziers(path, bezierPoints);
        }

        Expression CreatePath(SvgElement element, string attribute)
        {
            var pathData = ParsePath(element, attribute);
            var path = new GraphicsPath();
            disposableResources.Add(path);
            AddPathData(path, pathData);
            return Expression.Constant(path);
        }

        void CreateDrawPath(SvgElement element, Matrix transform, SvgRendererContext context)
        {
            var style = CreateStyle(element, context);
            if (style != null)
            {
                var path = CreatePath(element, "d");
                CreateDrawTransform(element, transform, context);
                if (style.Fill != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "FillPath", null, style.Fill, path));
                }
                if (style.Stroke != null)
                {
                    context.Expressions.Add(Expression.Call(context.Graphics, "DrawPath", null, style.Stroke, path));
                }
                CreateResetTransform(context);
            }
        }

        Color ParseStopColor(SvgStopElement stop)
        {
            var color = new SvgColor((string)stop.Style.Get("stop-color"));
            var opacity = float.Parse((string)stop.Style.Get("stop-opacity"), CultureInfo.InvariantCulture);
            return Color.FromArgb((int)(255 * opacity), color.Color);
        }

        void CreateLinearGradient(SvgElement element, SvgRendererContext context)
        {
            var linearGradient = element as SvgLinearGradientElement;
            if (linearGradient != null)
            {
                var color1 = default(Color);
                var color2 = default(Color);
                LinearGradientBrush gradient;
                if (linearGradient.Children.Count == 2)
                {
                    var stop1 = linearGradient.Children[0] as SvgStopElement;
                    var stop2 = linearGradient.Children[1] as SvgStopElement;
                    if (stop1 != null && stop2 != null)
                    {
                        color1 = ParseStopColor(stop1);
                        color2 = ParseStopColor(stop2);
                    }
                }
                else
                {
                    Brush referenceGradient;
                    LinearGradientBrush referenceLinearGradient;
                    var href = new SvgXRef(linearGradient).Href;
                    if (href != null && context.Gradients.TryGetValue(href, out referenceGradient) &&
                       (referenceLinearGradient = referenceGradient as LinearGradientBrush) != null)
                    {
                        color1 = referenceLinearGradient.LinearColors[0];
                        color2 = referenceLinearGradient.LinearColors[1];
                    }
                }

                var gradientTransform = ParseTransform(linearGradient, null, "gradientTransform");
                var points = new[]
                {
                    ParsePoint(linearGradient, "x1", "y1").GetValueOrDefault(PointF.Empty),
                    ParsePoint(linearGradient, "x2", "y2").GetValueOrDefault(new PointF(1, 1))
                };
                gradientTransform.TransformPoints(points);
                gradient = new LinearGradientBrush(points[0], points[1], color1, color2);
                context.Gradients.Add(new SvgUriReference(linearGradient).Href, gradient);
                disposableResources.Add(gradient);
            }
        }

        void CreateDrawDefinitions(SvgElement element, SvgRendererContext context)
        {
            foreach (SvgElement child in element.Children)
            {
                switch (child.Name)
                {
                    case "linearGradient": CreateLinearGradient(child, context); break;
                    default: continue;
                }
            }
        }

        void CreateDrawChildren(SvgElement element, Matrix transform, SvgRendererContext context)
        {
            foreach (SvgElement child in element.Children)
            {
                CreateDrawBody(child, transform, context);
            }
        }

        void CreateDrawBody(SvgElement element, Matrix transform, SvgRendererContext context)
        {
            switch (element.Name)
            {
                case "rect": CreateDrawRectangle(element, transform, context); break;
                case "circle": CreateDrawCircle(element, transform, context); break;
                case "ellipse": CreateDrawEllipse(element, transform, context); break;
                case "path": CreateDrawPath(element, transform, context); break;
                case "svg": CreateDrawChildren(element, transform, context); break;
                case "defs": CreateDrawDefinitions(element, context); break;
                case "g":
                    var localTransform = ParseTransform(element, transform);
                    CreateDrawChildren(element, localTransform, context);
                    break;
            }
        }

        SvgRenderer CreateRenderer(SvgElement element)
        {
            var transform = new Matrix();
            var context = new SvgRendererContext();
            CreateDrawBody(element, transform, context);
            var body = context.Expressions.Count > 0 ? (Expression)Expression.Block(context.Expressions) : Expression.Empty();
            var renderer = Expression.Lambda<SvgRenderer>(body, context.State, context.Graphics);
            return renderer.Compile();
        }

        public SvgRenderer GetIconRenderer(GraphNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var icon = node.Icon;
            SvgRenderer renderer = null;
            Stack<string> fallbackIcons = null;
            while (icon != null)
            {
                if (TryGetIconRenderer(icon, out renderer)) break;
                else
                {
                    if (fallbackIcons == null) fallbackIcons = new Stack<string>();
                    fallbackIcons.Push(icon.Name);
                    icon = icon.GetDefaultIcon();
                }
            }

            while (fallbackIcons != null && fallbackIcons.Count > 0)
            {
                rendererCache.Add(fallbackIcons.Pop(), renderer);
            }

            if (renderer == null && node.Icon.IsIncludeElement)
            {
                TryGetIconRenderer(ElementIcon.Include, out renderer);
            }
            return renderer;
        }

        public SvgRenderer GetIconRenderer(ElementCategory category)
        {
            SvgRenderer renderer;
            var categoryIcon = ElementIcon.FromElementCategory(category);
            if (!TryGetIconRenderer(categoryIcon, out renderer))
            {
                rendererCache.Add(categoryIcon.Name, renderer);
            }

            return renderer;
        }

        bool TryGetIconRenderer(ElementIcon icon, out SvgRenderer renderer)
        {
            if (icon == null)
            {
                throw new ArgumentNullException("icon");
            }

            if (!rendererCache.TryGetValue(icon.Name, out renderer))
            {
                using (var iconStream = icon.GetStream())
                {
                    if (iconStream == null) return false;
                    var svgDocument = new XmlDocument();
                    svgDocument.XmlResolver = null;
                    svgDocument.Load(iconStream);
                    var element = SvgFactory.LoadFromXML(svgDocument, null);
                    renderer = CreateRenderer(element);
                    rendererCache.Add(icon.Name, renderer);
                }
            }

            return true;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposableResources.RemoveAll(disposable =>
                {
                    disposable.Dispose();
                    return true;
                });
                disposed = true;
            }
        }
    }
}
