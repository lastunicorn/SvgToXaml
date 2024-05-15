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

public class IdTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSymbolWithId_WhenSvgFileIsParsed_ThenSymbolHasThatId()
    {
        ParseSvgFile("symbol-id.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Id.Should().Be("symbol1");
        });
    }

    [Fact]
    public void HavingSymbolWithNoId_WhenSvgFileIsParsed_ThenSymbolHasIdNull()
    {
        ParseSvgFile("symbol-id-missing.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Id.Should().BeNull();
        });
    }
}