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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal abstract class ToXamlConversion<TSvg, TXaml> : IConversion<TXaml>
    where TSvg : SvgElement
    where TXaml : UIElement
{
    protected SvgElement Referrer { get; }

    protected TSvg SvgElement { get; }

    protected List<SvgElement> ShadowTree { get; private set; }

    protected ConversionContext ConversionContext { get; }

    protected TXaml XamlElement { get; private set; }

    protected TXaml WrapperXamlElement { get; private set; }

    protected List<string> KnownStyleSelectors { get; } = new()
    {
        "fill",
        "fill-opacity",
        "fill-rule",
        "stroke",
        "stroke-opacity",
        "stroke-width",
        "stroke-linecap",
        "stroke-linejoin",
        "stroke-dasharray",
        "stroke-dashoffset",
        "stroke-miterlimit",
        "opacity",
        "display"
    };

    protected ToXamlConversion(TSvg svgElement, ConversionContext conversionContext, SvgElement referrer = null)
    {
        SvgElement = svgElement ?? throw new ArgumentNullException(nameof(svgElement));
        ConversionContext = conversionContext ?? throw new ArgumentNullException(nameof(conversionContext));
        Referrer = referrer;
    }

    public TXaml Execute()
    {
        OnExecuting();

        try
        {
            Display? display = SvgElement.ComputeDisplay();

            if (display == Display.None)
                return null;

            ShadowTree = EnumerateInheritedElements().ToList();
            XamlElement = CreateXamlElement();

            if (XamlElement == null)
                return null;

            CheckStyleDeclarations();

            ConvertProperties();

            return WrapperXamlElement ?? XamlElement;
        }
        catch (SvgConversionException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SvgConversionException(ex);
        }
        finally
        {
            OnExecuted();
        }
    }

    protected virtual void OnExecuting()
    {
    }

    protected abstract TXaml CreateXamlElement();

    private void CheckStyleDeclarations()
    {
        if (SvgElement.Style == null)
            return;

        IEnumerable<StyleDeclaration> unknownStyleDeclarations = SvgElement.Style
            .Where(x => !KnownStyleSelectors.Contains(x.Name));

        foreach (StyleDeclaration svgStyleDeclaration in unknownStyleDeclarations)
            ConversionContext.Issues.AddWarning($"[{SvgElement.ElementName}] Unknown style declaration: {svgStyleDeclaration.Name}");
    }

    protected virtual IEnumerable<SvgElement> EnumerateInheritedElements()
    {
        yield return SvgElement;

        if (Referrer == null)
        {
            IEnumerable<SvgElement> ancestors = SvgElement.EnumerateAncestors();

            foreach (SvgElement ancestor in ancestors)
                yield return ancestor;
        }
        else
        {
            IEnumerable<SvgElement> ancestors = SvgElement.EnumerateAncestors()
                .TakeWhile(x => x.GetType() != typeof(SvgDefinitions));

            foreach (SvgElement ancestor in ancestors)
                yield return ancestor;

            yield return Referrer;

            IEnumerable<SvgElement> referrerAncestors = Referrer.EnumerateAncestors();

            foreach (SvgElement ancestor in referrerAncestors)
                yield return ancestor;
        }
    }

    protected virtual void ConvertProperties()
    {
        if (XamlElement is FrameworkElement frameworkElement)
            SetLanguage(frameworkElement);

        if (SvgElement.Transforms.Count > 0)
            ApplyTransforms();

        if (SvgElement.ClipPath != null)
            ApplyClipPath();

        SetOpacity();
    }

    private void SetLanguage(FrameworkElement frameworkElement)
    {
        string languageId = SvgElement.Language;

        if (languageId != null)
            frameworkElement.Language = XmlLanguage.GetLanguage(languageId);
    }

    private void ApplyTransforms()
    {
        XamlElement.RenderTransform = SvgElement.Transforms.ToXaml(XamlElement.RenderTransform);
    }

    private void ApplyClipPath()
    {
        string referencedId = SvgElement.ClipPath.Url.ReferencedId;

        if (referencedId == null)
            return;

        SvgElement referencedElement = SvgElement.GetParentSvg()?.FindChild(referencedId);

        if (referencedElement is not SvgClipPath svgClipPath)
            return;

        SvgElement firstChild = svgClipPath.Children.FirstOrDefault();

        ToGeometryConversion toGeometryConversion = new(firstChild, ConversionContext);
        Geometry geometry = toGeometryConversion.Execute();

        if (geometry == null || geometry.IsEmpty())
            return;

        XamlElement.Clip = geometry;
    }

    private void SetOpacity()
    {
        double? opacity = SvgElement.ComputeOpacity();

        if (opacity != null)
            XamlElement.Opacity = opacity.Value;
    }

    protected virtual void OnExecuted()
    {
        if (XamlElement == null)
            return;

        bool isFullTransparent = XamlElement.Opacity == 0;
        if (isFullTransparent)
            ConversionContext.Issues.AddWarning($"[{XamlElement.GetType().Name}] Completely transparent element created.");
    }

    protected void SetWrapperXamlElement(TXaml wrapperElement)
    {
        if (wrapperElement is Panel panel)
            panel.Children.Add(XamlElement);

        WrapperXamlElement = wrapperElement;
    }
}