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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.RadialGradientTests;

public class IdTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRadialGradientWithId_WhenSvgFileIsParsed_ThenSvgRadialGradientHasThatId()
    {
        ParseSvgFile("radialGradient-id.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;
            SvgRadialGradient svgRadialGradient = svgDefinitions.Children[0] as SvgRadialGradient;

            svgRadialGradient.Id.Should().Be("radialGradient1");
        });
    }

    [Fact]
    public void HavingRadialGradientWithNoId_WhenSvgFileIsParsed_ThenSvgRadialGradientHasIdNull()
    {
        ParseSvgFile("radialGradient-noid.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;
            SvgRadialGradient svgRadialGradient = svgDefinitions.Children[0] as SvgRadialGradient;

            svgRadialGradient.Id.Should().BeNull();
        });
    }
}
