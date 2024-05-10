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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.GTests;

public class IdTests : SvgFileTestsBase
{
    [Fact]
    public void HavingGWithId_WhenSvgFileIsParsed_ThenSvgGroupHasThatId()
    {
        ParseSvgFile("g-id.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Id.Should().Be("g1");
        });
    }

    [Fact]
    public void HavingGWithNoId_WhenSvgFileIsParsed_ThenSvgGroupHasIdNull()
    {
        ParseSvgFile("g-noid.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Id.Should().BeNull();
        });
    }
}