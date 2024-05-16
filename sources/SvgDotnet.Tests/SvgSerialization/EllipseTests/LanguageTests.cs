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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.EllipseTests;

public class LanguageTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoLangAttribute_WhenSvgParsed_ThenLanguageIsNull()
    {
        ParseSvgFile("ellipse-lang-missing.svg", svg =>
        {
            SvgEllipse ellipse = svg.Children[0] as SvgEllipse;

            ellipse.Language.Should().BeNull();
        });
    }

    [Fact]
    public void HavingNoXmlLangAttribute_WhenSvgParsed_ThenXmlLanguageIsNull()
    {
        ParseSvgFile("ellipse-xmllang-missing.svg", svg =>
        {
            SvgEllipse ellipse = svg.Children[0] as SvgEllipse;

            ellipse.Language.Should().BeNull();
        });
    }

    [Fact]
    public void HavingLangAttribute_WhenSvgParsed_ThenEllipseContainsCorrectLanguage()
    {
        ParseSvgFile("ellipse-lang.svg", svg =>
        {
            SvgEllipse ellipse = svg.Children[0] as SvgEllipse;

            ellipse.Language.Should().Be("ro-RO");
        });
    }

    [Fact]
    public void HavingXmlLangAttribute_WhenSvgParsed_ThenEllipseContainsCorrectXmlLanguage()
    {
        ParseSvgFile("ellipse-xmllang.svg", svg =>
        {
            SvgEllipse ellipse = svg.Children[0] as SvgEllipse;

            ellipse.XmlLanguage.Should().Be("ro-RO");
        });
    }
}