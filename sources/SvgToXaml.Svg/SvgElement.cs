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

using System.Globalization;
using DustInTheWind.SvgToXaml.Svg.Serialization;

namespace DustInTheWind.SvgToXaml.Svg;

public class SvgElement
{
    public string Id { get; set; }

    public SvgGroup Parent { get; set; }

    public SvgPaint Fill { get; set; }

    public FillRule? FillRule { get; set; }

    public string Stroke { get; set; }

    public double? StrokeWidth { get; set; }

    public StrokeLineCap? StrokeLineCap { get; set; }

    public StrokeLineJoin? StrokeLineJoin { get; set; }

    public double? StrokeDashOffset { get; set; }

    public double? StrokeMiterLimit { get; set; }

    public SvgStyleDeclarationCollection Style { get; set; }

    public string[] ClassNames { get; set; }

    public SvgTransformList Transforms { get; set; }

    public double? Opacity { get; set; }

    public SvgElement()
    {
    }

    protected internal SvgElement(Element element)
    {
        if (element == null) throw new ArgumentNullException(nameof(element));

        Id = element.Id;

        Fill = element.Fill;

        FillRule = element.FillRule == null
            ? null
            : (FillRule)Enum.Parse(typeof(FillRule), element.FillRule, true);

        Stroke = element.Stroke;

        StrokeWidth = element.StrokeWidthSpecified
            ? element.StrokeWidth
            : null;

        StrokeLineJoin = element.StrokeLineJoin == null
            ? null
            : (StrokeLineJoin)Enum.Parse(typeof(StrokeLineJoin), element.StrokeLineJoin, true);

        StrokeLineCap = element.StrokeLineCap == null
            ? null
            : (StrokeLineCap)Enum.Parse(typeof(StrokeLineCap), element.StrokeLineCap, true);

        StrokeDashOffset = element.StrokeDashOffsetSpecified
            ? element.StrokeDashOffset
            : null;

        StrokeMiterLimit = element.StrokeMiterLimitSpecified
            ? element.StrokeMiterLimit
            : null;

        Style = element.Style;

        ClassNames = element.Class?.Split(new[] { ' ' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        Transforms = new SvgTransformList();

        if (element.Transform != null)
            Transforms.ParseAndAdd(element.Transform);

        Opacity = element.OpacitySpecified
            ? element.Opacity
            : null;
    }

    public Svg GetParentSvg()
    {
        SvgGroup parent = Parent;

        while (parent != null)
        {
            if (parent is Svg svg)
                return svg;

            parent = parent.Parent;
        }

        return null;
    }

    public IEnumerable<SvgElement> EnumerateAncestors()
    {
        SvgElement ancestor = Parent;

        while (ancestor != null)
        {
            yield return ancestor;
            ancestor = ancestor.Parent;
        }
    }

    private IEnumerable<SvgStyleRuleSet> GetApplicableClasses()
    {
        if (ClassNames == null)
            return null;

        Svg parentSvg = GetParentSvg();
        IEnumerable<SvgStyleRuleSet> cssClasses = parentSvg?.GetAllCssClasses();

        return cssClasses?
            .Where(x => ClassNames.Contains(x.Selector))
            .Where(x => x.Declarations != null)
            .Reverse();
    }

    private string GetStyleValueFromClasses(string name)
    {
        IEnumerable<SvgStyleRuleSet> applicableClasses = GetApplicableClasses();

        if (applicableClasses == null)
            return null;

        foreach (SvgStyleRuleSet svgStyleClass in applicableClasses)
        {
            SvgStyleDeclaration styleItem1 = svgStyleClass.Declarations?[name];

            if (styleItem1 != null)
                return styleItem1.Value;
        }

        return null;
    }

    public SvgPaint CalculateFill()
    {
        string rawValue = GetStyleValueFromClasses("fill");

        if (rawValue != null)
            return rawValue;

        SvgStyleDeclaration styleDeclaration = Style?["fill"];

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return Fill;
    }

    public FillRule? CalculateFillRule()
    {
        string rawValue = GetStyleValueFromClasses("fill-rule");

        if (rawValue != null)
            return (FillRule)Enum.Parse(typeof(FillRule), rawValue, true);

        SvgStyleDeclaration styleDeclaration = Style?["fill-rule"];

        if (styleDeclaration != null)
            return (FillRule)Enum.Parse(typeof(FillRule), styleDeclaration.Value, true);

        return FillRule;
    }

    public string CalculateStroke()
    {
        string rawValue = GetStyleValueFromClasses("stroke");

        if (rawValue != null)
            return rawValue;

        SvgStyleDeclaration styleDeclaration = Style?["stroke"];

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return Stroke;
    }

    public double? CalculateStrokeWidth()
    {
        string rawValue = GetStyleValueFromClasses("stroke-width");

        if (rawValue != null)
            return double.Parse(rawValue, CultureInfo.InvariantCulture);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-width"];

        if (styleDeclaration != null)
        {
            string valueAsString = styleDeclaration.Value.Trim();

            if (valueAsString.EndsWith("px"))
                valueAsString = valueAsString[..^2];

            return double.Parse(valueAsString, CultureInfo.InvariantCulture);
        }

        return StrokeWidth;
    }

    public StrokeLineCap? CalculateStrokeLineCap()
    {
        string rawValue = GetStyleValueFromClasses("stroke-linecap");

        if (rawValue != null)
            return (StrokeLineCap)Enum.Parse(typeof(StrokeLineCap), rawValue, true);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-linecap"];

        if (styleDeclaration != null)
            return (StrokeLineCap)Enum.Parse(typeof(StrokeLineCap), styleDeclaration.Value, true);

        return StrokeLineCap;
    }

    public StrokeLineJoin? CalculateStrokeLineJoin()
    {
        string rawValue = GetStyleValueFromClasses("stroke-linejoin");

        if (rawValue != null)
            return (StrokeLineJoin)Enum.Parse(typeof(StrokeLineJoin), rawValue, true);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-linejoin"];

        if (styleDeclaration != null)
            return (StrokeLineJoin)Enum.Parse(typeof(StrokeLineJoin), styleDeclaration.Value, true);

        return StrokeLineJoin;
    }

    public double? CalculateStrokeDashOffset()
    {
        string rawValue = GetStyleValueFromClasses("stroke-dashoffset");

        if (rawValue != null)
            return double.Parse(rawValue, CultureInfo.InvariantCulture);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-dashoffset"];

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        return StrokeDashOffset;
    }

    public double? CalculateStrokeMiterLimit()
    {
        string rawValue = GetStyleValueFromClasses("stroke-miterlimit");

        if (rawValue != null)
            return double.Parse(rawValue, CultureInfo.InvariantCulture);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-miterlimit"];

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        return StrokeMiterLimit;
    }

    public double? CalculateOpacity()
    {
        return Opacity;
    }
}