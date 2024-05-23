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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgElementTests;

public abstract class TabIndexTestsBase<T> : SvgFileTestsBase
    where T : SvgElement
{
    [Fact]
    public void HavingElementWithNoTabIndex_WhenSvgFileIsParsed_ThenElementHasTabIndexNull()
    {
        ParseSvgFile("tabindex-missing.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.TabIndex.Should().BeNull();
        });
    }

    [Fact]
    public void HavingElementWithPositiveTabIndex_WhenSvgFileIsParsed_ThenElementHasThatTabIndex()
    {
        ParseSvgFile("tabindex-positive.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.TabIndex.Should().Be(85);
        });
    }

    [Fact]
    public void HavingElementWithNegativeTabIndex_WhenSvgFileIsParsed_ThenElementHasThatTabIndex()
    {
        ParseSvgFile("tabindex-negative.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.TabIndex.Should().Be(-35);
        });
    }

    [Fact]
    public void HavingElementWithTabIndexZero_WhenSvgFileIsParsed_ThenElementHasTabIndexZero()
    {
        ParseSvgFile("tabindex-zero.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.TabIndex.Should().Be(0);
        });
    }

    [Fact]
    public void HavingElementWithLettersForTabIndex_WhenSvgFileIsParsed_ThenReturnsError()
    {
        ParseSvgFile("tabindex-nan.svg", result =>
        {
            result.Issues.Count.Should().Be(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Error);
        });
    }

    [Fact]
    public void HavingElementWithFloatTabIndex_WhenSvgFileIsParsed_ThenReturnsError()
    {
        ParseSvgFile("tabindex-float.svg", result =>
        {
            result.Issues.Count.Should().Be(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Error);
        });
    }

    protected virtual T SelectElementToTest(Svg svg)
    {
        return svg.Children[0] as T;
    }
}