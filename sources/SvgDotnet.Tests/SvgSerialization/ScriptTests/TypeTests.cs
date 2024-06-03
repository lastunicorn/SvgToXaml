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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.ScriptTests;

public class TypeTests : SvgFileTestsBase
{
    [Fact]
    public void HavingTypeNotSpecified_WhenSvgIsParsed_ThenTypeIsNull()
    {
        ParseSvgFile("type-missing.svg", svg =>
        {
            SvgScript svgScript = svg.Children[0] as SvgScript;

            svgScript.Type.Should().BeNull();
        });
    }

    [Fact]
    public void HavingTypeEcmascript_WhenSvgIsParsed_ThenTypeIsCss()
    {
        ParseSvgFile("type-ecmascript.svg", svg =>
        {
            SvgScript svgScript = svg.Children[0] as SvgScript;

            svgScript.Type.Should().Be("application/ecmascript");
        });
    }
}