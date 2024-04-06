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

using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectangleTests.TransformRotateTests;

public class TransformRotateTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRectWithTransformRotateHavingAngle_WhenSvgIsParsed_ThenResultedRectangleHasCorrectRenderTransformInformation()
    {
        TestConvertSvgFile("transform-rotate-angle.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = rectangle.RenderTransform as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(0);
            rotateTransform.CenterY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingRectWithTransformRotateHavingAngleAndRotationPoint_WhenSvgIsParsed_ThenResultedRectangleHasCorrectRenderTransformInformation()
    {
        TestConvertSvgFile("transform-rotate-angle-cx-cy.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = rectangle.RenderTransform as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(90);
            rotateTransform.CenterY.Should().Be(50);
        });
    }

    [Fact]
    public void HavingRectWithX150AndTransformRotate_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("rect-x-transform-rotate.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = rectangle.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(-150);
            rotateTransform.CenterY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingRectWithX150AndTransformRotateWithXCenter90_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("rect-x-transform-rotate-cx-cy.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = rectangle.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(-60);
            rotateTransform.CenterY.Should().Be(50);
        });
    }

    [Fact]
    public void HavingRectWithY120AndTransformRotate_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("rect-y-transform-rotate.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = rectangle.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(0);
            rotateTransform.CenterY.Should().Be(-120);
        });
    }

    [Fact]
    public void HavingRectWithY120AndTransformRotateWithYCenter90_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("rect-y-transform-rotate-cx-cy.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = rectangle.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(90);
            rotateTransform.CenterY.Should().Be(-70);
        });
    }
}