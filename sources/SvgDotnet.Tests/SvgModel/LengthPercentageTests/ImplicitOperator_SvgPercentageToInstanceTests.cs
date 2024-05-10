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

public class ImplicitOperator_SvgPercentageToInstanceTests
{
    [Fact]
    public void HavingPositiveLengthInCentimeters_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        SvgPercentage svgPercentage = new(43.2);

        LengthPercentage lengthPercentage = svgPercentage;

        SvgPercentage expected = new(43.2);
        lengthPercentage.Length.Should().BeNull();
        lengthPercentage.Percentage.Should().Be(expected);
    }

    [Fact]
    public void HavingNegativeLengthInCentimeters_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        SvgPercentage svgPercentage = new(-43.2);

        LengthPercentage lengthPercentage = svgPercentage;

        SvgPercentage expected = new(-43.2);
        lengthPercentage.Length.Should().BeNull();
        lengthPercentage.Percentage.Should().Be(expected);
    }

    [Fact]
    public void HavingZeroLengthInCentimeters_WhenCastToInstance_ThenReturnsLengthInstance()
    {
        SvgPercentage svgPercentage = new(0);

        LengthPercentage lengthPercentage = svgPercentage;

        SvgPercentage expected = new(0);
        lengthPercentage.Length.Should().BeNull();
        lengthPercentage.Percentage.Should().Be(expected);
    }
}