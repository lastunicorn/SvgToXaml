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

public class HeightTests : SvgFileTestsBase
{
    [Fact]
    public void HavingHeightNotSpecified_WhenSvgIsParsed_ThenHeightIsNull()
    {
        ParseSvgFile("height-missing.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Height.Should().BeNull();
        });
    }

    [Fact]
    public void HavingHeightWithoutUnit_WhenSvgIsParsed_ThenHeightIsInUserUnits()
    {
        ParseSvgFile("height-nounit.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInCentimeters_WhenSvgIsParsed_ThenHeightIsInCentimeters()
    {
        ParseSvgFile("height-cm.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Centimeters);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInMillimeters_WhenSvgIsParsed_ThenHeightIsInMillimeters()
    {
        ParseSvgFile("height-mm.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Millimeters);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInQuarterMillimeters_WhenSvgIsParsed_ThenHeightIsInQuarterMillimeters()
    {
        ParseSvgFile("height-q.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.QuarterMillimeters);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInInches_WhenSvgIsParsed_ThenHeightIsInInches()
    {
        ParseSvgFile("height-in.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Inches);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInPicas_WhenSvgIsParsed_ThenHeightIsInPicas()
    {
        ParseSvgFile("height-pc.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Picas);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInPoints_WhenSvgIsParsed_ThenHeightIsInPoints()
    {
        ParseSvgFile("height-pt.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Points);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInPixels_WhenSvgIsParsed_ThenHeightIsInPixels()
    {
        ParseSvgFile("height-px.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.Pixels);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInElementFontSize_WhenSvgIsParsed_ThenHeightIsInElementFontSize()
    {
        ParseSvgFile("height-em.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.ElementFontSize);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInElementFontHeight_WhenSvgIsParsed_ThenHeightIsInElementFontHeight()
    {
        ParseSvgFile("height-ex.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.ElementFontHeight);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInCharacterAdvanceOfZero_WhenSvgIsParsed_ThenHeightIsInCharacterAdvanceOfZero()
    {
        ParseSvgFile("height-ch.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.CharacterAdvanceOfZero);
            svgSymbol.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInRootElementFontSize_WhenSvgIsParsed_ThenHeightIsInRootElementFontSize()
    {
        ParseSvgFile("height-rem.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            Length expected = new(123, SvgLengthUnit.RootElementFontSize);
            svgSymbol.Height.Should().Be(expected);
        });
    }
}