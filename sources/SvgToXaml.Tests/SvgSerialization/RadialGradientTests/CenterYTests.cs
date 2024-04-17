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

using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.RadialGradientTests;

public class CenterYTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRadialGradientWithCenterY300_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterY300()
    {
        ParseSvgFile("radialgradient-cy-positive.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            SvgLength expected = 300;
            svgRadialGradient.CenterY.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingRadialGradientWithCenterYNegative300_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterYNegative300()
    {
        ParseSvgFile("radialgradient-cy-negative.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            SvgLength expected = -300;
            svgRadialGradient.CenterY.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingRadialGradientWithCenterYZero_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterYZero()
    {
        ParseSvgFile("radialgradient-cy-zero.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.CenterY.Should().Be(SvgLength.Zero);
        });
    }

    [Fact]
    public void HavingRadialGradientWithMissingCenterY_WhenSvgParsed_ThenSvgContainsRadialGradientWithCenterYZero()
    {
        ParseSvgFile("radialgradient-cy-missing.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.CenterY.Should().BeNull();
        });
    }
}