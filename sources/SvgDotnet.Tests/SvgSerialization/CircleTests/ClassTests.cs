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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.CircleTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithClassNotSpecified_WhenSvgFileIsParsed_ThenSvgContainsCircleWithEmptyClassNames()
    {
        ParseSvgFile("circle-class-missing.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingCircleWithEmptyClass_WhenSvgFileIsParsed_ThenSvgContainsCircleWithEmptyClassNames()
    {
        ParseSvgFile("circle-class-empty.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingCircleWithOneClass_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatOneClassName()
    {
        ParseSvgFile("circle-class.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            List<string> expected = new()
            {
                "class1"
            };
            svgCircle.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingCircleWithTwoClasses_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThoseTwoClassNames()
    {
        ParseSvgFile("circle-2class.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgCircle.ClassNames.Should().Equal(expected);
        });
    }
}