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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.EllipseTests;

public class RadiusYTests : SvgFileTestsBase
{
    [Fact]
    public void HavingEllipseWithRadiusY16_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusY16()
    {
        ParseSvgFile("ellipse-ry-positive.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusY.Should().Be(16);
        });
    }

    [Fact]
    public void HavingEllipseWithRadiusYNegative12_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusYZeroAndOneWarning()
    {
        ParseSvgFile("ellipse-ry-negative.svg", result =>
        {
            SvgEllipse svgEllipse = result.Svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusY.Should().Be(0);

            result.Warnings.Should().HaveCount(1);
            result.Warnings[0].Path.Should().Be("svg.(1)ellipse.@ry");
        });
    }

    [Fact]
    public void HavingEllipseWithRadiusYZero_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusYZero()
    {
        ParseSvgFile("ellipse-ry-zero.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingEllipseWithNoRadiusY_WhenSvgFileIsParsed_ThenSvgContainsEllipseWithRadiusYZero()
    {
        ParseSvgFile("ellipse-ry-missing.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.RadiusY.Should().Be(0);
        });
    }
}