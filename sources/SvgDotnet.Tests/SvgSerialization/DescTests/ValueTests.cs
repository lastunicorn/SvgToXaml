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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.DescTests;

public class ValueTests : SvgFileTestsBase
{
    [Fact]
    public void HavingBodylessDescElement_WhenSvgIsParsed_ThenDescValueIsEmptyString()
    {
        ParseSvgFile("01-nobody.svg", svg =>
        {
            SvgDescription svgDescription = svg.Children[0] as SvgDescription;

            svgDescription.Value.Should().BeEmpty();
        });
    }

    [Fact]
    public void HavingEmptyDescElement_WhenSvgIsParsed_ThenDescValueIsEmptyString()
    {
        ParseSvgFile("02-empty.svg", svg =>
        {
            SvgDescription svgDescription = svg.Children[0] as SvgDescription;

            svgDescription.Value.Should().BeEmpty();
        });
    }

    [Fact]
    public void HavingDescElementWithOneTextLine_WhenSvgIsParsed_ThenDescValueIsTheExpectedOne()
    {
        ParseSvgFile("03-value-text.svg", svg =>
        {
            SvgDescription svgDescription = svg.Children[0] as SvgDescription;

            svgDescription.Value.Should().Be("this is a description");
        });
    }
}