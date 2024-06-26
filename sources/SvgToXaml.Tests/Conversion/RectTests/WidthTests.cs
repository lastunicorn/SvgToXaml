﻿// SvgToXaml
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
using DustInTheWind.SvgToXaml.Conversion;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectTests;

public class WidthTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRectWithWidth0_WhenSvgIsParsed_ThenResultedRectangleHasHeight0()
    {
        ConvertSvgFile("width-0.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Width.Should().Be(0);
        });
    }

    [Fact]
    public void HavingRectWithHeight40_WhenSvgIsParsed_ThenResultedRectangleHasHeight40()
    {
        ConvertSvgFile("width-positive.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Width.Should().Be(40);
        });
    }

    [Fact]
    public void HavingRectWithHeightNegative40_WhenSvgIsParsed_ThenThrows()
    {
        Action action = () =>
        {
            ConvertSvgFile("width-negative.svg");
        };

        action.Should().Throw<StaEnvironmentException>()
            .WithInnerException<SvgConversionException>();
    }
}