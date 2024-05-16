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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.UseTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingUseElementWithClassNotSpecified_WhenSvgFileIsParsed_ThenUseHasEmptyClassNames()
    {
        ParseSvgFile("use-class-missing.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            svgUse.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingUseElementWithEmptyClass_WhenSvgFileIsParsed_ThenUseHasEmptyClassNames()
    {
        ParseSvgFile("use-class-empty.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            svgUse.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingUseElementWithOneClass_WhenSvgFileIsParsed_ThenUseHasThatOneClassName()
    {
        ParseSvgFile("use-class.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            List<string> expected = new()
            {
                "class1"
            };
            svgUse.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingUseElementWithTwoClasses_WhenSvgFileIsParsed_ThenUseHasThoseTwoClassNames()
    {
        ParseSvgFile("use-2class.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgUse.ClassNames.Should().Equal(expected);
        });
    }
}