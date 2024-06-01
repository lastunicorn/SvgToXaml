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

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal class SvgToXamlConversion : SvgContainerToXamlConversion<Svg, Canvas>
{
    public SvgToXamlConversion(Svg svg, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svg, conversionContext, referrer)
    {
    }

    protected override Canvas CreateXamlElement()
    {
        return new Canvas();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        // PreserveAspectRatio
        //      => ViewBox.Stretch
        //      => ViewBox.HorizontalAlignment
        //      => ViewBox.VerticalAlignment

        // none + <any>
        //      ViewBox.Stretch = "Fill"
        //      ViewBox.HorizontalAlignment - ignored
        //      ViewBox.VerticalAlignment - ignored

        // meet
        //      ViewBox.Stretch = "Uniform"
        //
        // slice
        //      ViewBox.Stretch = "UniformToFill"
        //
        // XMin, XMid, XMax
        //      ViewBox.HorizontalAlignment = Left, Center, Right
        //
        // YMin, YMid, YMax
        //      ViewBox.VerticalAlignment = Top, Center, Bottom
        //

        SetLocation();
        SetSize();
        SetTransformation();
    }

    private void SetLocation()
    {
        double x = 0;
        double y = 0;

        if (SvgElement.X != null)
            x = SvgElement.X.Value.ComputeValue();

        if (SvgElement.Y != null)
            y = SvgElement.Y.Value.ComputeValue();

        if (x != 0 || y != 0)
        {
            Canvas wrapperCanvas = new()
            {
                RenderTransform = new TranslateTransform
                {
                    X = x,
                    Y = y
                }
            };

            SetWrapperXamlElement(wrapperCanvas);
        }
    }

    private void SetSize()
    {
        if (SvgElement.Width != null)
            XamlElement.Width = SvgElement.Width.Value.ToUserUnits();
        else if (SvgElement.ViewBox != null)
            XamlElement.Width = SvgElement.ViewBox.Width.Value;

        if (SvgElement.Height != null)
            XamlElement.Height = SvgElement.Height.Value.ToUserUnits();
        else if (SvgElement.ViewBox != null)
            XamlElement.Height = SvgElement.ViewBox.Height.Value;
    }

    private void SetTransformation()
    {
        if (SvgElement.ViewBox != null)
        {
            TransformGroupBuilder transformGroupBuilder = new(XamlElement.RenderTransform);

            TranslateTransform translateTransform = CreateRenderTransform();
            if (translateTransform != null)
                transformGroupBuilder.Add(translateTransform);

            ScaleTransform scaleTransform = CreateTransformForSvgSize();
            if (scaleTransform != null)
                transformGroupBuilder.Add(scaleTransform);

            XamlElement.RenderTransform = transformGroupBuilder.RootTransform;
        }
    }

    private TranslateTransform CreateRenderTransform()
    {
        double x = 0;

        if (SvgElement.ViewBox != null && SvgElement.ViewBox.MinX.Value != 0)
            x -= SvgElement.ViewBox.MinX.Value;

        double y = 0;

        if (SvgElement.ViewBox != null && SvgElement.ViewBox.MinY.Value != 0)
            y -= SvgElement.ViewBox.MinY.Value;

        if (x != 0 || y != 0)
        {
            return new TranslateTransform
            {
                X = x,
                Y = y
            };
        }

        return null;
    }

    private ScaleTransform CreateTransformForSvgSize()
    {
        double scaleX = 1;
        double scaleY = 1;

        if (SvgElement.Width != null)
        {
            Length svgWidth = SvgElement.Width.Value.ToUserUnits();

            if (svgWidth.Value != 0)
                scaleX = svgWidth.Value / SvgElement.ViewBox.Width.Value;
        }

        if (SvgElement.Height != null)
        {
            Length svgHeight = SvgElement.Height.Value.ToUserUnits();

            if (svgHeight.Value != 0)
                scaleY = svgHeight.Value / SvgElement.ViewBox.Height.Value;
        }

        if (Math.Abs(scaleX - 1) > double.Epsilon || Math.Abs(scaleY - 1) > double.Epsilon)
        {
            return new ScaleTransform
            {
                ScaleX = scaleX,
                ScaleY = scaleY
            };
        }

        return null;
    }
}