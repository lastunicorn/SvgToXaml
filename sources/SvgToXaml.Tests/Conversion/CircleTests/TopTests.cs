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

using System.Windows.Controls;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleTests;

public class TopTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithCenterY200AndRadius50_WhenSvgIsParsed_ThenResultedEllipseHasTop150()
    {
        TestConvertSvgFile("top-from-radius-smaller-than-y.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            double actualTop = Canvas.GetTop(ellipse);
            actualTop.Should().Be(150);
        });
    }

    [Fact]
    public void HavingCircleWithCenterY200AndRadius200_WhenSvgIsParsed_ThenResultedEllipseHasTopNaN()
    {
        TestConvertSvgFile("top-from-radius-equal-to-y.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            double actualTop = Canvas.GetTop(ellipse);
            actualTop.Should().Be(double.NaN);
        });
    }

    [Fact]
    public void HavingCircleWithCenterY200AndRadius400_WhenSvgIsParsed_ThenResultedEllipseHasTopNegative200()
    {
        TestConvertSvgFile("top-from-radius-greater-than-y.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            double actualTop = Canvas.GetTop(ellipse);
            actualTop.Should().Be(-200);
        });
    }
}