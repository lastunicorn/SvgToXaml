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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.EllipseTests;

public class RadiusXTests : SvgFileTestsBase
{
    [Fact]
    public void HavingEllipseWithRadiusX16_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusX16()
    {
        ParseSvgFile("ellipse-rx-positive.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusX.Should().Be(16);
        });
    }

    [Fact]
    public void HavingEllipseWithRadiusXNegative12_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusXZeroAndOneWarning()
    {
        ParseSvgFile("ellipse-rx-negative.svg", result =>
        {
            SvgEllipse svgEllipse = result.Svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusX.Should().Be(0);
            
            result.Warnings.Should().HaveCount(1);
            result.Warnings[0].Path.Should().Be("svg.(1)ellipse.@rx");
        });
    }

    [Fact]
    public void HavingEllipseWithRadiusXZero_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusXZero()
    {
        ParseSvgFile("ellipse-rx-zero.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusX.Should().Be(0);
        });
    }

    [Fact]
    public void HavingEllipseWithNoRadiusX_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusXZero()
    {
        ParseSvgFile("ellipse-rx-missing.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusX.Should().Be(0);
        });
    }
}