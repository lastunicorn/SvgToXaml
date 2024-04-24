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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.SvgTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSvgWithClassNotSpecified_WhenSvgFileIsParsed_ThenSvgHasEmptyClassNames()
    {
        ParseSvgFile("svg-class-missing.svg", svg =>
        {
            svg.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingSvgWithEmptyClass_WhenSvgFileIsParsed_ThenSvgHasEmptyClassNames()
    {
        ParseSvgFile("svg-class-empty.svg", svg =>
        {
            svg.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingSvgWithOneClass_WhenSvgFileIsParsed_ThenSvgHasThatOneClassName()
    {
        ParseSvgFile("svg-class.svg", svg =>
        {
            List<string> expected = new()
            {
                "class1"
            };
            svg.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingSvgWithTwoClasses_WhenSvgFileIsParsed_ThenSvgHasThoseTwoClassNames()
    {
        ParseSvgFile("svg-2class.svg", svg =>
        {
            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svg.ClassNames.Should().Equal(expected);
        });
    }
}