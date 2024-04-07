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

using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;
using FillRule = DustInTheWind.SvgToXaml.SvgModel.FillRule;
using StrokeLineCap = DustInTheWind.SvgToXaml.SvgModel.StrokeLineCap;
using StrokeLineJoin = DustInTheWind.SvgToXaml.SvgModel.StrokeLineJoin;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal static class ElementExtensions
{
    public static void PopulateFrom(this SvgElement svgElement, XmlElement xmlElement)
    {
        if (xmlElement == null)
            return;

        svgElement.Id = xmlElement.Id;

        svgElement.Fill = xmlElement.Fill;

        svgElement.FillRule = xmlElement.FillRuleSpecified
            ? Convert(xmlElement.FillRule)
            : null;

        svgElement.Stroke = xmlElement.Stroke;

        svgElement.StrokeWidth = xmlElement.StrokeWidthSpecified
            ? xmlElement.StrokeWidth
            : null;

        svgElement.StrokeLineJoin = xmlElement.StrokeLineJoinSpecified
            ? Convert(xmlElement.StrokeLineJoin)
            : null;

        svgElement.StrokeLineCap = xmlElement.StrokeLineCapSpecified
            ? Convert(xmlElement.StrokeLineCap)
            : null;

        svgElement.StrokeDashOffset = xmlElement.StrokeDashOffsetSpecified
            ? xmlElement.StrokeDashOffset
            : null;

        svgElement.StrokeMiterLimit = xmlElement.StrokeMiterLimitSpecified
            ? xmlElement.StrokeMiterLimit
            : null;

        svgElement.Style = xmlElement.Style;

        svgElement.ClassNames = xmlElement.Class?.Split(new[] { ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (xmlElement.Transform != null)
            svgElement.Transforms.ParseAndAdd(xmlElement.Transform);

        svgElement.Opacity = xmlElement.OpacitySpecified
            ? xmlElement.Opacity
            : null;

        if (xmlElement.ClipPath != null)
            svgElement.ClipPath = new SvgClipPathReference(xmlElement.ClipPath);
    }

    private static FillRule Convert(SvgSerialization.XmlModels.XmlFillRule value)
    {
        return value switch
        {
            SvgSerialization.XmlModels.XmlFillRule.NonZero => SvgModel.FillRule.Nonzero,
            SvgSerialization.XmlModels.XmlFillRule.EvenOdd => SvgModel.FillRule.EvenOdd,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    private static StrokeLineJoin Convert(SvgSerialization.XmlModels.XmlStrokeLineJoin value)
    {
        return value switch
        {
            SvgSerialization.XmlModels.XmlStrokeLineJoin.Miter => SvgModel.StrokeLineJoin.Miter,
            SvgSerialization.XmlModels.XmlStrokeLineJoin.MiterClip => SvgModel.StrokeLineJoin.MiterClip,
            SvgSerialization.XmlModels.XmlStrokeLineJoin.Round => SvgModel.StrokeLineJoin.Round,
            SvgSerialization.XmlModels.XmlStrokeLineJoin.Bevel => SvgModel.StrokeLineJoin.Bevel,
            SvgSerialization.XmlModels.XmlStrokeLineJoin.Arcs => SvgModel.StrokeLineJoin.Arcs,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    private static StrokeLineCap Convert(SvgSerialization.XmlModels.XmlStrokeLineCap value)
    {
        return value switch
        {
            SvgSerialization.XmlModels.XmlStrokeLineCap.Butt => SvgModel.StrokeLineCap.Butt,
            SvgSerialization.XmlModels.XmlStrokeLineCap.Square => SvgModel.StrokeLineCap.Square,
            SvgSerialization.XmlModels.XmlStrokeLineCap.Round => SvgModel.StrokeLineCap.Round,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}