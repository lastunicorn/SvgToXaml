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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleRenderTests;

public class RenderTests : SvgFileTestsBase
{
    [Fact]
    public void HavingOneCircleInSvgRoot_WhenSvgIsConverted_ThenOneEllipseIsCreated()
    {
        ConvertSvgFile("01-circle.svg", canvas =>
        {
            canvas.Children.Count.Should().Be(1);
            canvas.Children[0].Should().BeOfType<Ellipse>();
        });
    }

    [Fact]
    public void HavingTwoCirclesInSvgRoot_WhenSvgIsConverted_ThenTwoEllipsesAreCreated()
    {
        ConvertSvgFile("02-2circles.svg", canvas =>
        {
            canvas.Children.Count.Should().Be(2);
            canvas.Children[0].Should().BeOfType<Ellipse>();
            canvas.Children[1].Should().BeOfType<Ellipse>();
        });
    }

    [Fact]
    public void HavingOneCircleInGroup_WhenSvgIsConverted_ThenOneEllipseIsCreatedInGroup()
    {
        ConvertSvgFile("03-group-circle.svg", canvas =>
        {
            Canvas canvas2 = canvas.GetElementByIndex<Canvas>(0);

            canvas2.Children.Count.Should().Be(1);
            canvas2.Children[0].Should().BeOfType<Ellipse>();
        });
    }

    [Fact]
    public void HavingTwoCirclesInGroup_WhenSvgIsConverted_ThenTwoEllipsesAreCreatedInGroup()
    {
        ConvertSvgFile("04-group-2circles.svg", canvas =>
        {
            Canvas canvas2 = canvas.GetElementByIndex<Canvas>(0);

            canvas2.Children.Count.Should().Be(2);
            canvas2.Children[0].Should().BeOfType<Ellipse>();
            canvas2.Children[1].Should().BeOfType<Ellipse>();
        });
    }
}