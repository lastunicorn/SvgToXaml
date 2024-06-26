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

using System.Windows.Controls;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleStrokeTests;

public class UseGroupContainingCircleTests : SvgFileTestsBase
{
    [Fact]
    public void HavingStrokeDeclaredOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("01-use-href-defs-group-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnGroup_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromGroup()
    {
        ConvertSvgFile("02-use-href-defs-group^-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUse_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromUse()
    {
        ConvertSvgFile("03-use^-href-defs-group-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff333333");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnGroupAndOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("04-use-href-defs-group^-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUseAndOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("05-use^-href-defs-group-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUseAndOnGroup_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromGroup()
    {
        ConvertSvgFile("06-use^-href-defs-group^-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUseOnGroupAndOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("07-use^-href-defs-group^-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }
}