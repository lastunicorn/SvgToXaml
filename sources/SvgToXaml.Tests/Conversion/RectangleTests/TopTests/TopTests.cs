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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectangleTests.TopTests;

public class TopTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRectWithY0_WhenSvgIsParsed_ThenResultedRectangleNoTop()
    {
        TestConvertSvgFile("top-0.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            double actualLeft = Canvas.GetTop(rectangle);
            actualLeft.Should().Be(double.NaN);
        });
    }

    [Fact]
    public void HavingRectWithAbsentY_WhenSvgIsParsed_ThenResultedRectangleNoTop()
    {
        TestConvertSvgFile("top-absent.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            double actualLeft = Canvas.GetTop(rectangle);
            actualLeft.Should().Be(double.NaN);
        });
    }

    [Fact]
    public void HavingRectWithY250_WhenSvgIsParsed_ThenResultedRectangleHasTop250()
    {
        TestConvertSvgFile("top-positive.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            double actualLeft = Canvas.GetTop(rectangle);
            actualLeft.Should().Be(250);
        });
    }

    [Fact]
    public void HavingRectWithYNegative250_WhenSvgIsParsed_ThenResultedRectangleHasTopNegative250()
    {
        TestConvertSvgFile("top-negative.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            double actualLeft = Canvas.GetTop(rectangle);
            actualLeft.Should().Be(-250);
        });
    }
}