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

public class XTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSymbolWithPositiveX_WhenSvgFileIsParsed_ThenSymbolHasThatX()
    {
        ParseSvgFile("symbol-x-positive.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            LengthPercentage expected = new SvgLength(10);
            svgSymbol.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithNegativeX_WhenSvgFileIsParsed_ThenSymbolHasThatX()
    {
        ParseSvgFile("symbol-x-negative.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            LengthPercentage expected = new SvgLength(-10);
            svgSymbol.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithZeroX_WhenSvgFileIsParsed_ThenSymbolHasZeroX()
    {
        ParseSvgFile("symbol-x-zero.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            LengthPercentage expected = new SvgLength(0);
            svgSymbol.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithMissingX_WhenSvgFileIsParsed_ThenSymbolHasNullX()
    {
        ParseSvgFile("symbol-x-missing.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.X.Should().BeNull();
        });
    }

    [Fact]
    public void HavingSymbolWithPercentageX_WhenSvgFileIsParsed_ThenSymbolHasThatX()
    {
        ParseSvgFile("symbol-x-percentage.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            LengthPercentage expected = new SvgPercentage(25);
            svgSymbol.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithPositiveXInPixels_WhenSvgFileIsParsed_ThenSymbolHasThatX()
    {
        ParseSvgFile("symbol-x-positivepx.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            LengthPercentage expected = new SvgLength(42, SvgLengthUnit.Pixels);
            svgSymbol.X.Should().Be(expected);
        });
    }
}