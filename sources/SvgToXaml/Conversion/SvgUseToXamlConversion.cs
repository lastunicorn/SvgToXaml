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

internal class SvgUseToXamlConversion : IConversion<UIElement>
{
    private readonly SvgUse svgUse;

    public SvgUseToXamlConversion(SvgUse svgUse)
    {
        this.svgUse = svgUse ?? throw new ArgumentNullException(nameof(svgUse));
    }

    public UIElement Execute()
    {
        SvgElement referencedElement = svgUse.GetReferencedElement();

        IConversion<UIElement> conversion = ConvertReferencedElement(referencedElement);
        UIElement uiElement = conversion.Execute();

        if (svgUse.Transforms.Count > 0)
            uiElement.RenderTransform = svgUse.Transforms.ToXaml();

        if (svgUse.X != 0)
            Canvas.SetLeft(uiElement, svgUse.X);

        if (svgUse.Y != 0)
            Canvas.SetTop(uiElement, svgUse.Y);

        return uiElement;
    }

    private IConversion<UIElement> ConvertReferencedElement(SvgElement svgElement)
    {
        switch (svgElement)
        {
            case SvgCircle svgCircle:
                return new SvgCircleToXamlConversion(svgCircle, svgUse);

            case SvgEllipse svgEllipse:
                return new SvgEllipseToXamlConversion(svgEllipse, svgUse);

            case SvgPath svgPath:
                return new SvgPathToXamlConversion(svgPath, svgUse);

            case SvgLine svgLine:
                return new SvgLineToXamlConversion(svgLine, svgUse);

            case SvgRectangle svgRect:
                return new SvgRectangleToXamlConversion(svgRect, svgUse);

            case SvgPolygon svgPolygon:
                return new SvgPolygonToXamlConversion(svgPolygon, svgUse);

            case SvgGroup svgGChild:
                return new SvgGroupToXamlConversion(svgGChild, svgUse);

            case SvgText svgText:
                return new SvgTextToXamlConversion(svgText, svgUse);

            default:
                Type inheritedElementType = svgElement.GetType();
                throw new UnknownElementTypeException(inheritedElementType);
        }
    }
}