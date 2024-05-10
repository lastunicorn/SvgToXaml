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

public class Constructor_SvgPercentagePositiveTests
{
    private readonly LengthPercentage lengthPercentage;

    public Constructor_SvgPercentagePositiveTests()
    {
        SvgPercentage svgPercentage = new(55.9);
        lengthPercentage = new LengthPercentage(svgPercentage);
    }

    [Fact]
    public void HavingPositivePercentage_WhenCreatingInstanceFromIt_ThenIsEmptyIsFalse()
    {
        lengthPercentage.IsEmpty.Should().BeFalse();
    }

    [Fact]
    public void HavingPositivePercentage_WhenCreatingInstanceFromIt_ThenLengthIsNull()
    {
        lengthPercentage.Length.Should().BeNull();
    }

    [Fact]
    public void HavingPositivePercentage_WhenCreatingInstanceFromIt_ThenPercentageIsProvidedValue()
    {
        SvgPercentage svgPercentage = new(55.9);
        lengthPercentage.Percentage.Should().Be(svgPercentage);
    }

    [Fact]
    public void HavingPositivePercentage_WhenCreatingInstanceFromIt_ThenIsNegativeIsFalse()
    {
        lengthPercentage.IsNegative.Should().BeFalse();
    }
}