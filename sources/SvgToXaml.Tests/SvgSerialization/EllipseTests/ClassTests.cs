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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.EllipseTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingEllipseWithClassNotSpecified_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithEmptyClassNames()
    {
        ParseSvgFile("ellipse-class-missing.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingEllipseWithEmptyClass_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithEmptyClassNames()
    {
        ParseSvgFile("ellipse-class-empty.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingEllipseWithOneClass_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithThatOneClassName()
    {
        ParseSvgFile("ellipse-class.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            List<string> expected = new()
            {
                "class1"
            };
            svgEllipse.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingEllipseWithTwoClasses_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithThoseTwoClassNames()
    {
        ParseSvgFile("ellipse-2class.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgEllipse.ClassNames.Should().Equal(expected);
        });
    }
}