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

using System.Globalization;

namespace DustInTheWind.SvgDotnet;

public abstract class SvgElement
{
    private static readonly StyleSelectorType[] StyleSelectorImportance =
    {
        StyleSelectorType.None,
        StyleSelectorType.Element,
        StyleSelectorType.Class,
        StyleSelectorType.Id
    };

    public SvgContainer Parent { get; set; }

    public abstract string ElementName { get; }

    // Core Attributes

    public string Id { get; set; }

    public int? TabIndex { get; set; }

    public string Language { get; set; }

    public Display? Display { get; set; }

    public ClassNameCollection ClassNames { get; } = new();

    public StyleDeclarationCollection Style { get; } = new();

    // Inherited Attributes

    /// <summary>
    /// The color used to paint the interior of the given graphical element.
    /// </summary>
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

    /// <summary>
    /// Specifies the opacity of the painting operation used to paint the fill the current object.
    /// This value is either a number between 0 and 1 or a percentage.
    /// Values outside the range [0,1] are not invalid, but are clamped to that range at
    /// parsed-value time.
    /// Default value: 100%
    /// </summary>
    public AlphaValue? FillOpacity { get; set; }

    /// <summary>
    /// The color used to paint the outline of the given graphical element.
    /// </summary>
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

    public DashArray StrokeDashArray { get; set; }

    public LengthPercentage? StrokeDashOffset { get; set; }

    public LengthPercentage? FontSize { get; set; }

    /// <summary>
    /// Deprecated XML attribute to specify whether white space is preserved in character data.
    /// New content should use the white-space property instead.
    /// </summary>
    [Obsolete("Use the white-space property instead.")]
    public SpacePreservation? Space { get; set; }

    public WhiteSpacePreservation? WhiteSpace { get; set; }

    // Other Attributes

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

    public Svg GetRootSvg()
    {
        Svg rootSvg = null;
        SvgContainer parent = Parent;

        while (parent != null)
        {
            if (parent is Svg svg)
                rootSvg = svg;

            parent = parent.Parent;
        }

        return rootSvg;
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

    public Display? ComputeDisplay()
    {
        StyleDeclaration styleDeclaration = Style?["display"] ?? GetStyleValueFromClasses("display");

        if (styleDeclaration != null)
            return Enum.Parse<Display>(styleDeclaration.Value, true);

        return Display;
    }

    public Paint ComputeFill()
    {
        StyleDeclaration styleDeclaration = Style?["fill"] ?? GetStyleValueFromClasses("fill");

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return Fill;
    }

    public AlphaValue? ComputeFillOpacity()
    {
        StyleDeclaration styleDeclaration = Style?["fill-opacity"] ?? GetStyleValueFromClasses("fill-opacity");

        if (styleDeclaration != null)
        {
            AlphaValue alphaValue = styleDeclaration.Value;
            return alphaValue;
        }

        return FillOpacity;
    }

    public FillRule? ComputeFillRule()
    {
        StyleDeclaration styleDeclaration = Style?["fill-rule"] ?? GetStyleValueFromClasses("fill-rule");

        if (styleDeclaration != null)
            return Enum.Parse<FillRule>(styleDeclaration.Value, true);

        return FillRule;
    }

    public Paint ComputeStroke()
    {
        StyleDeclaration styleDeclaration = Style?["stroke"] ?? GetStyleValueFromClasses("stroke");

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return Stroke;
    }

    public AlphaValue? ComputeStrokeOpacity()
    {
        StyleDeclaration styleDeclaration = Style?["stroke-opacity"] ?? GetStyleValueFromClasses("stroke-opacity");

        if (styleDeclaration != null)
        {
            AlphaValue alphaValue = styleDeclaration.Value;
            return alphaValue;
        }

        return StrokeOpacity;
    }

    public LengthPercentage? ComputeStrokeWidth()
    {
        StyleDeclaration styleDeclaration = Style?["stroke-width"] ?? GetStyleValueFromClasses("stroke-width");

        if (styleDeclaration != null)
        {
            Length length = styleDeclaration.Value.Trim();
            return length.Value;
        }

        return StrokeWidth;
    }

    public StrokeLineCap? ComputeStrokeLineCap()
    {
        StyleDeclaration styleDeclaration = Style?["stroke-linecap"] ?? GetStyleValueFromClasses("stroke-linecap");

        if (styleDeclaration != null)
            return Enum.Parse<StrokeLineCap>(styleDeclaration.Value, true);

        return StrokeLineCap;
    }

    public StrokeLineJoin? ComputeStrokeLineJoin()
    {
        StyleDeclaration styleDeclaration = Style?["stroke-linejoin"] ?? GetStyleValueFromClasses("stroke-linejoin");

        if (styleDeclaration != null)
            return Enum.Parse<StrokeLineJoin>(styleDeclaration.Value, true);

        return StrokeLineJoin;
    }

    public DashArray ComputeStrokeDashArray()
    {
        StyleDeclaration styleDeclaration = Style?["stroke-dasharray"] ?? GetStyleValueFromClasses("stroke-dasharray");

        if (styleDeclaration != null)
        {
            DashArray dashArray = DashArray.Parse(styleDeclaration.Value.Trim());
            return dashArray;
        }

        return StrokeDashArray;
    }

    public LengthPercentage? ComputeStrokeDashOffset()
    {
        StyleDeclaration styleDeclaration = Style?["stroke-dashoffset"] ?? GetStyleValueFromClasses("stroke-dashoffset");

        if (styleDeclaration != null)
        {
            LengthPercentage length = styleDeclaration.Value.Trim();
            return length;
        }

        return StrokeDashOffset;
    }

    public double? ComputeStrokeMiterLimit()
    {
        StyleDeclaration styleDeclaration = Style?["stroke-miterlimit"] ?? GetStyleValueFromClasses("stroke-miterlimit");

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        return StrokeMiterLimit;
    }

    public double? ComputeOpacity()
    {
        StyleDeclaration styleDeclaration = Style?["opacity"] ?? GetStyleValueFromClasses("opacity");

        if (styleDeclaration != null)
            return double.Parse(styleDeclaration.Value, CultureInfo.InvariantCulture);

        return Opacity;
    }

    public LengthPercentage? ComputeFontSize()
    {
        StyleDeclaration styleDeclaration = Style?["font-size"] ?? GetStyleValueFromClasses("font-size");

        if (styleDeclaration != null)
        {
            LengthPercentage length = styleDeclaration.Value.Trim();
            return length;
        }

        return FontSize;
    }

    protected StyleDeclaration GetStyleValueFromClasses(string name)
    {
        IEnumerable<StyleRuleSet> applicableStyleRuleSets = GetApplicableStyleRuleSets()?.ToList();

        return applicableStyleRuleSets?
            .Select(x => x.Declarations?[name])
            .FirstOrDefault(x => x != null);
    }

    private IEnumerable<StyleRuleSet> GetApplicableStyleRuleSets()
    {
        List<StyleSelector> possibleSelectors = ComputePossibleStyleRuleSetSelectors();

        Svg parentSvg = GetParentSvg();
        IEnumerable<StyleRuleSet> styleRuleSets = parentSvg?.GetAllStyleRuleSets(MimeTypes.TextCss);

        return styleRuleSets?
            .Where(x => possibleSelectors.Contains(x.Selector))
            .Where(x => x.Declarations != null)
            .OrderBy(x => Array.IndexOf(StyleSelectorImportance, x.Selector.Type))
            .Reverse()
            .ToList();
    }

    private List<StyleSelector> ComputePossibleStyleRuleSetSelectors()
    {
        List<StyleSelector> possibleSelectors = new()
        {
            new StyleSelector(StyleSelectorType.Element, ElementName)
        };

        if (ClassNames is { Count: > 0 })
        {
            IEnumerable<StyleSelector> styleRuleSetSelectors = ClassNames
                .Select(x => new StyleSelector(StyleSelectorType.Class, x));

            possibleSelectors.AddRange(styleRuleSetSelectors);
        }

        bool hasId = !string.IsNullOrWhiteSpace(Id);
        if (hasId)
        {
            StyleSelector styleSelector = new(StyleSelectorType.Id, Id);
            possibleSelectors.Add(styleSelector);
        }

        return possibleSelectors;
    }

    public SpacePreservation? ComputeSpacePreservation()
    {
        return Space;
    }

    public WhiteSpacePreservation? ComputeWhiteSpacePreservation()
    {
        StyleDeclaration styleDeclaration = Style?["white-space"] ?? GetStyleValueFromClasses("white-space");

        if (styleDeclaration != null)
            return Enum.Parse<WhiteSpacePreservation>(styleDeclaration.Value, true);

        return WhiteSpace;
    }
}