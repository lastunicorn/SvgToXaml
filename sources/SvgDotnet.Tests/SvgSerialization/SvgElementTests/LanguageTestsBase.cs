﻿// SvgToXaml
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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgElementTests;

public abstract class LanguageTestsBase<T> : SvgFileTestsBase
    where T : SvgElement
{
    [Fact]
    public void HavingNoLangAttribute_WhenSvgParsed_ThenLanguageIsNull()
    {
        ParseSvgFile("lang-missing.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.Language.Should().BeNull();
        });
    }

    [Fact]
    public void HavingNoXmlLangAttribute_WhenSvgParsed_ThenXmlLanguageIsNull()
    {
        ParseSvgFile("xmllang-missing.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.Language.Should().BeNull();
        });
    }

    [Fact]
    public void HavingLangAttribute_WhenSvgParsed_ThenElementContainsCorrectLanguage()
    {
        ParseSvgFile("lang.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.Language.Should().Be("ro-RO");
        });
    }

    [Fact]
    public void HavingXmlLangAttribute_WhenSvgParsed_ThenElementContainsCorrectLanguage()
    {
        ParseSvgFile("xmllang.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.Language.Should().Be("de-DE");
        });
    }

    [Fact]
    public void HavingBothLangAndXmlLangAttributes_WhenSvgParsed_ThenElementContainsLanguageValueFromXmlLang()
    {
        ParseSvgFile("lang-and-xmllang.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.Language.Should().Be("de-DE");
        });
    }

    protected virtual T SelectElementToTest(Svg svg)
    {
        return svg.Children[0] as T;
    }
}