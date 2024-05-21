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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SymbolTests;

public class WidthTests : SvgFileTestsBase
{
    [Fact]
    public void HavingWidthNotSpecified_WhenSvgIsParsed_ThenWidthIsNull()
    {
        ParseSvgFile("width-missing.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Width.Should().BeNull();
        });
    }

    [Fact]
    public void HavingWidthWithoutUnit_WhenSvgIsParsed_ThenWidthIsInUserUnits()
    {
        ParseSvgFile("width-nounit.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInCentimeters_WhenSvgIsParsed_ThenWidthIsInCentimeters()
    {
        ParseSvgFile("width-cm.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Centimeters);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInMillimeters_WhenSvgIsParsed_ThenWidthIsInMillimeters()
    {
        ParseSvgFile("width-mm.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Millimeters);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInQuarterMillimeters_WhenSvgIsParsed_ThenWidthIsInQuarterMillimeters()
    {
        ParseSvgFile("width-q.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.QuarterMillimeters);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInInches_WhenSvgIsParsed_ThenWidthIsInInches()
    {
        ParseSvgFile("width-in.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Inches);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInPicas_WhenSvgIsParsed_ThenWidthIsInPicas()
    {
        ParseSvgFile("width-pc.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Picas);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInPoints_WhenSvgIsParsed_ThenWidthIsInPoints()
    {
        ParseSvgFile("width-pt.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Points);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInPixels_WhenSvgIsParsed_ThenWidthIsInPixels()
    {
        ParseSvgFile("width-px.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Pixels);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInElementFontSize_WhenSvgIsParsed_ThenWidthIsInElementFontSize()
    {
        ParseSvgFile("width-em.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.ElementFontSize);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInElementFontWidth_WhenSvgIsParsed_ThenWidthIsInElementFontWidth()
    {
        ParseSvgFile("width-ex.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.ElementFontHeight);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInCharacterAdvanceOfZero_WhenSvgIsParsed_ThenWidthIsInCharacterAdvanceOfZero()
    {
        ParseSvgFile("width-ch.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.CharacterAdvanceOfZero);
            svgSymbol.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInRootElementFontSize_WhenSvgIsParsed_ThenWidthIsInRootElementFontSize()
    {
        ParseSvgFile("width-rem.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.RootElementFontSize);
            svgSymbol.Width.Should().Be(expected);
        });
    }
}