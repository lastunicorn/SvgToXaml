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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleStrokeTests;

public class StrokeAttributeTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoStrokeDeclaredOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeNull()
    {
        ConvertSvgFile("circle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().BeNull();
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnSvgRootContainingCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromSvgRoot()
    {
        ConvertSvgFile("svgroot^-circle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff222222");
        });
    }

    [Fact]
    public void HavingStrokeDeclaredOnBothSvgRootAndOnCircle_WhenSvgIsParsed_ThenResultedEllipseHasStrokeColorFromCircle()
    {
        ConvertSvgFile("svgroot^-circle^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Stroke.Should().Be("#ff111111");
        });
    }
}