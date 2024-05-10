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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.CircleTests;

public class CenterYTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithCenterY300_WhenSvgParsed_ThenSvgContainsCircleWithCenterY300()
    {
        ParseSvgFile("circle-cy-positive.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterY.Should().Be(300);
        });
    }

    [Fact]
    public void HavingCircleWithCenterYNegative300_WhenSvgParsed_ThenSvgContainsCircleWithCenterYNegative300()
    {
        ParseSvgFile("circle-cy-negative.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterY.Should().Be(-300);
        });
    }

    [Fact]
    public void HavingCircleWithCenterYZero_WhenSvgParsed_ThenSvgContainsCircleWithCenterYZero()
    {
        ParseSvgFile("circle-cy-zero.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithMissingCenterY_WhenSvgParsed_ThenSvgContainsCircleWithCenterYZero()
    {
        ParseSvgFile("circle-cy-missing.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterY.Should().Be(0);
        });
    }
}