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

using DustInTheWind.SvgToXaml.SvgSerialization;

namespace DustInTheWind.SvgToXaml.SvgModel.Conversion;

internal static class ElementExtensions
{
    public static void PopulateFrom(this SvgElement svgElement, Element element)
    {
        if (element == null)
            return;

        svgElement.Id = element.Id;

        svgElement.Fill = element.Fill;

        svgElement.FillRule = element.FillRuleSpecified
            ? Convert(element.FillRule)
            : null;

        svgElement.Stroke = element.Stroke;

        svgElement.StrokeWidth = element.StrokeWidthSpecified
            ? element.StrokeWidth
            : null;

        svgElement.StrokeLineJoin = element.StrokeLineJoinSpecified
            ? Convert(element.StrokeLineJoin)
            : null;

        svgElement.StrokeLineCap = element.StrokeLineCapSpecified
            ? Convert(element.StrokeLineCap)
            : null;

        svgElement.StrokeDashOffset = element.StrokeDashOffsetSpecified
            ? element.StrokeDashOffset
            : null;

        svgElement.StrokeMiterLimit = element.StrokeMiterLimitSpecified
            ? element.StrokeMiterLimit
            : null;

        svgElement.Style = element.Style;

        svgElement.ClassNames = element.Class?.Split(new[] { ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (element.Transform != null)
            svgElement.Transforms.ParseAndAdd(element.Transform);

        svgElement.Opacity = element.OpacitySpecified
            ? element.Opacity
            : null;

        if (element.ClipPath != null)
            svgElement.ClipPath = new SvgClipPathReference(element.ClipPath);
    }

    private static FillRule Convert(SvgSerialization.FillRule value)
    {
        return value switch
        {
            SvgSerialization.FillRule.NonZero => SvgModel.FillRule.Nonzero,
            SvgSerialization.FillRule.EvenOdd => SvgModel.FillRule.EvenOdd,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    private static StrokeLineJoin Convert(SvgSerialization.StrokeLineJoin value)
    {
        return value switch
        {
            SvgSerialization.StrokeLineJoin.Miter => SvgModel.StrokeLineJoin.Miter,
            SvgSerialization.StrokeLineJoin.MiterClip => SvgModel.StrokeLineJoin.MiterClip,
            SvgSerialization.StrokeLineJoin.Round => SvgModel.StrokeLineJoin.Round,
            SvgSerialization.StrokeLineJoin.Bevel => SvgModel.StrokeLineJoin.Bevel,
            SvgSerialization.StrokeLineJoin.Arcs => SvgModel.StrokeLineJoin.Arcs,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    private static StrokeLineCap Convert(SvgSerialization.StrokeLineCap value)
    {
        return value switch
        {
            SvgSerialization.StrokeLineCap.Butt => SvgModel.StrokeLineCap.Butt,
            SvgSerialization.StrokeLineCap.Square => SvgModel.StrokeLineCap.Square,
            SvgSerialization.StrokeLineCap.Round => SvgModel.StrokeLineCap.Round,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}