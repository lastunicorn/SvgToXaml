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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.SvgTests;

public class WidthTests : SvgFileTestsBase
{
    [Fact]
    public void HavingWidthNotSpecified_WhenSvgIsParsed_ThenWidthIsNull()
    {
        ParseSvgFile("width-missing.svg", svg =>
        {
            svg.Width.Should().BeNull();
        });
    }

    [Fact]
    public void HavingWidthWithoutUnit_WhenSvgIsParsed_ThenWidthIsInUserUnits()
    {
        ParseSvgFile("width-nounit.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Unspecified);
            svg.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInCentimeters_WhenSvgIsParsed_ThenWidthIsInCentimeters()
    {
        ParseSvgFile("width-cm.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Centimeters);
            svg.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInMillimeters_WhenSvgIsParsed_ThenWidthIsInMillimeters()
    {
        ParseSvgFile("width-mm.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Millimeters);
            svg.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInQuarterMillimeters_WhenSvgIsParsed_ThenWidthIsInQuarterMillimeters()
    {
        ParseSvgFile("width-q.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.QuarterMillimeters);
            svg.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInInches_WhenSvgIsParsed_ThenWidthIsInInches()
    {
        ParseSvgFile("width-in.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Inches);
            svg.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInPicas_WhenSvgIsParsed_ThenWidthIsInPicas()
    {
        ParseSvgFile("width-pc.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Picas);
            svg.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInPoints_WhenSvgIsParsed_ThenWidthIsInPoints()
    {
        ParseSvgFile("width-pt.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Points);
            svg.Width.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingWidthInPixels_WhenSvgIsParsed_ThenWidthIsInPixels()
    {
        ParseSvgFile("width-px.svg", svg =>
        {
            SvgLength expected = new(123, SvgLengthUnit.Pixels);
            svg.Width.Should().Be(expected);
        });
    }
}