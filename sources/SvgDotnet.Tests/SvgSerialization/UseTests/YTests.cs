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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.UseTests;

public class YTests : SvgFileTestsBase
{
    [Fact]
    public void HavingUseWithPositiveY_WhenSvgFileIsParsed_ThenUseHasThatY()
    {
        ParseSvgFile("y-positive.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new SvgLength(10);
            svgUse.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithNegativeY_WhenSvgFileIsParsed_ThenUseHasThatY()
    {
        ParseSvgFile("y-negative.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new SvgLength(-10);
            svgUse.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithZeroY_WhenSvgFileIsParsed_ThenUseHasZeroY()
    {
        ParseSvgFile("y-zero.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new SvgLength(0);
            svgUse.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithMissingY_WhenSvgFileIsParsed_ThenUseHasNullY()
    {
        ParseSvgFile("y-missing.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            svgUse.Y.Should().BeNull();
        });
    }

    [Fact]
    public void HavingUseWithPercentageY_WhenSvgFileIsParsed_ThenUseHasThatY()
    {
        ParseSvgFile("y-percentage.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new SvgPercentage(25);
            svgUse.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithPositiveYInPixels_WhenSvgFileIsParsed_ThenUseHasThatY()
    {
        ParseSvgFile("y-positivepx.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new SvgLength(42, SvgLengthUnit.Pixels);
            svgUse.Y.Should().Be(expected);
        });
    }
}