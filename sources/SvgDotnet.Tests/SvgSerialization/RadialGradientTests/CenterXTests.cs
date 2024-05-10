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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.RadialGradientTests;

public class CenterXTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRadialGradientWithCenterX300_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterX300()
    {
        ParseSvgFile("radialgradient-cx-positive.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            SvgLength expected = 300;
            svgRadialGradient.CenterX.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingRadialGradientWithCenterXNegative300_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterXNegative300()
    {
        ParseSvgFile("radialgradient-cx-negative.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            SvgLength expected = -300;
            svgRadialGradient.CenterX.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingRadialGradientWithCenterXZero_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterXZero()
    {
        ParseSvgFile("radialgradient-cx-zero.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.CenterX.Should().Be(SvgLength.Zero);
        });
    }

    [Fact]
    public void HavingRadialGradientWithMissingCenterX_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterXZero()
    {
        ParseSvgFile("radialgradient-cx-missing.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.CenterX.Should().BeNull();
        });
    }
}