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

namespace DustInTheWind.SvgToXaml.Tests.SvgModel.TransformParserTests;

public class ParseOneItemWithValueTests
{
    private readonly TransformParser transformParser;

    public ParseOneItemWithValueTests()
    {
        const string text = "func1(value1)";
        transformParser = new TransformParser(text);
    }

    [Fact]
    public void HavingStringWithOneItemWithValue_WhenMoveNextIsCalled_ThenReturnsTrue()
    {
        bool moveSuccess = transformParser.MoveNext();

        moveSuccess.Should().BeTrue();
    }

    [Fact]
    public void HavingStringWithOneItemWithValue_WhenMoveNextIsCalled_ThenCurrentContainsTheItemAndValue()
    {
        transformParser.MoveNext();

        transformParser.Current.Key.Should().Be("func1");
        transformParser.Current.Value.Should().Be("value1");
    }

    [Fact]
    public void HavingStringWithOneItemWithValue_WhenMoveNextIsCalledTwice_ThenReturnsFalse()
    {
        transformParser.MoveNext();
        bool moveSuccess = transformParser.MoveNext();

        moveSuccess.Should().BeFalse();
    }
}