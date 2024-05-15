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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.PathTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingPathWithClassNotSpecified_WhenSvgFileIsParsed_ThenPathHasEmptyClassNames()
    {
        ParseSvgFile("path-class-missing.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            svgPath.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingPathWithEmptyClass_WhenSvgFileIsParsed_ThenPathHasEmptyClassNames()
    {
        ParseSvgFile("path-class-empty.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            svgPath.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingPathWithOneClass_WhenSvgFileIsParsed_ThenPathHasThatOneClassName()
    {
        ParseSvgFile("path-class.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            List<string> expected = new()
            {
                "class1"
            };
            svgPath.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingPathWithTwoClasses_WhenSvgFileIsParsed_ThenPathHasThoseTwoClassNames()
    {
        ParseSvgFile("path-2class.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgPath.ClassNames.Should().Equal(expected);
        });
    }
}