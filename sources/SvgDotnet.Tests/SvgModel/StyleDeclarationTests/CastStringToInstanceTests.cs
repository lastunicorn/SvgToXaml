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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.StyleDeclarationTests;

public class CastStringToInstanceTests
{
    [Fact]
    public void HavingNullString_WhenCast_ThenReturnsNull()
    {
        StyleDeclaration styleDeclaration = (string)null;

        styleDeclaration.Should().BeNull();
    }

    [Fact]
    public void HavingEmptyString_WhenCast_ThenReturnsNull()
    {
        StyleDeclaration styleDeclaration = "";

        styleDeclaration.Should().BeNull();
    }

    [Fact]
    public void HavingStringContainingOnlyName_WhenCast_ThenReturnsNull()
    {
        StyleDeclaration styleDeclaration = "fill";

        styleDeclaration.Should().BeNull();
    }

    [Fact]
    public void HavingStringContainingNameAndValue_WhenCast_ThenReturnsInstanceContainingThatNameAndValue()
    {
        StyleDeclaration styleDeclaration = "fill:#ff0000";

        styleDeclaration.Name.Should().Be("fill");
        styleDeclaration.Value.Should().Be("#ff0000");
    }

    [Fact]
    public void HavingStringContainingNameAndValueAndSpaces_WhenCast_ThenReturnsInstanceContainingThatNameAndValueWithoutSpaces()
    {
        StyleDeclaration styleDeclaration = " fill : #ff0000 ";

        styleDeclaration.Name.Should().Be("fill");
        styleDeclaration.Value.Should().Be("#ff0000");
    }
}