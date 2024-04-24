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
using System.Windows.Media;
using DustInTheWind.SvgToXaml.SvgModel;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgUseToXamlConversion : IConversion<UIElement>
{
    private readonly SvgUse svgUse;
    private readonly ConversionContext conversionContext;
    private readonly SvgElement referrer;

    public SvgUseToXamlConversion(SvgUse svgUse, ConversionContext conversionContext, SvgElement referrer = null)
    {
        this.svgUse = svgUse ?? throw new ArgumentNullException(nameof(svgUse));
        this.conversionContext = conversionContext ?? throw new ArgumentNullException(nameof(conversionContext));
        this.referrer = referrer;
    }

    public UIElement Execute()
    {
        SvgElement referencedElement = svgUse.GetReferencedElement();

        if (referencedElement == null)
            return null;

        IConversion<UIElement> conversion = ConvertReferencedElement(referencedElement);
        UIElement uiElement = conversion.Execute();

        double left = svgUse.X;
        double top = svgUse.Y;

        if (svgUse.Transforms.Count > 0) 
            uiElement.RenderTransform = svgUse.Transforms.ToXaml(uiElement.RenderTransform);

        if (left != 0 || top != 0)
        {
            TransformGroupBuilder transformGroupBuilder = new(uiElement.RenderTransform);

            TranslateTransform translateTransform = new(left, top);
            transformGroupBuilder.Add(translateTransform);
            uiElement.RenderTransform = transformGroupBuilder.RootTransform;
        }

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
                return new SvgGroupToXamlConversion(svgGChild, conversionContext, svgUse);

            case SvgText svgText:
                return new SvgTextToXamlConversion(svgText, svgUse);

            case SvgUse childSvgUse:
                return new SvgUseToXamlConversion(childSvgUse,conversionContext, svgUse);

            default:
                Type inheritedElementType = svgElement.GetType();
                throw new UnknownElementTypeException(inheritedElementType);
        }
    }
}