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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleStrokeTests;

public class GroupUseHrefDefsGroupTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoStrokeDeclared_WhenSvgIsParsed_ThenResultedEllipseHasNullStroke()
    {
        ConvertSvgFile("01-group-use-href-defs-group-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().BeNull();
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("02-group-use-href-defs-group-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnGroupContainingCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromGroup()
    {
        ConvertSvgFile("03-group-use-href-defs-group^-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnGroupContainingCircleAndCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("04-group-use-href-defs-group^-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUseAndGroupContainingCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromUse()
    {
        ConvertSvgFile("05-group-use^-href-defs-group^-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUse_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromUse()
    {
        ConvertSvgFile("06-group-use^-href-defs-group-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff333333");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnGroupContainingUse_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromGroupContainingUse()
    {
        ConvertSvgFile("07-group^-use-href-defs-group-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff444444");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnGroupContainingUseAndGroupContainingCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromGroupContainingCircle()
    {
        ConvertSvgFile("08-group^-use-href-defs-group^-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnSvgRoot_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromGroupContainingCircle()
    {
        ConvertSvgFile("09-svgroot^-group-use-href-defs-group-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff555555");
        });
    }
}