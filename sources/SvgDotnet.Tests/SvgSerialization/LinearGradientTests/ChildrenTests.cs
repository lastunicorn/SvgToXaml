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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.LinearGradientTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenLinearGradientContainsDescription()
    {
        ParseSvgFile("lineargradient-desc.svg", svg =>
        {
            SvgLinearGradient svgLinearGradient = svg.Children[0] as SvgLinearGradient;

            svgLinearGradient.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenLinearGradientContainsTitle()
    {
        ParseSvgFile("lineargradient-title.svg", svg =>
        {
            SvgLinearGradient svgLinearGradient = svg.Children[0] as SvgLinearGradient;

            svgLinearGradient.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingStopChild_WhenSvgFileIsParsed_ThenLinearGradientContainsStop()
    {
        ParseSvgFile("lineargradient-stop.svg", svg =>
        {
            SvgLinearGradient svgLinearGradient = svg.Children[0] as SvgLinearGradient;

            svgLinearGradient.Children[0].Should().BeOfType<SvgStop>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenLinearGradientContainsStyle()
    {
        ParseSvgFile("lineargradient-style.svg", svg =>
        {
            SvgLinearGradient svgLinearGradient = svg.Children[0] as SvgLinearGradient;

            svgLinearGradient.Children[0].Should().BeOfType<SvgStyle>();
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("lineargradient-invalid.svg", context =>
        {
            context.Issues.Should().HaveCount(1);
            context.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}