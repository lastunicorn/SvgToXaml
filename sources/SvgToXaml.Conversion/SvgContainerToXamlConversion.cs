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
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion;

public abstract class SvgContainerToXamlConversion<TSvg, TXaml> : ToXamlConversion<TSvg, TXaml>
    where TSvg : SvgContainer
    where TXaml : Panel
{
    protected SvgContainerToXamlConversion(TSvg svgElement, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgElement, conversionContext, referrer)
    {
    }

    protected override void ConvertProperties(List<SvgElement> inheritedSvgElements)
    {
        base.ConvertProperties(inheritedSvgElements);

        ConvertChildren();
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
                return new SvgCircleToXamlConversion(svgCircle, ConversionContext, Referrer);

            case SvgEllipse svgEllipse:
                return new SvgEllipseToXamlConversion(svgEllipse, ConversionContext, Referrer);

            case SvgPath svgPath:
                return new SvgPathToXamlConversion(svgPath, ConversionContext, Referrer);

            case SvgLine svgLine:
                return new SvgLineToXamlConversion(svgLine, ConversionContext, Referrer);

            case SvgRectangle svgRect:
                return new SvgRectangleToXamlConversion(svgRect, ConversionContext, Referrer);

            case SvgPolygon svgPolygon:
                return new SvgPolygonToXamlConversion(svgPolygon, ConversionContext, Referrer);

            case SvgPolyline svgPolyline:
                return new SvgPolylineToXamlConversion(svgPolyline, ConversionContext, Referrer);

            case SvgGroup svgGChild:
                return new SvgGroupToXamlConversion(svgGChild, ConversionContext, Referrer);

            case Svg childSvg:
                return new SvgToXamlConversion(childSvg, ConversionContext, Referrer);

            case SvgUse svgUse:
                return new SvgUseToXamlConversion(svgUse, ConversionContext);

            case SvgText svgText:
                return new SvgTextToXamlConversion(svgText, ConversionContext, Referrer);

            case SvgTitle:
            case SvgDescription:
            case SvgDefinitions:
            case SvgLinearGradient:
            case SvgRadialGradient:
            case SvgClipPath:
            case SvgStyle:
            {
                Type inheritedElementType = svgElement.GetType();
                ConversionIssue conversionIssue = new("Conversion", $"Element ignored. Type: {inheritedElementType.FullName}");
                ConversionContext.Warnings.Add(conversionIssue);
                return null;
            }

            default:
            {
                Type inheritedElementType = svgElement.GetType();
                ConversionIssue conversionIssue = new("Conversion", $"Unknown element type: {inheritedElementType.FullName}");
                ConversionContext.Errors.Add(conversionIssue);
                return null;
            }
        }
    }
}