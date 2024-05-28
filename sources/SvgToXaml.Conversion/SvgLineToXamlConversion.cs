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
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgLineToXamlConversion : SvgShapeToXamlConversion<SvgLine, Line>
{
    public SvgLineToXamlConversion(SvgLine svgLine, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgLine, conversionContext, referrer)
    {
    }

    protected override Line CreateXamlElement()
    {
        return new Line();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        XamlElement.X1 = SvgElement.X1;
        XamlElement.Y1 = SvgElement.Y1;
        XamlElement.X2 = SvgElement.X2;
        XamlElement.Y2 = SvgElement.Y2;
    }
}