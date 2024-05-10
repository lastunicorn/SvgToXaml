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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.SvgColorTests;

public class ParseRgbTests
{
    private readonly SvgColor svgColor = SvgColor.Parse("rgb(100,40,27)");

    [Fact]
    public void WhenParsingRgbText_ThenRedValueIsTheProvidedOne()
    {
        svgColor.Red.Should().Be(100);
    }

    [Fact]
    public void WhenParsingRgbText_ThenGreenValueIsTheProvidedOne()
    {
        svgColor.Green.Should().Be(40);
    }

    [Fact]
    public void WhenParsingRgbText_ThenBlueValueIsTheProvidedOne()
    {
        svgColor.Blue.Should().Be(27);
    }

    [Fact]
    public void WhenParsingRgbText_ThenAlphaValueIsNull()
    {
        svgColor.Alpha.Should().BeNull();
    }

    [Fact]
    public void WhenParsingRgbText_ThenIsEmptyIsFalse()
    {
        svgColor.IsEmpty.Should().BeFalse();
    }
}