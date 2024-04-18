// SvgToXaml
// Copyright (C) 2022-2024 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgToXaml.Conversion;

internal abstract class SvgShapeToXamlConversion<TSvg, TXaml> : SvgElementToXamlConversion<TSvg, TXaml>
    where TSvg : SvgShape
    where TXaml : Shape
{
    protected SvgShapeToXamlConversion(TSvg svgElement, SvgElement referrer = null)
        : base(svgElement, referrer)
    {
    }

    protected override void InheritPropertiesFrom(IEnumerable<SvgElement> svgElements)
    {
        base.InheritPropertiesFrom(svgElements);

        SetFill(svgElements);
        SetStroke(svgElements);
        SetStrokeThickness(svgElements);
        SetStrokeLineCap(svgElements);
        SetStrokeLineJoin(svgElements);
        SetStrokeDashOffset(svgElements);
        SetStrokeMiterLimit(svgElements);
    }

    private void SetFill(IEnumerable<SvgElement> svgElements)
    {
        SvgPaint fill = svgElements
            .Select(x => x.ComputeFill())
            .FirstOrDefault(x => x != null);

        if (fill == null)
        {
            XamlElement.Fill = Brushes.Black;
        }
        else if (fill.IsNone)
        {
        }
        else if (fill.Color is { IsEmpty: false })
        {
            double? fillOpacity = SvgElement.ComputeFillOpacity();

            Color color = fillOpacity == null
                ? fill.Color.ToColor()
                : fill.Color.ToColor(fillOpacity.Value);

            XamlElement.Fill = new SolidColorBrush(color);
        }
        else if (fill.Url is { IsEmpty: false })
        {
            SvgElement referencedElement = SvgElement.GetParentSvg().FindChild(fill.Url.ReferencedId);

            if (referencedElement is SvgLinearGradient svgLinearGradient)
                XamlElement.Fill = ConvertLinearGradient(svgLinearGradient);
            else if (referencedElement is SvgRadialGradient svgRadialGradient) 
                XamlElement.Fill = ConvertRadialGradient(svgRadialGradient);

            if (XamlElement.Fill != null)
            {
                double? fillOpacity = SvgElement.ComputeFillOpacity();

                if(fillOpacity != null)
                    XamlElement.Fill.Opacity = fillOpacity.Value;
            }
        }
    }

    private static LinearGradientBrush ConvertLinearGradient(SvgLinearGradient svgLinearGradient)
    {
        IEnumerable<GradientStop> gradientStops = svgLinearGradient.ComputeStops()
            .Select(Transform)
            .ToList();

        GradientStopCollection gradientStopCollection = new(gradientStops);
        LinearGradientBrush linearGradientBrush = new(gradientStopCollection);

        double x1 = svgLinearGradient.ComputeX1() ?? SvgLength.Zero;
        double x2 = svgLinearGradient.ComputeX2() ?? SvgLength.Zero;
        double y1 = svgLinearGradient.ComputeY1() ?? new SvgLength(1);
        double y2 = svgLinearGradient.ComputeY2() ?? new SvgLength(1);

        linearGradientBrush.StartPoint = new Point(x1, y1);
        linearGradientBrush.EndPoint = new Point(x2, y2);

        if (svgLinearGradient.GradientUnits != null)
        {
            linearGradientBrush.MappingMode = svgLinearGradient.GradientUnits switch
            {
                SvgGradientUnits.ObjectBoundingBox => BrushMappingMode.RelativeToBoundingBox,
                SvgGradientUnits.UserSpaceOnUse => BrushMappingMode.Absolute,
                _ => throw new Exception("Unknown gradient units.")
            };
        }

        if (svgLinearGradient.GradientTransforms.Count > 0)
            linearGradientBrush.Transform = svgLinearGradient.GradientTransforms.ToXaml(linearGradientBrush.Transform);

        return linearGradientBrush;
    }

    private static RadialGradientBrush ConvertRadialGradient(SvgRadialGradient svgRadialGradient)
    {
        IEnumerable<GradientStop> gradientStops = svgRadialGradient.ComputeStops()
            .Select(Transform);

        GradientStopCollection gradientStopCollection = new(gradientStops);
        RadialGradientBrush radialGradientBrush = new(gradientStopCollection);

        double cx = svgRadialGradient.ComputeCenterX() ?? SvgLength.Zero;
        double cy = svgRadialGradient.ComputeCenterY() ?? SvgLength.Zero;
        double r = svgRadialGradient.ComputeRadius() ?? SvgLength.Zero;

        radialGradientBrush.Center = new Point(cx, cy);
        radialGradientBrush.GradientOrigin = new Point(cx, cy);
        radialGradientBrush.RadiusX = r;
        radialGradientBrush.RadiusY = r;

        if (svgRadialGradient.GradientUnits != null)
        {
            radialGradientBrush.MappingMode = svgRadialGradient.GradientUnits switch
            {
                SvgGradientUnits.ObjectBoundingBox => BrushMappingMode.RelativeToBoundingBox,
                SvgGradientUnits.UserSpaceOnUse => BrushMappingMode.Absolute,
                _ => throw new Exception("Unknown gradient units.")
            };
        }

        if (svgRadialGradient.GradientTransforms.Count > 0)
            radialGradientBrush.Transform = svgRadialGradient.GradientTransforms.ToXaml(radialGradientBrush.Transform);

        return radialGradientBrush;
    }

    private static GradientStop Transform(SvgStop svgStop)
    {
        SvgColor stopColor = svgStop.ComputeStopColor();

        if (stopColor.Alpha == null)
        {
            SvgOpacity? stopOpacity = svgStop.ComputeStopOpacity();

            if (stopOpacity.HasValue)
                stopColor = stopColor.SetAlpha(stopOpacity.Value);
        }

        Color color = stopColor.ToColor();
        return new GradientStop(color, svgStop.Offset);
    }

    private void SetStroke(IEnumerable<SvgElement> svgElements)
    {
        SvgPaint stroke = svgElements
            .Select(x => x.ComputeStroke())
            .FirstOrDefault(x => x != null);

        if (stroke == null || stroke.IsNone)
        {
        }
        else if (stroke.Color is { IsEmpty: false })
        {
            double? strokeOpacity = SvgElement.ComputeStrokeOpacity();

            Color color = strokeOpacity == null
                ? stroke.Color.ToColor()
                : stroke.Color.ToColor(strokeOpacity.Value);

            XamlElement.Stroke = new SolidColorBrush(color);
        }
        else if (stroke.Url is { IsEmpty: false })
        {
            SvgElement referencedElement = SvgElement.GetParentSvg().FindChild(stroke.Url.ReferencedId);

            if (referencedElement is SvgLinearGradient svgLinearGradient)
                XamlElement.Stroke = ConvertLinearGradient(svgLinearGradient);
            else if (referencedElement is SvgRadialGradient svgRadialGradient)
                XamlElement.Stroke = ConvertRadialGradient(svgRadialGradient);

            if (XamlElement.Fill != null)
            {
                double? strokeOpacity = SvgElement.ComputeStrokeOpacity();

                if (strokeOpacity != null)
                    XamlElement.Stroke.Opacity = strokeOpacity.Value;
            }
        }
    }

    private void SetStrokeThickness(IEnumerable<SvgElement> svgElements)
    {
        SvgLengthPercentage? strokeWidth = svgElements
            .Select(x => x.ComputeStrokeWidth())
            .FirstOrDefault(x => x != null);

        if (strokeWidth != null)
            XamlElement.StrokeThickness = strokeWidth.Value.Length?.Value ?? 0;
    }

    private void SetStrokeLineCap(IEnumerable<SvgElement> svgElements)
    {
        StrokeLineCap? strokeLineCap = svgElements
            .Select(x => x.ComputeStrokeLineCap())
            .FirstOrDefault(x => x != null);

        if (strokeLineCap != null)
        {
            PenLineCap penLineCap = strokeLineCap switch
            {
                StrokeLineCap.Butt => PenLineCap.Flat,
                StrokeLineCap.Square => PenLineCap.Square,
                StrokeLineCap.Round => PenLineCap.Round,
                _ => throw new ArgumentOutOfRangeException()
            };

            XamlElement.StrokeStartLineCap = penLineCap;
            XamlElement.StrokeEndLineCap = penLineCap;
        }
    }

    private void SetStrokeLineJoin(IEnumerable<SvgElement> svgElements)
    {
        StrokeLineJoin? strokeLineJoin = svgElements
            .Select(x => x.ComputeStrokeLineJoin())
            .FirstOrDefault(x => x != null);

        if (strokeLineJoin != null)
        {
            XamlElement.StrokeLineJoin = strokeLineJoin switch
            {
                StrokeLineJoin.Miter => PenLineJoin.Miter,
                StrokeLineJoin.MiterClip => PenLineJoin.Miter,
                StrokeLineJoin.Round => PenLineJoin.Round,
                StrokeLineJoin.Bevel => PenLineJoin.Bevel,
                StrokeLineJoin.Arcs => PenLineJoin.Miter,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private void SetStrokeDashOffset(IEnumerable<SvgElement> svgElements)
    {
        double? strokeDashOffset = svgElements
            .Select(x => x.ComputeStrokeDashOffset())
            .FirstOrDefault(x => x != null);

        if (strokeDashOffset != null)
            XamlElement.StrokeDashOffset = strokeDashOffset.Value;
    }

    private void SetStrokeMiterLimit(IEnumerable<SvgElement> svgElements)
    {
        double? strokeMiterLimit = svgElements
            .Select(x => x.ComputeStrokeMiterLimit())
            .FirstOrDefault(x => x != null);

        if (strokeMiterLimit != null)
            XamlElement.StrokeMiterLimit = strokeMiterLimit.Value;
    }
}