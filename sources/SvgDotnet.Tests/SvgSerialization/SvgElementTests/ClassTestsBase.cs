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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgElementTests;

public abstract class ClassTestsBase<T> : SvgFileTestsBase
    where T : SvgElement
{
    [Fact]
    public void HavingElementWithClassNotSpecified_WhenSvgFileIsParsed_ThenElementHasEmptyClassNames()
    {
        ParseSvgFile("01-class-missing.svg", svg =>
        {
            T svgElement = SelectElementToTest(svg);

            svgElement.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingElementWithEmptyClass_WhenSvgFileIsParsed_ThenElementHasEmptyClassNames()
    {
        ParseSvgFile("02-class-empty.svg", svg =>
        {
            T svgElement = SelectElementToTest(svg);

            svgElement.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingElementWithOneClass_WhenSvgFileIsParsed_ThenElementHasThatOneClassName()
    {
        ParseSvgFile("03-class-1.svg", svg =>
        {
            T svgElement = SelectElementToTest(svg);

            List<string> expected = new()
            {
                "class1"
            };
            svgElement.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingElementWithTwoClasses_WhenSvgFileIsParsed_ThenElementHasThoseTwoClassNames()
    {
        ParseSvgFile("04-class-2-space-1.svg", svg =>
        {
            T svgElement = SelectElementToTest(svg);

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgElement.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingElementWithTwoClassesSeparatedByTwoSpaces_WhenSvgFileIsParsed_ThenSvgContainsElementWithThoseTwoClassNames()
    {
        ParseSvgFile("05-class-2-space-2.svg", svg =>
        {
            T svgElement = SelectElementToTest(svg);

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgElement.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingElementWithTwoClassesSeparatedByOneTab_WhenSvgFileIsParsed_ThenSvgContainsElementWithThoseTwoClassNames()
    {
        ParseSvgFile("06-class-2-tab-1.svg", svg =>
        {
            T svgElement = SelectElementToTest(svg);

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgElement.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingElementWithTwoClassesSeparatedByTwoTabs_WhenSvgFileIsParsed_ThenSvgContainsElementWithThoseTwoClassNames()
    {
        ParseSvgFile("07-class-2-tab-2.svg", svg =>
        {
            T svgElement = SelectElementToTest(svg);

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgElement.ClassNames.Should().Equal(expected);
        });
    }

    protected virtual T SelectElementToTest(Svg svg)
    {
        return svg.Children[0] as T;
    }
}