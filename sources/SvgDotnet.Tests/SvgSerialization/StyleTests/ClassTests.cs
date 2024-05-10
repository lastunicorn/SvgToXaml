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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.StyleTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingStyleWithClassNotSpecified_WhenSvgFileIsParsed_ThenSvgContainsStyleWithEmptyClassNames()
    {
        ParseSvgFile("style-class-missing.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            svgStyle.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingStyleWithEmptyClass_WhenSvgFileIsParsed_ThenSvgContainsStyleWithEmptyClassNames()
    {
        ParseSvgFile("style-class-empty.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            svgStyle.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingStyleWithOneClass_WhenSvgFileIsParsed_ThenSvgContainsStyleWithThatOneClassName()
    {
        ParseSvgFile("style-class.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            List<string> expected = new()
            {
                "class1"
            };
            svgStyle.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingStyleWithTwoClasses_WhenSvgFileIsParsed_ThenSvgContainsStyleWithThoseTwoClassNames()
    {
        ParseSvgFile("style-2class.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgStyle.ClassNames.Should().Equal(expected);
        });
    }
}