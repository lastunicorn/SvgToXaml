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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgTests;

public class HeightTests : SvgFileTestsBase
{
    [Fact]
    public void HavingHeightNotSpecified_WhenSvgIsParsed_ThenHeightIsNull()
    {
        ParseSvgFile("height-missing.svg", svg =>
        {
            svg.Height.Should().BeNull();
        });
    }

    [Fact]
    public void HavingHeightWithoutUnit_WhenSvgIsParsed_ThenHeightIsInUserUnits()
    {
        ParseSvgFile("height-nounit.svg", svg =>
        {
            SvgLength expected = new(123);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInCentimeters_WhenSvgIsParsed_ThenHeightIsInCentimeters()
    {
        ParseSvgFile("height-cm.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Centimeters);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInMillimeters_WhenSvgIsParsed_ThenHeightIsInMillimeters()
    {
        ParseSvgFile("height-mm.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Millimeters);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInQuarterMillimeters_WhenSvgIsParsed_ThenHeightIsInQuarterMillimeters()
    {
        ParseSvgFile("height-q.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.QuarterMillimeters);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInInches_WhenSvgIsParsed_ThenHeightIsInInches()
    {
        ParseSvgFile("height-in.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Inches);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInPicas_WhenSvgIsParsed_ThenHeightIsInPicas()
    {
        ParseSvgFile("height-pc.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Picas);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInPoints_WhenSvgIsParsed_ThenHeightIsInPoints()
    {
        ParseSvgFile("height-pt.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Points);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInPixels_WhenSvgIsParsed_ThenHeightIsInPixels()
    {
        ParseSvgFile("height-px.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Pixels);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInElementFontSize_WhenSvgIsParsed_ThenHeightIsInElementFontSize()
    {
        ParseSvgFile("height-em.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.ElementFontSize);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInElementFontHeight_WhenSvgIsParsed_ThenHeightIsInElementFontHeight()
    {
        ParseSvgFile("height-ex.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.ElementFontHeight);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInCharacterAdvanceOfZero_WhenSvgIsParsed_ThenHeightIsInCharacterAdvanceOfZero()
    {
        ParseSvgFile("height-ch.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.CharacterAdvanceOfZero);
            svg.Height.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingHeightInRootElementFontSize_WhenSvgIsParsed_ThenHeightIsInRootElementFontSize()
    {
        ParseSvgFile("height-rem.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.RootElementFontSize);
            svg.Height.Should().Be(expected);
        });
    }
}