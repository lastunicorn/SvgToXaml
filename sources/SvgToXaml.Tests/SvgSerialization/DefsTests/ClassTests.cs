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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.DefsTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDefsWithClassNotSpecified_WhenSvgFileIsParsed_ThenSvgContainsDefinitionsWithEmptyClassNames()
    {
        ParseSvgFile("defs-class-missing.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingDefsWithEmptyClass_WhenSvgFileIsParsed_ThenSvgContainsDefinitionsWithEmptyClassNames()
    {
        ParseSvgFile("defs-class-empty.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingDefsWithOneClass_WhenSvgFileIsParsed_ThenSvgContainsDefinitionsWithThatOneClassName()
    {
        ParseSvgFile("defs-class.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            List<string> expected = new()
            {
                "class1"
            };
            svgDefinitions.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingDefsWithTwoClasses_WhenSvgFileIsParsed_ThenSvgContainsDefinitionsWithThoseTwoClassNames()
    {
        ParseSvgFile("defs-2class.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgDefinitions.ClassNames.Should().Equal(expected);
        });
    }
}