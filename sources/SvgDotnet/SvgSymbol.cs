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

namespace DustInTheWind.SvgDotnet;

public class SvgSymbol : SvgContainer
{
    protected override string ElementName => "symbol";

    public LengthPercentage? X { get; set; }

    public LengthPercentage? Y { get; set; }

    public Length? Width { get; set; }

    public Length? Height { get; set; }

    public SvgViewBox ViewBox { get; set; }

    public SvgSymbol()
    {
        Children.AcceptedTypes = new[]
        {
            typeof(SvgDescription),
            typeof(SvgTitle),

            typeof(SvgLinearGradient),
            typeof(SvgRadialGradient),

            typeof(SvgCircle),
            typeof(SvgEllipse),
            typeof(SvgLine),
            typeof(SvgPath),
            typeof(SvgPolygon),
            typeof(SvgPolyline),
            typeof(SvgRectangle),

            typeof(SvgDefinitions),
            typeof(SvgGroup),
            typeof(Svg),
            typeof(SvgSymbol),
            typeof(SvgUse),

            typeof(SvgClipPath),
            typeof(SvgStyle),
            typeof(SvgText)
        };
    }
}