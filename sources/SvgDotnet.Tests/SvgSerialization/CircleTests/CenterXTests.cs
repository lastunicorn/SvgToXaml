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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.CircleTests;

public class CenterXTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithCenterX300_WhenSvgParsed_ThenSvgContainsCircleWithCenterX300()
    {
        ParseSvgFile("circle-cx-positive.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterX.Should().Be(300);
        });
    }

    [Fact]
    public void HavingCircleWithCenterXNegative300_WhenSvgParsed_ThenSvgContainsCircleWithCenterXNegative300()
    {
        ParseSvgFile("circle-cx-negative.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterX.Should().Be(-300);
        });
    }

    [Fact]
    public void HavingCircleWithCenterXZero_WhenSvgParsed_ThenSvgContainsCircleWithCenterXZero()
    {
        ParseSvgFile("circle-cx-zero.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterX.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithMissingCenterX_WhenSvgParsed_ThenSvgContainsCircleWithCenterXZero()
    {
        ParseSvgFile("circle-cx-missing.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.CenterX.Should().Be(0);
        });
    }
}