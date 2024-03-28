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

using System.Windows.Controls;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleTests;

public class LeftTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithCenterX300AndRadius50_WhenSvgIsParsed_ThenResultedEllipseHasLeft250()
    {
        TestConvertSvgFile("left-from-radius-smaller-than-x.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            double actualLeft = Canvas.GetLeft(ellipse);
            actualLeft.Should().Be(250);
        });
    }

    [Fact]
    public void HavingCircleWithCenterX300AndRadius300_WhenSvgIsParsed_ThenResultedEllipseHasLeftNaN()
    {
        TestConvertSvgFile("left-from-radius-equal-to-x.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            double actualLeft = Canvas.GetLeft(ellipse);
            actualLeft.Should().Be(double.NaN);
        });
    }

    [Fact]
    public void HavingCircleWithCenterX300AndRadius400_WhenSvgIsParsed_ThenResultedEllipseHasLeftNegative100()
    {
        TestConvertSvgFile("left-from-radius-greater-than-x.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            double actualLeft = Canvas.GetLeft(ellipse);
            actualLeft.Should().Be(-100);
        });
    }
}