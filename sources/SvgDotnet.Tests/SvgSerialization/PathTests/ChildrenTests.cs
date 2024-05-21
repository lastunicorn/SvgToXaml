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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.PathTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenPathContainsDescription()
    {
        ParseSvgFile("path-desc.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            svgPath.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenPathContainsTitle()
    {
        ParseSvgFile("path-title.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            svgPath.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenPathContainsStyle()
    {
        ParseSvgFile("path-style.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            svgPath.Children[0].Should().BeOfType<SvgStyle>();
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("path-invalid.svg", context =>
        {
            context.Warnings.Should().HaveCount(1);
        });
    }
}