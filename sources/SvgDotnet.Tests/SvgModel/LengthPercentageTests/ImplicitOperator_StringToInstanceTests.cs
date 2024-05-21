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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.LengthPercentageTests;

public class ImplicitOperator_StringToInstanceTests
{
    [Fact]
    public void HavingNullString_WhenCastToInstance_ThenReturnsEmptyInstance()
    {
        string text = null;

        LengthPercentage lengthPercentage = text;

        lengthPercentage.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void HavingEmptyString_WhenCastToInstance_ThenReturnsEmptyInstance()
    {
        string text = string.Empty;

        LengthPercentage lengthPercentage = text;

        lengthPercentage.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void HavingPositiveNumberAsString_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        string text = "24";

        LengthPercentage lengthPercentage = text;

        Length expected = new(24);
        lengthPercentage.Length.Should().Be(expected);
        lengthPercentage.Percentage.Should().BeNull();
    }

    [Fact]
    public void HavingNegativeNumberAsString_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        string text = "-24";

        LengthPercentage lengthPercentage = text;

        Length expected = new(-24);
        lengthPercentage.Length.Should().Be(expected);
        lengthPercentage.Percentage.Should().BeNull();
    }

    [Fact]
    public void HavingZeroNumberAsString_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        string text = "0";

        LengthPercentage lengthPercentage = text;

        Length expected = Length.Zero;
        lengthPercentage.Length.Should().Be(expected);
        lengthPercentage.Percentage.Should().BeNull();
    }

    [Fact]
    public void HavingPositivePixelNumberAsString_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        string text = "24px";

        LengthPercentage lengthPercentage = text;

        Length expected = new(24, SvgLengthUnit.Pixels);
        lengthPercentage.Length.Should().Be(expected);
        lengthPercentage.Percentage.Should().BeNull();
    }

    [Fact]
    public void HavingNegativePixelNumberAsString_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        string text = "-24px";

        LengthPercentage lengthPercentage = text;

        Length expected = new(-24, SvgLengthUnit.Pixels);
        lengthPercentage.Length.Should().Be(expected);
        lengthPercentage.Percentage.Should().BeNull();
    }

    [Fact]
    public void HavingZeroPixelNumberAsString_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        string text = "0px";

        LengthPercentage lengthPercentage = text;

        Length expected = new(0, SvgLengthUnit.Pixels);
        lengthPercentage.Length.Should().Be(expected);
        lengthPercentage.Percentage.Should().BeNull();
    }

    [Fact]
    public void HavingPositivePercentageAsString_WhenCastToInstance_ThenReturnsPercentageInstance()
    {
        string text = "12%";

        LengthPercentage lengthPercentage = text;

        SvgPercentage expected = new(12);
        lengthPercentage.Length.Should().BeNull();
        lengthPercentage.Percentage.Should().Be(expected);
    }

    [Fact]
    public void HavingNegativePercentageAsString_WhenCastToInstance_ThenReturnsPercentageInstance()
    {
        string text = "-12%";

        LengthPercentage lengthPercentage = text;

        SvgPercentage expected = new(-12);
        lengthPercentage.Length.Should().BeNull();
        lengthPercentage.Percentage.Should().Be(expected);
    }

    [Fact]
    public void HavingZeroPercentageAsString_WhenCastToInstance_ThenReturnsPercentageInstance()
    {
        string text = "0%";

        LengthPercentage lengthPercentage = text;

        SvgPercentage expected = new(0);
        lengthPercentage.Length.Should().BeNull();
        lengthPercentage.Percentage.Should().Be(expected);
    }
}