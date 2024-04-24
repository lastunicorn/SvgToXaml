﻿// SvgToXaml
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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.RadialGradientTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRadialGradientWithClassNotSpecified_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithEmptyClassNames()
    {
        ParseSvgFile("radialgradient-class-missing.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;
            SvgRadialGradient svgRadialGradient = svgDefinitions.Children[0] as SvgRadialGradient;

            svgRadialGradient.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingRadialGradientWithEmptyClass_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithEmptyClassNames()
    {
        ParseSvgFile("radialgradient-class-empty.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;
            SvgRadialGradient svgRadialGradient = svgDefinitions.Children[0] as SvgRadialGradient;

            svgRadialGradient.ClassNames.Should().HaveCount(0);
        });
    }

    [Fact]
    public void HavingRadialGradientWithOneClass_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithThatOneClassName()
    {
        ParseSvgFile("radialgradient-class.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;
            SvgRadialGradient svgRadialGradient = svgDefinitions.Children[0] as SvgRadialGradient;

            List<string> expected = new()
            {
                "class1"
            };
            svgRadialGradient.ClassNames.Should().Equal(expected);
        });
    }

    [Fact]
    public void HavingRadialGradientWithTwoClasses_WhenSvgFileIsParsed_ThenSvgContainsRadialGradientWithThoseTwoClassNames()
    {
        ParseSvgFile("radialgradient-2class.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;
            SvgRadialGradient svgRadialGradient = svgDefinitions.Children[0] as SvgRadialGradient;

            List<string> expected = new()
            {
                "class1",
                "class2"
            };
            svgRadialGradient.ClassNames.Should().Equal(expected);
        });
    }
}