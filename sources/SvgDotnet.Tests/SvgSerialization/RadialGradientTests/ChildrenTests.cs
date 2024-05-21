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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.RadialGradientTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenRadialGradientContainsDescription()
    {
        ParseSvgFile("radialgradient-desc.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenRadialGradientContainsTitle()
    {
        ParseSvgFile("radialgradient-title.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingStopChild_WhenSvgFileIsParsed_ThenRadialGradientContainsStop()
    {
        ParseSvgFile("radialgradient-stop.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.Children[0].Should().BeOfType<SvgStop>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenRadialGradientContainsStyle()
    {
        ParseSvgFile("radialgradient-style.svg", svg =>
        {
            SvgRadialGradient svgRadialGradient = svg.Children[0] as SvgRadialGradient;

            svgRadialGradient.Children[0].Should().BeOfType<SvgStyle>();
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("radialgradient-invalid.svg", context =>
        {
            context.Warnings.Should().HaveCount(1);
        });
    }
}