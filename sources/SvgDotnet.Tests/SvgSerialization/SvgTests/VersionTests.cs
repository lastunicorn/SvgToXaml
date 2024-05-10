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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgTests;

public class VersionTests : SvgFileTestsBase
{
    [Fact]
    public void HavingVersionIsNotSpecified_WhenSvgIsParsed_ThenSvgVersionIsNull()
    {
        ParseSvgFile("version-missing.svg", svg =>
        {
            svg.Version.Should().BeNull();
        });
    }

    [Fact]
    public void HavingVersionIs11_WhenSvgIsParsed_ThenSvgVersionIs11()
    {
        ParseSvgFile("version-1-1.svg", svg =>
        {
            svg.Version.Should().Be("1.1");
        });
    }

    [Fact]
    public void HavingVersionIs2_WhenSvgIsParsed_ThenSvgVersionIs2()
    {
        ParseSvgFile("version-2.svg", svg =>
        {
            svg.Version.Should().Be("2");
        });
    }
}