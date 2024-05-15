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

public class XTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSvgWithPositiveX_WhenSvgFileIsParsed_ThenSvgHasThatX()
    {
        ParseSvgFile("svg-x-positive.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(10);
            svg.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithNegativeX_WhenSvgFileIsParsed_ThenSvgHasThatX()
    {
        ParseSvgFile("svg-x-negative.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(-10);
            svg.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithZeroX_WhenSvgFileIsParsed_ThenSvgHasZeroX()
    {
        ParseSvgFile("svg-x-zero.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(0);
            svg.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithMissingX_WhenSvgFileIsParsed_ThenSvgHasNullX()
    {
        ParseSvgFile("svg-x-missing.svg", svg =>
        {
            svg.X.Should().BeNull();
        });
    }

    [Fact]
    public void HavingSvgWithPercentageX_WhenSvgFileIsParsed_ThenSvgHasThatX()
    {
        ParseSvgFile("svg-x-percentage.svg", svg =>
        {
            LengthPercentage expected = new SvgPercentage(25);
            svg.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSvgWithPositiveXInPixels_WhenSvgFileIsParsed_ThenSvgHasThatX()
    {
        ParseSvgFile("svg-x-positivepx.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(42, SvgLengthUnit.Pixels);
            svg.X.Should().Be(expected);
        });
    }
}