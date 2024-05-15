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

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSymbolWithClassNotSpecified_WhenSvgFileIsParsed_ThenSymbolHasEmptyClassNames()
    {
        ParseSvgFile("symbol-class-missing.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingSymbolWithEmptyClass_WhenSvgFileIsParsed_ThenSymbolHasEmptyClassNames()
    {
        ParseSvgFile("symbol-class-empty.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingSymbolWithOneClass_WhenSvgFileIsParsed_ThenSymbolHasThatOneClassName()
    {
        ParseSvgFile("symbol-class.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            List<string> expected = new()
            {
                "class1"
            };
            svgSymbol.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingSymbolWithTwoClasses_WhenSvgFileIsParsed_ThenSymbolHasThoseTwoClassNames()
    {
        ParseSvgFile("symbol-2class.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgSymbol.ClassNames.Should().Equal(expected);
        });
    }
}