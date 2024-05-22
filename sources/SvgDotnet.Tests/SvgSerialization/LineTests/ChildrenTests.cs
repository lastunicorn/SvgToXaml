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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.LineTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenRectangleContainsDescription()
    {
        ParseSvgFile("line-desc.svg", svg =>
        {
            SvgLine svgLine = svg.Children[0] as SvgLine;

            svgLine.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenRectangleContainsTitle()
    {
        ParseSvgFile("line-title.svg", svg =>
        {
            SvgLine svgLine = svg.Children[0] as SvgLine;

            svgLine.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingLinearGradientChild_WhenSvgFileIsParsed_ThenLineContainsLinearGradient()
    {
        ParseSvgFile("line-lineargradient.svg", svg =>
        {
            SvgLine svgLine = svg.Children[0] as SvgLine;

            svgLine.Children[0].Should().BeOfType<SvgLinearGradient>();
        });
    }

    [Fact]
    public void HavingRadialGradientChild_WhenSvgFileIsParsed_ThenLineContainsRadialGradient()
    {
        ParseSvgFile("line-radialgradient.svg", svg =>
        {
            SvgLine svgLine = svg.Children[0] as SvgLine;

            svgLine.Children[0].Should().BeOfType<SvgRadialGradient>();
        });
    }

    [Fact]
    public void HavingClipPathChild_WhenSvgFileIsParsed_ThenLineContainsClipPath()
    {
        ParseSvgFile("line-clippath.svg", svg =>
        {
            SvgLine svgLine = svg.Children[0] as SvgLine;

            svgLine.Children[0].Should().BeOfType<SvgClipPath>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenRectangleContainsStyle()
    {
        ParseSvgFile("line-style.svg", svg =>
        {
            SvgLine svgLine = svg.Children[0] as SvgLine;

            svgLine.Children[0].Should().BeOfType<SvgStyle>();
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("line-invalid.svg", context =>
        {
            context.Warnings.Should().HaveCount(1);
        });
    }
}