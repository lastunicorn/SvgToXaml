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

namespace DustInTheWind.SvgToXaml.SvgModel;

public class SvgElement
{
    public SvgContainer Parent { get; set; }

    // Core Attributes

    public string Id { get; set; }

    public string[] ClassNames { get; set; }

    public SvgStyleDeclarationCollection Style { get; set; }

    // Inherited Attributes

    public SvgPaint Fill { get; set; }

    public FillRule? FillRule { get; set; }

    public SvgPaint Stroke { get; set; }

    public double? StrokeWidth { get; set; }

    public StrokeLineCap? StrokeLineCap { get; set; }

    public StrokeLineJoin? StrokeLineJoin { get; set; }

    public double? StrokeDashOffset { get; set; }

    public double? StrokeMiterLimit { get; set; }

    //

    public SvgTransformList Transforms { get; } = new();

    public double? Opacity { get; set; }

    public SvgClipPathReference ClipPath { get; set; }

    public Svg GetParentSvg()
    {
        SvgContainer parent = Parent;

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

    public SvgPaint ComputeFill()
    {
        string rawValue = GetStyleValueFromClasses("fill");

        if (rawValue != null)
            return rawValue;

        SvgStyleDeclaration styleDeclaration = Style?["fill"];

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return Fill;
    }

    public FillRule? ComputeFillRule()
    {
        string rawValue = GetStyleValueFromClasses("fill-rule");

        if (rawValue != null)
            return (FillRule)Enum.Parse(typeof(FillRule), rawValue, true);

        SvgStyleDeclaration styleDeclaration = Style?["fill-rule"];

        if (styleDeclaration != null)
            return (FillRule)Enum.Parse(typeof(FillRule), styleDeclaration.Value, true);

        return FillRule;
    }

    public SvgPaint ComputeStroke()
    {
        string rawValue = GetStyleValueFromClasses("stroke");

        if (rawValue != null)
            return rawValue;

        SvgStyleDeclaration styleDeclaration = Style?["stroke"];

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return Stroke;
    }

    public SvgLength? ComputeStrokeWidth()
    {
        string rawValue = GetStyleValueFromClasses("stroke-width");

        if (rawValue != null)
        {
            SvgLength svgLength = rawValue;
            return svgLength.Value;
        }

        SvgStyleDeclaration styleDeclaration = Style?["stroke-width"];

        if (styleDeclaration != null)
        {
            SvgLength length = styleDeclaration.Value.Trim();
            return length.Value;
        }

        return StrokeWidth;
    }

    public StrokeLineCap? ComputeStrokeLineCap()
    {
        string rawValue = GetStyleValueFromClasses("stroke-linecap");

        if (rawValue != null)
            return (StrokeLineCap)Enum.Parse(typeof(StrokeLineCap), rawValue, true);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-linecap"];

        if (styleDeclaration != null)
            return (StrokeLineCap)Enum.Parse(typeof(StrokeLineCap), styleDeclaration.Value, true);

        return StrokeLineCap;
    }

    public StrokeLineJoin? ComputeStrokeLineJoin()
    {
        string rawValue = GetStyleValueFromClasses("stroke-linejoin");

        if (rawValue != null)
            return (StrokeLineJoin)Enum.Parse(typeof(StrokeLineJoin), rawValue, true);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-linejoin"];

        if (styleDeclaration != null)
            return (StrokeLineJoin)Enum.Parse(typeof(StrokeLineJoin), styleDeclaration.Value, true);

        return StrokeLineJoin;
    }

    public double? ComputeStrokeDashOffset()
    {
        string rawValue = GetStyleValueFromClasses("stroke-dashoffset");

        if (rawValue != null)
            return double.Parse(rawValue, CultureInfo.InvariantCulture);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-dashoffset"];

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        return StrokeDashOffset;
    }

    public double? ComputeStrokeMiterLimit()
    {
        string rawValue = GetStyleValueFromClasses("stroke-miterlimit");

        if (rawValue != null)
            return double.Parse(rawValue, CultureInfo.InvariantCulture);

        SvgStyleDeclaration styleDeclaration = Style?["stroke-miterlimit"];

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        return StrokeMiterLimit;
    }

    public double? ComputeOpacity()
    {
        return Opacity;
    }

    protected string GetStyleValueFromClasses(string name)
    {
        IEnumerable<SvgStyleRuleSet> applicableStyleRuleSets = GetApplicableStyleRuleSets();

        if (applicableStyleRuleSets == null)
            return null;

        foreach (SvgStyleRuleSet styleRuleSet in applicableStyleRuleSets)
        {
            SvgStyleDeclaration styleDeclaration = styleRuleSet.Declarations?[name];

            if (styleDeclaration != null)
                return styleDeclaration.Value;
        }

        return null;
    }

    private IEnumerable<SvgStyleRuleSet> GetApplicableStyleRuleSets()
    {
        if (ClassNames == null)
            return null;

        Svg parentSvg = GetParentSvg();
        IEnumerable<SvgStyleRuleSet> styleRuleSets = parentSvg?.GetAllStyleRuleSets();

        return styleRuleSets?
            .Where(x => ClassNames.Contains(x.Selector))
            .Where(x => x.Declarations != null)
            .Reverse();
    }
}