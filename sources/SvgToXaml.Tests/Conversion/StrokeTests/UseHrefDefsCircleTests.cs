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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.StrokeTests;

public class UseHrefDefsCircleTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoStrokeDeclared_WhenSvgIsParsed_ThenResultedEllipseHasNullStroke()
    {
        ConvertSvgFile("00-use-href-defs-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().BeNull();
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("01-use-href-defs-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUse_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromUse()
    {
        ConvertSvgFile("02-use^-href-defs-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnUseAndOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("03-use^-href-defs-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnSvgRoot_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromSvgRoot()
    {
        ConvertSvgFile("04-svgroot^-use-href-defs-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff333333");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnSvgRootAndOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("05-svgroot^-use-href-defs-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnSvgRootAndOnUse_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromUse()
    {
        ConvertSvgFile("06-svgroot^-use^-href-defs-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnSvgRootOnUseAndOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("07-svgroot^-use^-href-defs-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }
}