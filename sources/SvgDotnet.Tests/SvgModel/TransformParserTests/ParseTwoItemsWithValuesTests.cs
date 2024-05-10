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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.TransformParserTests;

public class ParseTwoItemsWithValuesTests
{
    private readonly TransformParser transformParser;

    public ParseTwoItemsWithValuesTests()
    {
        const string text = "func1(value1) func2(value2)";
        transformParser = new TransformParser(text);
    }

    [Fact]
    public void HavingStringWithTwoItemsWithValues_WhenMoveNextIsCalled_ThenReturnsTrue()
    {
        bool moveSuccess = transformParser.MoveNext();

        moveSuccess.Should().BeTrue();
    }

    [Fact]
    public void HavingStringWithTwoItemsWithValue_WhenMoveNextIsCalled_ThenCurrentContainsTheFirstItem()
    {
        transformParser.MoveNext();

        transformParser.Current.Key.Should().Be("func1");
        transformParser.Current.Value.Should().Be("value1");
    }

    [Fact]
    public void HavingStringWithTwoItemsWithValues_WhenMoveNextIsCalledTwice_ThenReturnsTrue()
    {
        transformParser.MoveNext();
        bool moveSuccess = transformParser.MoveNext();

        moveSuccess.Should().BeTrue();
    }

    [Fact]
    public void HavingStringWithTwoItemsWithValues_WhenMoveNextIsCalledTwice_ThenCurrentContainsTheSecondItem()
    {
        transformParser.MoveNext();
        transformParser.MoveNext();

        transformParser.Current.Key.Should().Be("func2");
        transformParser.Current.Value.Should().Be("value2");
    }

    [Fact]
    public void HavingStringWithTwoItemsWithValues_WhenMoveNextIsCalledThreeTimes_ThenReturnsFalse()
    {
        transformParser.MoveNext();
        transformParser.MoveNext();
        bool moveSuccess = transformParser.MoveNext();

        moveSuccess.Should().BeFalse();
    }
}