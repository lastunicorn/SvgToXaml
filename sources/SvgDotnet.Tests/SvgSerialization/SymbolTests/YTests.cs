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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SymbolTests;

public class YTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSymbolWithPositiveY_WhenSvgFileIsParsed_ThenSymbolHasThatY()
    {
        ParseSvgFile("symbol-y-positive.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(10);
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithNegativeY_WhenSvgFileIsParsed_ThenSymbolHasThatY()
    {
        ParseSvgFile("symbol-y-negative.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(-10);
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithZeroY_WhenSvgFileIsParsed_ThenSymbolHasZeroY()
    {
        ParseSvgFile("symbol-y-zero.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(0);
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithMissingY_WhenSvgFileIsParsed_ThenSymbolHasNullY()
    {
        ParseSvgFile("symbol-y-missing.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Y.Should().BeNull();
        });
    }

    [Fact]
    public void HavingSymbolWithPercentageY_WhenSvgFileIsParsed_ThenSymbolHasThatY()
    {
        ParseSvgFile("symbol-y-percentage.svg", svg =>
        {
            LengthPercentage expected = new SvgPercentage(25);
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Y.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithPositiveYInPixels_WhenSvgFileIsParsed_ThenSymbolHasThatY()
    {
        ParseSvgFile("symbol-y-positivepx.svg", svg =>
        {
            LengthPercentage expected = new SvgLength(42, SvgLengthUnit.Pixels);
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Y.Should().Be(expected);
        });
    }
}