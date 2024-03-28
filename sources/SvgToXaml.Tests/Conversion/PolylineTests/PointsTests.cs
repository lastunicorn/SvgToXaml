// Country Flags
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

using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.PolylineTests;

public class PointsTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithRadius0_WhenSvgIsParsed_ThenResultedEllipseHasWidth0()
    {
        TestConvertSvgFile("points-1.svg", canvas =>
        {
            Polyline polyline = canvas.GetElementByIndex<Polyline>(0);

            polyline.Points.Should().HaveCount(1);
        });
    }

    [Fact]
    public void HavingCircleWithRadius50_WhenSvgIsParsed_ThenResultedEllipseHasWidth100()
    {
        TestConvertSvgFile("points-2.svg", canvas =>
        {
            Polyline polyline = canvas.GetElementByIndex<Polyline>(0);

            polyline.Points.Should().HaveCount(2);
        });
    }

    [Fact]
    public void HavingCircleWithRadiusMinus50_WhenSvgIsParsed_ThenThrows()
    {
        TestConvertSvgFile("points-3.svg", canvas =>
        {
            Polyline polyline = canvas.GetElementByIndex<Polyline>(0);

            polyline.Points.Should().HaveCount(3);
        });
    }
}