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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectFillTests;

public class LiteralColorTests : SvgFileTestsBase
{
    [Fact]
    public void HavingFillSpecifiedAsRgbColorOf6Digits_WhenSvgIsParsed_ThenResultedRectangleHasCorrectFillColor()
    {
        ConvertSvgFile("rect-fill-rgb-full.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff123456");
        });
    }

    [Fact]
    public void HavingFillSpecifiedAsRgbColorOf3Digits_WhenSvgIsParsed_ThenResultedRectangleHasCorrectFillColor()
    {
        ConvertSvgFile("rect-fill-rgb-short.svg", canvas =>
        {
            Rectangle rectangles = canvas
                .GetElementByIndex<Rectangle>(0);

            rectangles.Fill.Should().Be("#ff112233");
        });
    }

    [Fact]
    public void HavingFillSpecifiedAsArgbColorOf8Digits_WhenSvgIsParsed_ThenResultedRectangleHasCorrectFillColor()
    {
        ConvertSvgFile("rect-fill-argb.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#78123456");
        });
    }
}