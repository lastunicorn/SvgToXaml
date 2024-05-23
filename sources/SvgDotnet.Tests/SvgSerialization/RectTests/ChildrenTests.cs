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

using DustInTheWind.SvgDotnet.Serialization;

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.RectTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenRectangleContainsDescription()
    {
        ParseSvgFile("rect-desc.svg", svg =>
        {
            SvgRectangle svgRectangle = svg.Children[0] as SvgRectangle;

            svgRectangle.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenRectangleContainsTitle()
    {
        ParseSvgFile("rect-title.svg", svg =>
        {
            SvgRectangle svgRectangle = svg.Children[0] as SvgRectangle;

            svgRectangle.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingLinearGradientChild_WhenSvgFileIsParsed_ThenRectangleContainsLinearGradient()
    {
        ParseSvgFile("rect-lineargradient.svg", svg =>
        {
            SvgRectangle svgRectangle = svg.Children[0] as SvgRectangle;

            svgRectangle.Children[0].Should().BeOfType<SvgLinearGradient>();
        });
    }

    [Fact]
    public void HavingRadialGradientChild_WhenSvgFileIsParsed_ThenRectangleContainsRadialGradient()
    {
        ParseSvgFile("rect-radialgradient.svg", svg =>
        {
            SvgRectangle svgRectangle = svg.Children[0] as SvgRectangle;

            svgRectangle.Children[0].Should().BeOfType<SvgRadialGradient>();
        });
    }

    [Fact]
    public void HavingClipPathChild_WhenSvgFileIsParsed_ThenRectangleContainsClipPath()
    {
        ParseSvgFile("rect-clippath.svg", svg =>
        {
            SvgRectangle svgRectangle = svg.Children[0] as SvgRectangle;

            svgRectangle.Children[0].Should().BeOfType<SvgClipPath>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenRectangleContainsStyle()
    {
        ParseSvgFile("rect-style.svg", svg =>
        {
            SvgRectangle svgRectangle = svg.Children[0] as SvgRectangle;

            svgRectangle.Children[0].Should().BeOfType<SvgStyle>();
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("rect-invalid.svg", context =>
        {
            context.Issues.Should().HaveCount(1);
            context.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}