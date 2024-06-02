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

using System.Windows.Shapes;
using DustInTheWind.SvgDotnet;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal class SvgCircleToXamlConversion : SvgShapeToXamlConversion<SvgCircle, Ellipse>
{
    public SvgCircleToXamlConversion(SvgCircle svgCircle, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgCircle, conversionContext, referrer)
    {
    }

    protected override Ellipse CreateXamlElement()
    {
        return new Ellipse();
    }

    protected override void ConvertProperties()
    {
        SetPosition();

        base.ConvertProperties();

        SetSize();
    }

    private void SetPosition()
    {
        double left = SvgElement.CenterX - SvgElement.Radius;
        double top = SvgElement.CenterY - SvgElement.Radius;

        if (left != 0 || top != 0)
            XamlElement.RenderTransform = new TranslateTransform(left, top);
    }

    private void SetSize()
    {
        XamlElement.Width = SvgElement.Radius * 2;
        XamlElement.Height = SvgElement.Radius * 2;
    }

    protected override void OnExecuting()
    {
        base.OnExecuting();

        bool hasBorder = SvgElement.StrokeWidth != null && SvgElement.StrokeWidth.Value.ComputeValue() != 0;
        if (hasBorder)
            ConversionContext.Issues.AddWarning($"[{SvgElement.ElementName}] Border is present. It will not be correctly translated.");
    }

    protected override void OnExecuted()
    {
        base.OnExecuted();

        if (XamlElement == null)
            return;

        bool isZeroSize = XamlElement.Width == 0 || XamlElement.Height == 0;
        if (isZeroSize)
            ConversionContext.Issues.AddWarning($"[{XamlElement.GetType().Name}] Zero-size circle created.");
    }
}