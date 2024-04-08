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

using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.PolylineTests;

public class PointsTests : SvgFileTestsBase
{
    [Fact]
    public void HavingPolylineWith1Point_WhenSvgIsParsed_ThenResultedPolylineHas1Point()
    {
        ConvertSvgFile("polyline-points-1.svg", canvas =>
        {
            Polyline polyline = canvas.GetElementByIndex<Polyline>(0);

            polyline.Points.Should().HaveCount(1);
            polyline.Points[0].X.Should().Be(435.026);
            polyline.Points[0].Y.Should().Be(543.2);
        });
    }

    [Fact]
    public void HavingPolylineWith2Points_WhenSvgIsParsed_ThenResultedPolylineHas2Points()
    {
        ConvertSvgFile("polyline-points-2.svg", canvas =>
        {
            Polyline polyline = canvas.GetElementByIndex<Polyline>(0);

            polyline.Points.Should().HaveCount(2);
            polyline.Points[0].X.Should().Be(435.026);
            polyline.Points[0].Y.Should().Be(543.2);
            polyline.Points[1].X.Should().Be(392.102);
            polyline.Points[1].Y.Should().Be(551.987);
        });
    }

    [Fact]
    public void HavingPolylineWith3Points_WhenSvgIsParsed_ThenResultedPolylineHas3Points()
    {
        ConvertSvgFile("polyline-points-3.svg", canvas =>
        {
            Polyline polyline = canvas.GetElementByIndex<Polyline>(0);

            polyline.Points.Should().HaveCount(3);
            polyline.Points[0].X.Should().Be(435.026);
            polyline.Points[0].Y.Should().Be(543.2);
            polyline.Points[1].X.Should().Be(392.102);
            polyline.Points[1].Y.Should().Be(551.987);
            polyline.Points[2].X.Should().Be(391.925);
            polyline.Points[2].Y.Should().Be(551.987);
        });
    }
}