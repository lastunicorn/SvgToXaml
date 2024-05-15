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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.PathTests;

public class IdTests : SvgFileTestsBase
{
    [Fact]
    public void HavingPathWithId_WhenSvgFileIsParsed_ThenPathHasThatId()
    {
        ParseSvgFile("path-id.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            svgPath.Id.Should().Be("path1");
        });
    }

    [Fact]
    public void HavingPathWithNoId_WhenSvgFileIsParsed_ThenPathHasIdNull()
    {
        ParseSvgFile("path-id-missing.svg", svg =>
        {
            SvgPath svgPath = svg.Children[0] as SvgPath;

            svgPath.Id.Should().BeNull();
        });
    }
}