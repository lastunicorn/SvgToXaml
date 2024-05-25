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
using ScaleTransform = System.Windows.Media.ScaleTransform;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgToXamlConversion : SvgContainerToXamlConversion<Svg, Canvas>
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
        
        if (svg.Width != null)
            XamlElement.Width = svg.Width.Value.ToUserUnits();

        if (svg.Height != null)
            XamlElement.Height = svg.Height.Value.ToUserUnits();

        if (svg.ViewBox != null)
        {
            XamlElement.Width = svg.ViewBox.Width.Value;
            XamlElement.Height = svg.ViewBox.Height.Value;

            //XamlElement.ClipToBounds = true;

            bool viewBoxIsTranslated = svg.ViewBox.MinX is { Value: not 0 } ||
                                       svg.ViewBox.MinY is { Value: not 0 };

            TransformGroupBuilder transformGroupBuilder = new(XamlElement.RenderTransform);

            if (viewBoxIsTranslated)
            {
                TranslateTransform translateTransform = CreateRenderTransform(svg.ViewBox);
                transformGroupBuilder.Add(translateTransform);
            }

            bool svgHasSizeDefined = svg.Width != null || svg.Height != null;

            if (svgHasSizeDefined)
            {
                ScaleTransform scaleTransform = CreateTransformForSvgSize();
                transformGroupBuilder.Add(scaleTransform);
            }

            XamlElement.RenderTransform = transformGroupBuilder.RootTransform;
        }
    }

    private static TranslateTransform CreateRenderTransform(SvgViewBox svgViewBox)
    {
        TranslateTransform translateTransform = new();

        if (svgViewBox.MinX.Value != 0)
            translateTransform.X = -svgViewBox.MinX.Value;

        if (svgViewBox.MinY.Value != 0)
            translateTransform.Y = -svgViewBox.MinY.Value;

        return translateTransform;
    }

    private ScaleTransform CreateTransformForSvgSize()
    {
        ScaleTransform scaleTransform = new();

        if (svg.Width != null)
        {
            Length svgWidth = svg.Width.Value.ToUserUnits();

            if (svgWidth.Value != 0)
                scaleTransform.ScaleX = svgWidth.Value / svg.ViewBox.Width.Value;
        }

        if (svg.Height != null)
        {
            Length svgHeight = svg.Height.Value.ToUserUnits();

            if (svgHeight.Value != 0)
                scaleTransform.ScaleY = svgHeight.Value / svg.ViewBox.Height.Value;
        }

        return scaleTransform;
    }
}