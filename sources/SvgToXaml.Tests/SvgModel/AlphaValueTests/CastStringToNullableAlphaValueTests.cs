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

namespace DustInTheWind.SvgToXaml.Tests.SvgModel.AlphaValueTests;

public class CastStringToNullableAlphaValueTests
{
    [Fact]
    public void HavingStringContainingNumber10_WhenCastToAlphaValue_ThenValueIs10AndUnitIsNumber()
    {
        string text = "10";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(10);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingStringContainingNumberNegative10_WhenCastToAlphaValue_ThenValueIsNegative10AndUnitIsNumber()
    {
        string text = "-10";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(-10);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingStringContainingNumberZero_WhenCastToAlphaValue_ThenValueIsZeroAndUnitIsNumber()
    {
        string text = "0";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(0);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingEmptyString_WhenCastToAlphaValue_ThenValueIsZeroAndUnitIsNumber()
    {
        string text = "";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(0);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingStringContainingOneSpace_WhenCastToAlphaValue_ThenValueIsZeroAndUnitIsNumber()
    {
        string text = " ";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(0);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingNullString_WhenCastToAlphaValue_ThenAlphaValueIsNull()
    {
        string text = null;

        AlphaValue? alphaValue = text;

        alphaValue.Should().BeNull();
    }

    [Fact]
    public void HavingStringContaining10Percent_WhenCastToAlphaValue_ThenValueIs10AndUnitIsPercent()
    {
        string text = "10%";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(10);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Percentage);
    }

    [Fact]
    public void HavingStringContainingNegative10Percent_WhenCastToAlphaValue_ThenValueIsNegative10AndUnitIsPercent()
    {
        string text = "-10%";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(-10);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Percentage);
    }

    [Fact]
    public void HavingStringContainingZeroPercent_WhenCastToAlphaValue_ThenValueIsZeroAndUnitIsPercent()
    {
        string text = "0%";

        AlphaValue? alphaValue = text;

        alphaValue.Value.Value.Should().Be(0);
        alphaValue.Value.Unit.Should().Be(AlphaValueUnit.Percentage);
    }

    [Fact]
    public void HavingStringContainingInvalidValue_WhenCastToAlphaValue_ThenThrows()
    {
        string text = "abc";

        Action action = () =>
        {
            AlphaValue? alphaValue = text;
        };

        action.Should().Throw<ArgumentException>();
    }
}