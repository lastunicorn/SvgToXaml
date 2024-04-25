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

public class RadiusTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRadialGradientWithRadius16_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithRadius16()
    {
        ParseSvgFile("radialgradient-r-positive.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            SvgLength expected = 16;
            svgRadialGradient.Radius.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingRadialGradientWithRadiusNegative12_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithRadiusZeroAndOneWarning()
    {
        ParseSvgFile("radialgradient-r-negative.svg", result =>
        {
            SvgRadialGradient svgRadialGradient = result.Svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.Radius.Should().Be(SvgLength.Zero);
            
            result.Warnings.Should().HaveCount(1);
            result.Warnings[0].Path.Should().Be("svg.radialGradient(1).@r");
        });
    }

    [Fact]
    public void HavingRadialGradientWithRadiusZero_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithRadiusZero()
    {
        ParseSvgFile("radialgradient-r-zero.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.Radius.Should().Be(SvgLength.Zero);
        });
    }

    [Fact]
    public void HavingRadialGradientWithNoRadius_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithRadiusZero()
    {
        ParseSvgFile("radialgradient-r-missing.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.Radius.Should().BeNull();
        });
    }
}