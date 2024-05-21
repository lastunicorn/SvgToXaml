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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.LengthPercentageTests;

public class ComputeValueTests
{
    [Fact]
    public void HavingEmptyInstance_WhenComputingTheValue_ThenReturnsZero()
    {
        LengthPercentage lengthPercentage = LengthPercentage.Empty;

        double computedValue = lengthPercentage.ComputeValue();

        computedValue.Should().Be(0.0);
    }

    [Fact]
    public void HavingInstanceContainingPositiveMillimetersLength_WhenComputingTheValue_ThenReturnsTheLengthValue()
    {
        Length length = new(63.4, SvgLengthUnit.Millimeters);
        LengthPercentage lengthPercentage = new(length);

        double computedValue = lengthPercentage.ComputeValue();

        computedValue.Should().Be(63.4);
    }

    [Fact]
    public void HavingInstanceContainingNegativeMillimetersLength_WhenComputingTheValue_ThenReturnsTheLengthValue()
    {
        Length length = new(-63.4, SvgLengthUnit.Millimeters);
        LengthPercentage lengthPercentage = new(length);

        double computedValue = lengthPercentage.ComputeValue();

        computedValue.Should().Be(-63.4);
    }

    [Fact]
    public void HavingInstanceContainingPositiveMillimetersPercentage_WhenComputingTheValue_ThenReturnsThePercentageValue()
    {
        SvgPercentage svgPercentage = new(75.1);
        LengthPercentage lengthPercentage = new(svgPercentage);

        double computedValue = lengthPercentage.ComputeValue();

        computedValue.Should().Be(75.1);
    }

    [Fact]
    public void HavingInstanceContainingNegativeMillimetersPercentage_WhenComputingTheValue_ThenReturnsThePercentageValue()
    {
        SvgPercentage svgPercentage = new(-75.1);
        LengthPercentage lengthPercentage = new(svgPercentage);

        double computedValue = lengthPercentage.ComputeValue();

        computedValue.Should().Be(-75.1);
    }
}