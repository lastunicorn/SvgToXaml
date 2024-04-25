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

using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.SvgModel;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgRectangleToXamlConversion : SvgShapeToXamlConversion<SvgRectangle, Rectangle>
{
    public SvgRectangleToXamlConversion(SvgRectangle svgRectangle, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgRectangle, conversionContext, referrer)
    {
    }

    protected override Rectangle CreateXamlElement()
    {
        return new Rectangle();
    }

    protected override void ConvertProperties(List<SvgElement> inheritedSvgElements)
    {
        SetPosition();

        base.ConvertProperties(inheritedSvgElements);

        SetSize();
        SetCornetRadius();
    }

    private void SetPosition()
    {
        double left = SvgElement.X;
        double top = SvgElement.Y;

        if (left != 0 || top != 0)
            XamlElement.RenderTransform = new TranslateTransform(left, top);
    }

    private void SetSize()
    {
        XamlElement.Width = SvgElement.Width;
        XamlElement.Height = SvgElement.Height;
    }

    private void SetCornetRadius()
    {
        double? radiusX = SvgElement.Rx ?? SvgElement.Ry;
        double? radiusY = SvgElement.Ry ?? SvgElement.Rx;

        if (radiusX != null)
            XamlElement.RadiusX = radiusX.Value;

        if (radiusY != null)
            XamlElement.RadiusY = radiusY.Value;
    }

    protected override void OnExecuted()
    {
        base.OnExecuted();

        if (XamlElement == null)
            return;

        bool isZeroSize = XamlElement.Width == 0 || XamlElement.Height == 0;

        if (isZeroSize)
        {
            ConversionIssue conversionIssue = new("Conversion", "Zero-size rectangle present.");
            ConversionContext.Warnings.Add(conversionIssue);
        }
    }
}