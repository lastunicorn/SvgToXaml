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

public class CenterYTests : SvgFileTestsBase
{
    [Fact]
    public void HavingEllipseWithCenterY300_WhenSvgParsed_ThenSvgContainsEllipseWithCenterY300()
    {
        ParseSvgFile("ellipse-cy-positive.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterY.Should().Be(300);
        });
    }

    [Fact]
    public void HavingEllipseWithCenterYNegative300_WhenSvgParsed_ThenSvgContainsEllipseWithCenterYNegative300()
    {
        ParseSvgFile("ellipse-cy-negative.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterY.Should().Be(-300);
        });
    }

    [Fact]
    public void HavingEllipseWithCenterYZero_WhenSvgParsed_ThenSvgContainsEllipseWithCenterYZero()
    {
        ParseSvgFile("ellipse-cy-zero.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingEllipseWithMissingCenterY_WhenSvgParsed_ThenSvgContainsEllipseWithCenterYZero()
    {
        ParseSvgFile("ellipse-cy-missing.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterY.Should().Be(0);
        });
    }
}