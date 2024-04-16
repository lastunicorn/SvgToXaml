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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectFillTests;

public class UseGroupContainingCircleTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoFillDeclared_WhenSvgIsParsed_ThenResultedRectangleHasBlackFill()
    {
        ConvertSvgFile("01-group-use-href-defs-group-rect.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff000000");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnCircle_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromRect()
    {
        ConvertSvgFile("02-group-use-href-defs-group-rect^.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff333333");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnGroupContainingRect_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromGroup()
    {
        ConvertSvgFile("03-group-use-href-defs-group^-rect.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff666666");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnGroupContainingRect_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromRect()
    {
        ConvertSvgFile("04-group-use-href-defs-group^-rect^.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff333333");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnUseAndGroupContainingRect_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromUse()
    {
        ConvertSvgFile("05-group-use^-href-defs-group^-rect.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff666666");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnUse_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromUse()
    {
        ConvertSvgFile("06-group-use^-href-defs-group-rect.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff999999");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnGroupContainingUse_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromGroupContainingUse()
    {
        ConvertSvgFile("07-group^-use-href-defs-group-rect.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ffcccccc");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnGroupContainingUseAndGroupContainingRect_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromGroupContainingRect()
    {
        ConvertSvgFile("08-group^-use-href-defs-group^-rect.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ff666666");
        });
    }

    [Fact]
    public void HavingFillDeclaredOnSvgRoot_WhenSvgIsParsed_ThenResultedRectangleHasFillColorFromGroupContainingRect()
    {
        ConvertSvgFile("09-svgroot^-group-use-href-defs-group-rect.svg", canvas =>
        {
            Rectangle rectangle = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#ffffffff");
        });
    }
}