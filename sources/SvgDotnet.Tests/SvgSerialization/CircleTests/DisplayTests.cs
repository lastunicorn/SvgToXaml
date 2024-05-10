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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.CircleTests;

public class DisplayTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithNoDisplay_WhenSvgFileIsParsed_ThenSvgContainsCircleWithDisplayNull()
    {
        ParseSvgFile("circle-display-missing.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.Display.Should().BeNull();
        });
    }

    [Fact]
    public void HavingCircleWithDisplayInline_WhenSvgFileIsParsed_ThenSvgContainsCircleWithDisplayInline()
    {
        ParseSvgFile("circle-display-inline.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.Display.Should().Be(Display.Inline);
        });
    }

    [Fact]
    public void HavingCircleWithDisplayNone_WhenSvgFileIsParsed_ThenSvgContainsCircleWithDisplayNone()
    {
        ParseSvgFile("circle-display-none.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.Display.Should().Be(Display.None);
        });
    }

    [Fact]
    public void HavingCircleInParentGWithDisplayNone_WhenSvgFileIsParsed_ThenSvgContainsCircleWithDisplayNull()
    {
        ParseSvgFile("g-display-none-circle-display-missing.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;
            SvgCircle svgCircle = svgGroup.Children[0] as SvgCircle;

            svgCircle.Display.Should().BeNull();
        });
    }

    [Fact]
    public void HavingCircleWithDisplayInlineInParentGWithDisplayNone_WhenSvgFileIsParsed_ThenSvgContainsCircleWithDisplayInline()
    {
        ParseSvgFile("g-display-none-circle-display-inline.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;
            SvgCircle svgCircle = svgGroup.Children[0] as SvgCircle;

            svgCircle.Display.Should().Be(Display.Inline);
        });
    }
}