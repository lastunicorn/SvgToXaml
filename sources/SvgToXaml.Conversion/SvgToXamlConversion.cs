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

using System.Windows.Controls;
using DustInTheWind.SvgDotnet;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion;

public class SvgToXamlConversion : SvgContainerToXamlConversion<Svg, Canvas>
{
    private readonly Svg svg;

    public SvgToXamlConversion(Svg svg, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svg, conversionContext, referrer)
    {
        this.svg = svg ?? throw new ArgumentNullException(nameof(svg));
    }

    protected override Canvas CreateXamlElement()
    {
        return new Canvas();
    }

    protected override void ConvertProperties(List<SvgElement> inheritedSvgElements)
    {
        base.ConvertProperties(inheritedSvgElements);

        if (svg.ViewBox == null)
        {
            if (svg.Width != null)
                XamlElement.Width = svg.Width.Value.ToUserUnits();

            if (svg.Height != null)
                XamlElement.Height = svg.Height.Value.ToUserUnits();
        }
        else
        {
            XamlElement.Width = svg.ViewBox.Width.Value;
            XamlElement.Height = svg.ViewBox.Height.Value;

            bool viewBoxIsTranslated = svg.ViewBox.OriginX is { Value: not 0 } ||
                                       svg.ViewBox.OriginY is { Value: not 0 };

            if (viewBoxIsTranslated)
                XamlElement.RenderTransform = CreateRenderTransform(svg.ViewBox);
        }
    }

    private static TranslateTransform CreateRenderTransform(SvgViewBox svgViewBox)
    {
        TranslateTransform translateTransform = new();

        if (svgViewBox.OriginX.Value != 0)
            translateTransform.X = -svgViewBox.OriginX.Value;

        if (svgViewBox.OriginY.Value != 0)
            translateTransform.Y = -svgViewBox.OriginY.Value;

        return translateTransform;
    }
}