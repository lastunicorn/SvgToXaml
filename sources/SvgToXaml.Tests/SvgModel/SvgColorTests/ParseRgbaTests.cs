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

namespace DustInTheWind.SvgToXaml.Tests.SvgModel.SvgColorTests;

public class ParseRgbaTests
{
    private readonly SvgColor svgColor = SvgColor.Parse("#32b06f35");

    [Fact]
    public void WhenParsingRgbaText_ThenRedValueIsTheProvidedOne()
    {
        svgColor.Red.Should().Be(0x32);
    }

    [Fact]
    public void WhenParsingRgbaText_ThenGreenValueIsTheProvidedOne()
    {
        svgColor.Green.Should().Be(0xb0);
    }

    [Fact]
    public void WhenParsingRgbaText_ThenBlueValueIsTheProvidedOne()
    {
        svgColor.Blue.Should().Be(0x6f);
    }

    [Fact]
    public void WhenParsingRgbaText_ThenAlphaValueIsTheProvidedOne()
    {
        svgColor.Alpha.Should().Be(0x35);
    }

    [Fact]
    public void WhenParsingRgbaText_ThenIsEmptyIsFalse()
    {
        svgColor.IsEmpty.Should().BeFalse();
    }
}