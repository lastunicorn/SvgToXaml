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

public class TryParseTests
{
    [Fact]
    public void HavingStringContainingNumber10_WhenParsed_ThenValueIs10AndUnitIsNumber()
    {
        string text = "10";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(10);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingStringContainingNumberNegative10_WhenParsed_ThenValueIsNegative10AndUnitIsNumber()
    {
        string text = "-10";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(-10);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingStringContainingNumberZero_WhenParsed_ThenValueIsZeroAndUnitIsNumber()
    {
        string text = "0";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(0);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingEmptyString_WhenParsed_ThenValueIsZeroAndUnitIsNumber()
    {
        string text = "";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(0);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingStringContainingOneSpace_WhenParsed_ThenValueIsZeroAndUnitIsNumber()
    {
        string text = " ";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(0);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Number);
    }

    [Fact]
    public void HavingStringContaining10Percent_WhenParsed_ThenValueIs10AndUnitIsPercent()
    {
        string text = "10%";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(10);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Percentage);
    }

    [Fact]
    public void HavingStringContainingNegative10Percent_WhenParsed_ThenValueIsNegative10AndUnitIsPercent()
    {
        string text = "-10%";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(-10);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Percentage);
    }

    [Fact]
    public void HavingStringContainingZeroPercent_WhenParsed_ThenValueIsZeroAndUnitIsPercent()
    {
        string text = "0%";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeTrue();
        alphaValue.Value.Should().Be(0);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Percentage);
    }

    [Fact]
    public void HavingStringContainingInvalidValue_WhenParsed_ThenThrows()
    {
        string text = "abc";

        bool success = AlphaValue.TryParse(text, out AlphaValue alphaValue);

        success.Should().BeFalse();
        alphaValue.Value.Should().Be(0);
        alphaValue.Unit.Should().Be(AlphaValueUnit.Number);
    }
}