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

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal abstract class XmlElementToModelConversion<TXml, TSvg> : ToModelConversion<TXml, TSvg>
    where TXml : XmlElement
    where TSvg : SvgElement
{
    protected XmlElementToModelConversion(TXml xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        SvgElement.Id = XmlElement.Id;

        SvgElement.Language = XmlElement.Lang;
        SvgElement.XmlLanguage = XmlElement.XmlLang;

        SvgElement.Fill = XmlElement.Fill;

        SvgElement.FillOpacity = XmlElement.FillOpacity;

        SvgElement.FillRule = XmlElement.FillRuleSpecified
            ? Convert(XmlElement.FillRule)
            : null;

        SvgElement.Stroke = XmlElement.Stroke;

        SvgElement.StrokeOpacity = XmlElement.StrokeOpacity;

        LengthPercentage? strokeWidth = XmlElement.StrokeWidth;

        if (strokeWidth != null)
        {
            bool isNegative = strokeWidth.Value.IsNegative;

            if (isNegative)
            {
                DeserializationContext.Path.AddAttribute("stroke-width");
                string path = DeserializationContext.Path.ToString();
                DeserializationContext.Path.RemoveLast();

                NegativeValueIssue negativeValueIssue = new(path);
                DeserializationContext.Errors.Add(negativeValueIssue);

                SvgElement.StrokeWidth = LengthPercentage.Zero;
            }
            else
            {
                SvgElement.StrokeWidth = strokeWidth;
            }
        }

        SvgElement.StrokeLineJoin = XmlElement.StrokeLineJoinSpecified
            ? Convert(XmlElement.StrokeLineJoin)
            : null;

        SvgElement.StrokeLineCap = XmlElement.StrokeLineCapSpecified
            ? Convert(XmlElement.StrokeLineCap)
            : null;

        SvgElement.StrokeDashOffset = XmlElement.StrokeDashOffset;

        SvgElement.StrokeMiterLimit = XmlElement.StrokeMiterLimitSpecified
            ? XmlElement.StrokeMiterLimit
            : null;

        SvgElement.Style = XmlElement.Style;

        if (XmlElement.Class != null)
            SvgElement.ClassNames.ParseAndAdd(XmlElement.Class);

        if (XmlElement.Transform != null)
            SvgElement.Transforms.ParseAndAdd(XmlElement.Transform);

        SvgElement.Opacity = XmlElement.OpacitySpecified
            ? XmlElement.Opacity
            : null;

        if (XmlElement.ClipPath != null)
            SvgElement.ClipPath = new SvgClipPathReference(XmlElement.ClipPath);
    }

    private static FillRule Convert(XmlFillRule value)
    {
        return value switch
        {
            XmlFillRule.NonZero => FillRule.Nonzero,
            XmlFillRule.EvenOdd => FillRule.EvenOdd,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    private static StrokeLineJoin Convert(XmlStrokeLineJoin value)
    {
        return value switch
        {
            XmlStrokeLineJoin.Miter => StrokeLineJoin.Miter,
            XmlStrokeLineJoin.MiterClip => StrokeLineJoin.MiterClip,
            XmlStrokeLineJoin.Round => StrokeLineJoin.Round,
            XmlStrokeLineJoin.Bevel => StrokeLineJoin.Bevel,
            XmlStrokeLineJoin.Arcs => StrokeLineJoin.Arcs,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    private static StrokeLineCap Convert(XmlStrokeLineCap value)
    {
        return value switch
        {
            XmlStrokeLineCap.Butt => StrokeLineCap.Butt,
            XmlStrokeLineCap.Square => StrokeLineCap.Square,
            XmlStrokeLineCap.Round => StrokeLineCap.Round,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}