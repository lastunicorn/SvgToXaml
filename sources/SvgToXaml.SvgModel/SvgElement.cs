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

    public string Language { get; set; }

    public string XmlLanguage { get; set; }

    public ClassNameCollection ClassNames { get; } = new();

    public SvgStyleDeclarationCollection Style { get; set; }

    // Inherited Attributes

    public Paint Fill { get; set; }

    /// <summary>
    /// The fill-rule property indicates the algorithm (or winding rule) which is to be used to
    /// determine what parts of the canvas are included inside the shape. For a simple,
    /// non-intersecting path, it is intuitively clear what region lies "inside"; however, for a
    /// more complex path, such as a path that intersects itself or where one subpath encloses
    /// another, the interpretation of "inside" is not so obvious.
    /// 
    /// The fill-rule property provides two options for how the inside of a shape is determined:
    /// nonzero, evenodd.
    /// </summary>
    public FillRule? FillRule { get; set; }

    public AlphaValue? FillOpacity { get; set; }

    public Paint Stroke { get; set; }

    /// <summary>
    /// Specifies the opacity of the painting operation used to stroke the current object.
    /// This value is either a number between 0 and 1 or a percentage.
    /// Values outside the range [0,1] are not invalid, but are clamped to that range at
    /// parsed-value time.
    /// Default value: 100%
    /// </summary>
    public AlphaValue? StrokeOpacity { get; set; }

    /// <summary>
    /// Specifies the width of the stroke on the current object. A zero value causes no stroke to
    /// be painted. A negative value is invalid.
    /// </summary>
    public LengthPercentage? StrokeWidth { get; set; }

    public StrokeLineCap? StrokeLineCap { get; set; }

    public StrokeLineJoin? StrokeLineJoin { get; set; }

    public double? StrokeMiterLimit { get; set; }

    public LengthPercentage? StrokeDashOffset { get; set; }

    //

    public TransformCollection Transforms { get; } = new();

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

    public Paint ComputeFill()
    {
        SvgStyleDeclaration styleDeclaration = Style?["fill"];

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        string rawValue = GetStyleValueFromClasses("fill");

        if (rawValue != null)
            return rawValue;

        return Fill;
    }

    public AlphaValue? ComputeFillOpacity()
    {
        SvgStyleDeclaration styleDeclaration = Style?["fill-opacity"];

        if (styleDeclaration != null)
        {
            AlphaValue alphaValue = styleDeclaration.Value;
            return alphaValue;
        }

        string rawValue = GetStyleValueFromClasses("fill-opacity");

        if (rawValue != null)
        {
            AlphaValue alphaValue = rawValue;
            return alphaValue;
        }

        return FillOpacity;
    }

    public FillRule? ComputeFillRule()
    {
        SvgStyleDeclaration styleDeclaration = Style?["fill-rule"];

        if (styleDeclaration != null)
            return (FillRule)Enum.Parse(typeof(FillRule), styleDeclaration.Value, true);

        string rawValue = GetStyleValueFromClasses("fill-rule");

        if (rawValue != null)
            return (FillRule)Enum.Parse(typeof(FillRule), rawValue, true);

        return FillRule;
    }

    public Paint ComputeStroke()
    {
        SvgStyleDeclaration styleDeclaration = Style?["stroke"];

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        string rawValue = GetStyleValueFromClasses("stroke");

        if (rawValue != null)
            return rawValue;

        return Stroke;
    }

    public AlphaValue? ComputeStrokeOpacity()
    {
        SvgStyleDeclaration styleDeclaration = Style?["stroke-opacity"];

        if (styleDeclaration != null)
        {
            AlphaValue alphaValue = styleDeclaration.Value;
            return alphaValue;
        }

        string rawValue = GetStyleValueFromClasses("stroke-opacity");

        if (rawValue != null)
        {
            AlphaValue alphaValue = rawValue;
            return alphaValue;
        }

        return StrokeOpacity;
    }

    public LengthPercentage? ComputeStrokeWidth()
    {
        SvgStyleDeclaration styleDeclaration = Style?["stroke-width"];

        if (styleDeclaration != null)
        {
            SvgLength length = styleDeclaration.Value.Trim();
            return length.Value;
        }

        string rawValue = GetStyleValueFromClasses("stroke-width");

        if (rawValue != null)
        {
            SvgLength svgLength = rawValue;
            return svgLength.Value;
        }

        return StrokeWidth;
    }

    public StrokeLineCap? ComputeStrokeLineCap()
    {
        SvgStyleDeclaration styleDeclaration = Style?["stroke-linecap"];

        if (styleDeclaration != null)
            return (StrokeLineCap)Enum.Parse(typeof(StrokeLineCap), styleDeclaration.Value, true);

        string rawValue = GetStyleValueFromClasses("stroke-linecap");

        if (rawValue != null)
            return (StrokeLineCap)Enum.Parse(typeof(StrokeLineCap), rawValue, true);

        return StrokeLineCap;
    }

    public StrokeLineJoin? ComputeStrokeLineJoin()
    {
        SvgStyleDeclaration styleDeclaration = Style?["stroke-linejoin"];

        if (styleDeclaration != null)
            return (StrokeLineJoin)Enum.Parse(typeof(StrokeLineJoin), styleDeclaration.Value, true);

        string rawValue = GetStyleValueFromClasses("stroke-linejoin");

        if (rawValue != null)
            return (StrokeLineJoin)Enum.Parse(typeof(StrokeLineJoin), rawValue, true);

        return StrokeLineJoin;
    }

    public LengthPercentage? ComputeStrokeDashOffset()
    {
        SvgStyleDeclaration styleDeclaration = Style?["stroke-dashoffset"];

        if (styleDeclaration != null)
        {
            LengthPercentage length = styleDeclaration.Value.Trim();
            return length;
        }

        string rawValue = GetStyleValueFromClasses("stroke-dashoffset");

        if (rawValue != null)
        {
            LengthPercentage length = rawValue;
            return length;
        }

        return StrokeDashOffset;
    }

    public double? ComputeStrokeMiterLimit()
    {
        SvgStyleDeclaration styleDeclaration = Style?["stroke-miterlimit"];

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        string rawValue = GetStyleValueFromClasses("stroke-miterlimit");

        if (rawValue != null)
            return double.Parse(rawValue, CultureInfo.InvariantCulture);

        return StrokeMiterLimit;
    }

    public double? ComputeOpacity()
    {
        SvgStyleDeclaration styleDeclaration = Style?["opacity"];

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        string rawValue = GetStyleValueFromClasses("opacity");

        if (rawValue != null)
            return double.Parse(rawValue, CultureInfo.InvariantCulture);

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
        IEnumerable<SvgStyleRuleSet> styleRuleSets = parentSvg?.GetAllStyleRuleSets("text/css");

        return styleRuleSets?
            .Where(x => ClassNames.Contains(x.Selector))
            .Where(x => x.Declarations != null)
            .Reverse();
    }
}