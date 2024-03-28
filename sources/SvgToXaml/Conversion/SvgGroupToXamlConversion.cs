// Country Flags
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
using DustInTheWind.SvgToXaml.Svg;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgGroupToXamlConversion : IConversion<Canvas>
{
    private readonly SvgGroup svgGroup;
    private readonly SvgElement referrer;

    public SvgGroupToXamlConversion(SvgGroup svgGroup, SvgElement referrer = null)
    {
        this.svgGroup = svgGroup ?? throw new ArgumentNullException(nameof(svgGroup));
        this.referrer = referrer;
    }

    public Canvas Execute()
    {
        try
        {
            Canvas canvas = CreateXamlElement();

            if (svgGroup.Transforms.Count > 0)
                canvas.RenderTransform = svgGroup.Transforms.ToXaml();

            IEnumerable<UIElement> xamlElements = svgGroup.Children
                .Where(x => x is not SvgDefinitions)
                .Select(CreateConversion)
                .Select(x => x.Execute());

            foreach (UIElement uiElement in xamlElements)
                canvas.Children.Add(uiElement);

            return canvas;
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

    protected virtual Canvas CreateXamlElement()
    {
        return new Canvas();
    }

    private IConversion<UIElement> CreateConversion(SvgElement svgElement)
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

            case SvgDefinitions:
                return null;

            case SvgGroup svgGChild:
                return new SvgGroupToXamlConversion(svgGChild, referrer);

            case SvgUse svgUse:
                return new SvgUseToXamlConversion(svgUse);

            case SvgText svgText:
                return new SvgTextToXamlConversion(svgText, referrer);

            default:
                Type inheritedElementType = svgElement.GetType();
                throw new Exception($"Unknown inherited element type: {inheritedElementType.FullName}");
        }
    }
}