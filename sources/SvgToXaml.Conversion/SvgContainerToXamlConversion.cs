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
using System.Windows.Controls;
using System.Windows.Markup;
using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgToXaml.Conversion;

public abstract class SvgContainerToXamlConversion : IConversion<Canvas>
{
    private readonly SvgElement referrer;

    public SvgContainer SvgElement { get; }

    public Canvas XamlElement { get; private set; }

    protected SvgContainerToXamlConversion(SvgContainer svgContainer, SvgElement referrer = null)
    {
        SvgElement = svgContainer ?? throw new ArgumentNullException(nameof(svgContainer));
        this.referrer = referrer;
    }

    public Canvas Execute()
    {
        try
        {
            XamlElement = CreateXamlElement();
            
            if (XamlElement is FrameworkElement frameworkElement)
                SetLanguage(frameworkElement);

            if (SvgElement.Transforms.Count > 0)
                ApplyTransforms();

            ConvertSpecificAttributes();

            ConvertChildren();

            return XamlElement;
        }
        catch (SvgConversionException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SvgConversionException(ex);
        }
    }

    protected abstract Canvas CreateXamlElement();

    private void SetLanguage(FrameworkElement frameworkElement)
    {
        string languageId = SvgElement.Language ?? SvgElement.XmlLanguage;

        if (languageId != null)
            frameworkElement.Language = XmlLanguage.GetLanguage(languageId);
    }

    private void ApplyTransforms()
    {
        XamlElement.RenderTransform = SvgElement.Transforms.ToXaml(XamlElement.RenderTransform);
    }

    protected virtual void ConvertSpecificAttributes()
    {
    }

    private void ConvertChildren()
    {
        IEnumerable<UIElement> xamlElements = SvgElement.Children
            .Where(x => x is not SvgDefinitions)
            .Select(CreateConversionFor)
            .Where(x => x != null)
            .Select(x => x.Execute())
            .Where(x => x != null);

        foreach (UIElement uiElement in xamlElements)
            XamlElement.Children.Add(uiElement);
    }

    private IConversion<UIElement> CreateConversionFor(SvgElement svgElement)
    {
        switch (svgElement)
        {
            case SvgCircle svgCircle:
                return new SvgCircleToXamlConversion(svgCircle, referrer);

            case SvgEllipse svgEllipse:
                return new SvgEllipseToXamlConversion(svgEllipse, referrer);

            case SvgPath svgPath:
                return new SvgPathToXamlConversion(svgPath, referrer);

            case SvgLine svgLine:
                return new SvgLineToXamlConversion(svgLine, referrer);

            case SvgRectangle svgRect:
                return new SvgRectangleToXamlConversion(svgRect, referrer);

            case SvgPolygon svgPolygon:
                return new SvgPolygonToXamlConversion(svgPolygon, referrer);

            case SvgPolyline svgPolyline:
                return new SvgPolylineToXamlConversion(svgPolyline, referrer);

            case SvgGroup svgGChild:
                return new SvgGroupToXamlConversion(svgGChild, referrer);

            case SvgUse svgUse:
                return new SvgUseToXamlConversion(svgUse);

            case SvgText svgText:
                return new SvgTextToXamlConversion(svgText, referrer);

            case SvgDefinitions:
            case SvgLinearGradient:
            case SvgRadialGradient:
                return null;

            default:
                Type inheritedElementType = svgElement.GetType();
                throw new Exception($"Unknown inherited element type: {inheritedElementType.FullName}");
        }
    }
}