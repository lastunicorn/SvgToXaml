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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.StyleTests;

public class TypeTests : SvgFileTestsBase
{
    [Fact]
    public void HavingTypeNotSpecified_WhenSvgIsParsed_ThenTypeIsNull()
    {
        ParseSvgFile("type-missing.svg", svg =>
        {
            SvgStyleSheet svgStyleSheet = svg.Children[0] as SvgStyleSheet;

            svgStyleSheet.Type.Should().BeNull();
        });
    }

    [Fact]
    public void HavingTypeCss_WhenSvgIsParsed_ThenTypeIsCss()
    {
        ParseSvgFile("type-css.svg", svg =>
        {
            SvgStyleSheet svgStyleSheet = svg.Children[0] as SvgStyleSheet;

            svgStyleSheet.Type.Should().Be("text/css");
        });
    }

    [Fact]
    public void HavingTypeHtml_WhenSvgIsParsed_ThenTypeIsHtml()
    {
        ParseSvgFile("type-html.svg", svg =>
        {
            SvgStyleSheet svgStyleSheet = svg.Children[0] as SvgStyleSheet;

            svgStyleSheet.Type.Should().Be("text/html");
        });
    }
}