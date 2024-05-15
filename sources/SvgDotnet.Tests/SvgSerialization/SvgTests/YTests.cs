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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgTests;

public class YTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSvgWithPositiveY_WhenSvgFileIsParsed_ThenSvgHasThatY()
    {
        ParseSvgFile("svg-y-positive.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(10);
            svg.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithNegativeY_WhenSvgFileIsParsed_ThenSvgHasThatY()
    {
        ParseSvgFile("svg-y-negative.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(-10);
            svg.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithZeroY_WhenSvgFileIsParsed_ThenSvgHasZeroY()
    {
        ParseSvgFile("svg-y-zero.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(0);
            svg.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithMissingY_WhenSvgFileIsParsed_ThenSvgHasNullY()
    {
        ParseSvgFile("svg-y-missing.svg", svg =>
        {
            svg.Y.Should().BeNull();
        });
    }

    [Fact]
    public void HavingSvgWithPercentageY_WhenSvgFileIsParsed_ThenSvgHasThatY()
    {
        ParseSvgFile("svg-y-percentage.svg", svg =>
        {
            LengthPercentage expected = new SvgPercentage(25);
            svg.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithPositiveYInPixels_WhenSvgFileIsParsed_ThenSvgHasThatY()
    {
        ParseSvgFile("svg-y-positivepx.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(42, SvgLengthUnit.Pixels);
            svg.Y.Should().Be(expected);
        });
    }
}