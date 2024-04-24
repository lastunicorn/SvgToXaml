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

using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.SvgModel;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgRectangleToXamlConversion : SvgShapeToXamlConversion<SvgRectangle, Rectangle>
{
    public SvgRectangleToXamlConversion(SvgRectangle svgRectangle, SvgElement referrer = null)
        : base(svgRectangle, referrer)
    {
    }

    protected override Rectangle CreateXamlElement()
    {
        double? radiusX = SvgElement.Rx ?? SvgElement.Ry;
        double? radiusY = SvgElement.Ry ?? SvgElement.Rx;

        Rectangle rectangle = new()
        {
            Width = SvgElement.Width,
            Height = SvgElement.Height
        };

        if (radiusX != null)
            rectangle.RadiusX = radiusX.Value;

        if (radiusY != null)
            rectangle.RadiusY = radiusY.Value;

        double left = SvgElement.X;
        double top = SvgElement.Y;

        if (left != 0 || top != 0)
            rectangle.RenderTransform = new TranslateTransform(left, top);
        
        return rectangle;
    }
}