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

public class CenterXTests : SvgFileTestsBase
{
    [Fact]
    public void HavingEllipseWithCenterX300_WhenSvgParsed_ThenSvgContainsEllipseWithCenterX300()
    {
        ParseSvgFile("ellipse-cx-positive.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterX.Should().Be(300);
        });
    }

    [Fact]
    public void HavingEllipseWithCenterXNegative300_WhenSvgParsed_ThenSvgContainsEllipseWithCenterXNegative300()
    {
        ParseSvgFile("ellipse-cx-negative.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterX.Should().Be(-300);
        });
    }

    [Fact]
    public void HavingEllipseWithCenterXZero_WhenSvgParsed_ThenSvgContainsEllipseWithCenterXZero()
    {
        ParseSvgFile("ellipse-cx-zero.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterX.Should().Be(0);
        });
    }

    [Fact]
    public void HavingEllipseWithMissingCenterX_WhenSvgParsed_ThenSvgContainsEllipseWithCenterXZero()
    {
        ParseSvgFile("ellipse-cx-missing.svg", svg =>
        {
            SvgEllipse svgEllipse = svg.Children[0] as SvgEllipse;

            svgEllipse.CenterX.Should().Be(0);
        });
    }
}