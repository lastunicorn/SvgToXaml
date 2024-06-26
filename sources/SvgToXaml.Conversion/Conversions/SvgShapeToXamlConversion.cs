﻿// SvgToXaml
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

using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal abstract class SvgShapeToXamlConversion<TSvg, TXaml> : ToXamlConversion<TSvg, TXaml>
    where TSvg : SvgShape
    where TXaml : Shape
{
    protected SvgShapeToXamlConversion(TSvg svgElement, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgElement, conversionContext, referrer)
    {
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        SetFill();
        SetStroke();
        SetStrokeThickness();
        SetStrokeLineCap();
        SetStrokeLineJoin();
        SetStrokeDashArray();
        SetStrokeDashOffset();
        SetStrokeMiterLimit();
    }

    private void SetFill()
    {
        Paint fill = ShadowTree
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
            AlphaValue? fillOpacity = SvgElement.ComputeFillOpacity();

            Color color = fillOpacity == null
                ? fill.Color.ToColor()
                : fill.Color.ToColor(fillOpacity.Value.NumberValue);

            XamlElement.Fill = new SolidColorBrush(color);
        }
        else if (fill.Url is { IsEmpty: false })
        {
            SvgElement referencedElement = SvgElement.GetParentSvg().FindChild(fill.Url.ReferencedId);

            if (referencedElement is SvgLinearGradient svgLinearGradient)
                XamlElement.Fill = svgLinearGradient.Transform();
            else if (referencedElement is SvgRadialGradient svgRadialGradient)
                XamlElement.Fill = svgRadialGradient.Transform();

            if (XamlElement.Fill != null)
            {
                AlphaValue? fillOpacity = SvgElement.ComputeFillOpacity();

                if (fillOpacity != null)
                    XamlElement.Fill.Opacity = fillOpacity.Value.NumberValue;
            }
        }
    }

    private void SetStroke()
    {
        Paint stroke = ShadowTree
            .Select(x => x.ComputeStroke())
            .FirstOrDefault(x => x != null);

        if (stroke == null || stroke.IsNone)
        {
        }
        else if (stroke.Color is { IsEmpty: false })
        {
            AlphaValue? strokeOpacity = SvgElement.ComputeStrokeOpacity();

            Color color = strokeOpacity == null
                ? stroke.Color.ToColor()
                : stroke.Color.ToColor(strokeOpacity.Value.NumberValue);

            XamlElement.Stroke = new SolidColorBrush(color);
        }
        else if (stroke.Url is { IsEmpty: false })
        {
            SvgElement referencedElement = SvgElement.GetParentSvg().FindChild(stroke.Url.ReferencedId);

            if (referencedElement is SvgLinearGradient svgLinearGradient)
                XamlElement.Stroke = svgLinearGradient.Transform();
            else if (referencedElement is SvgRadialGradient svgRadialGradient)
                XamlElement.Stroke = svgRadialGradient.Transform();

            if (XamlElement.Fill != null)
            {
                AlphaValue? strokeOpacity = SvgElement.ComputeStrokeOpacity();

                if (strokeOpacity != null)
                    XamlElement.Stroke.Opacity = strokeOpacity.Value.NumberValue;
            }
        }
    }

    private void SetStrokeThickness()
    {
        LengthPercentage? strokeWidth = ShadowTree
            .Select(x => x.ComputeStrokeWidth())
            .FirstOrDefault(x => x != null);

        if (strokeWidth != null)
            XamlElement.StrokeThickness = strokeWidth.Value.Length?.Value ?? 0;
    }

    private void SetStrokeLineCap()
    {
        StrokeLineCap? strokeLineCap = ShadowTree
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

    private void SetStrokeLineJoin()
    {
        StrokeLineJoin? strokeLineJoin = ShadowTree
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

    /// <summary>
    /// This method needs the stroke thickness to be already calculated.
    /// Call it only after the stroke thickness was calculated.
    /// </summary>
    private void SetStrokeDashArray()
    {
        DashArray strokeDashArray = ShadowTree
            .Select(x => x.ComputeStrokeDashArray())
            .FirstOrDefault(x => x != null);

        if (strokeDashArray is { IsEmpty: false })
        {
            double strokeThickness = XamlElement.StrokeThickness;

            IEnumerable<double> values = strokeDashArray.Select(x =>
            {
                double computedValue = x.ComputeValue();

                if (x.Length != null)
                    return computedValue / strokeThickness;

                if (x.Percentage != null)
                {
                    // This value is wrong. It should be calculated as percentage from the root Canvas.
                    return computedValue / strokeThickness;
                }

                return computedValue;
            });

            XamlElement.StrokeDashArray = new DoubleCollection(values);
        }
    }

    /// <summary>
    /// This method needs the stroke thickness to be already calculated.
    /// Call it only after the stroke thickness was calculated.
    /// </summary>
    private void SetStrokeDashOffset()
    {
        LengthPercentage? strokeDashOffset = ShadowTree
            .Select(x => x.ComputeStrokeDashOffset())
            .FirstOrDefault(x => x != null);

        if (strokeDashOffset != null)
        {
            double strokeThickness = XamlElement.StrokeThickness;
            double computedValue = strokeDashOffset.Value.ComputeValue();

            if (strokeDashOffset.Value.Length != null)
            {
                XamlElement.StrokeDashOffset = computedValue / strokeThickness;
            }
            else if (strokeDashOffset.Value.Percentage != null)
            {
                // This value is wrong. It should be calculated as percentage from the root Canvas.
                XamlElement.StrokeDashOffset = computedValue / strokeThickness;
            }
            else
            {
                XamlElement.StrokeDashOffset = strokeDashOffset.Value.ComputeValue();
            }
        }
    }

    private void SetStrokeMiterLimit()
    {
        double? strokeMiterLimit = ShadowTree
            .Select(x => x.ComputeStrokeMiterLimit())
            .FirstOrDefault(x => x != null);

        if (strokeMiterLimit != null)
            XamlElement.StrokeMiterLimit = strokeMiterLimit.Value;
    }
}