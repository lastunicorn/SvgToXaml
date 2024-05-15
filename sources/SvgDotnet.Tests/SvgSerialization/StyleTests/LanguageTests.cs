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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.StyleTests;

public class LanguageTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoLangAttribute_WhenSvgParsed_ThenLanguageIsNull()
    {
        ParseSvgFile("style-lang-missing.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            svgStyle.Language.Should().BeNull();
        });
    }

    [Fact]
    public void HavingNoXmlLangAttribute_WhenSvgParsed_ThenXmlLanguageIsNull()
    {
        ParseSvgFile("style-xmllang-missing.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            svgStyle.Language.Should().BeNull();
        });
    }

    [Fact]
    public void HavingLangAttributeOnSvgRoot_WhenSvgParsed_ThenSvgContainsCorrectLanguage()
    {
        ParseSvgFile("style-lang.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            svgStyle.Language.Should().Be("ro-RO");
        });
    }

    [Fact]
    public void HavingXmlLangAttributeOnSvgRoot_WhenSvgParsed_ThenSvgContainsCorrectXmlLanguage()
    {
        ParseSvgFile("style-xmllang.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            svgStyle.XmlLanguage.Should().Be("ro-RO");
        });
    }
}