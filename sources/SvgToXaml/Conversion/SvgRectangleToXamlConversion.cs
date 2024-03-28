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

using System.Windows.Controls;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Svg;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgRectangleToXamlConversion : SvgShapeToXamlConversion<SvgRectangle, Rectangle>
{
    public SvgRectangleToXamlConversion(SvgRectangle svgRectangle, SvgElement referrer = null)
        : base(svgRectangle, referrer)
    {
    }

    protected override Rectangle CreateXamlElement()
    {
        Rectangle rectangle = new()
        {
            Width = SvgElement.Width,
            Height = SvgElement.Height
        };

        if (SvgElement.X != 0)
            Canvas.SetLeft(rectangle, SvgElement.X);

        if (SvgElement.Y != 0)
            Canvas.SetTop(rectangle, SvgElement.Y);

        return rectangle;
    }
}