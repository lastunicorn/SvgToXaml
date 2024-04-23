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

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.DefsTests;

public class IdTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDefsWithId_WhenSvgFileIsParsed_ThenSvgDefinitionsHasThatId()
    {
        ParseSvgFile("defs-id.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Id.Should().Be("defs1");
        });
    }

    [Fact]
    public void HavingDefsWithNoId_WhenSvgFileIsParsed_ThenSvgDefinitionsHasIdNull()
    {
        ParseSvgFile("defs-noid.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Id.Should().BeNull();
        });
    }
}
