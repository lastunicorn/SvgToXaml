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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.GTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingGWithClassNotSpecified_WhenSvgFileIsParsed_ThenSvgContainsGroupWithEmptyClassNames()
    {
        ParseSvgFile("g-class-missing.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingGWithEmptyClass_WhenSvgFileIsParsed_ThenSvgContainsGroupWithEmptyClassNames()
    {
        ParseSvgFile("g-class-empty.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingGWithOneClass_WhenSvgFileIsParsed_ThenSvgContainsGroupWithThatOneClassName()
    {
        ParseSvgFile("g-class.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            List<string> expected = new()
            {
                "class1"
            };
            svgGroup.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingGWithTwoClasses_WhenSvgFileIsParsed_ThenSvgContainsGroupWithThoseTwoClassNames()
    {
        ParseSvgFile("g-2class.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgGroup.ClassNames.Should().Equal(expected);
        });
    }
}