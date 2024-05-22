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

using System.Windows.Markup;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleTests;

public class LanguageTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoLangOrXmlLangAttribute_WhenSvgIsConverted_ThenLanguageIsEnglish()
    {
        ConvertSvgFile("circle-lang-xmllang-missing.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            XmlLanguage expected = XmlLanguage.GetLanguage("en-US");
            ellipse.Language.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingOnlyLangAttribute_WhenSvgIsConverted_ThenLanguageHasLangValue()
    {
        ConvertSvgFile("circle-lang.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            XmlLanguage expected = XmlLanguage.GetLanguage("ro-RO");
            ellipse.Language.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingOnlyXmlLangAttribute_WhenSvgIsConverted_ThenLanguageHasXmlLangValue()
    {
        ConvertSvgFile("circle-xmllang.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            XmlLanguage expected = XmlLanguage.GetLanguage("ro-RO");
            ellipse.Language.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingBothLangAndXmlLangAttributes_WhenSvgIsConverted_ThenLanguageHasXmlLangValue()
    {
        ConvertSvgFile("circle-lang-xmllang.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            XmlLanguage expected = XmlLanguage.GetLanguage("fr-FR");
            ellipse.Language.Should().Be(expected);
        });
    }
}