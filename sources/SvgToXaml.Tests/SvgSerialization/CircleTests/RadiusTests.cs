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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.CircleTests;

public class RadiusTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithRadius16_WhenSvgFileIsParsed_ThenSvgContainsCircleWithRadius16()
    {
        ParseSvgFile("circle-r-positive.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.Radius.Should().Be(16);
        });
    }

    [Fact]
    public void HavingCircleWithRadiusNegative12_WhenSvgFileIsParsed_ThenSvgContainsCircleWithRadiusZeroAndOneWarning()
    {
        ParseSvgFile("circle-r-negative.svg", result =>
        {
            SvgCircle svgCircle = result.Svg.Children[0] as SvgCircle;

            svgCircle.Radius.Should().Be(0);
            
            result.Warnings.Should().HaveCount(1);
            result.Warnings[0].Path.Should().Be("svg.circle(1).@r");
        });
    }

    [Fact]
    public void HavingCircleWithRadiusZero_WhenSvgFileIsParsed_ThenSvgContainsCircleWithRadiusZero()
    {
        ParseSvgFile("circle-r-zero.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.Radius.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithNoRadius_WhenSvgFileIsParsed_ThenSvgContainsCircleWithRadiusZero()
    {
        ParseSvgFile("circle-r-missing.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.Radius.Should().Be(0);
        });
    }
}