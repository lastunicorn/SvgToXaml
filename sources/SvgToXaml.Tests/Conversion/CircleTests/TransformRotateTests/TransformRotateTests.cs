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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleTests.TransformRotateTests;

public class TransformRotateTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithRotateAngle15_WhenSvgIsParsed_ThenResultedRectangleHasCorrectRenderTransformInformation()
    {
        TestConvertSvgFile("transform-rotate-angle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = ellipse.RenderTransform as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(30);
            rotateTransform.CenterY.Should().Be(30);
        });
    }

    [Fact]
    public void HavingCircleWithRotateAngle15AndRotationPoint_WhenSvgIsParsed_ThenResultedRectangleHasCorrectRenderTransformInformation()
    {
        TestConvertSvgFile("transform-rotate-angle-cx-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = ellipse.RenderTransform as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(120);
            rotateTransform.CenterY.Should().Be(80);
        });
    }

    [Fact]
    public void HavingCircleWithCx150AndRotateAngle15_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("circle-cx-transform-rotate.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = ellipse.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(-120);
            rotateTransform.CenterY.Should().Be(30);
        });
    }

    [Fact]
    public void HavingCircleWithCx150AndRotateAngle15AndRotateCx90_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("circle-cx-transform-rotate-cx-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = ellipse.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(-30);
            rotateTransform.CenterY.Should().Be(80);
        });
    }

    [Fact]
    public void HavingCircleWithY120AndRotateAngle15_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("circle-cy-transform-rotate.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = ellipse.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(30);
            rotateTransform.CenterY.Should().Be(-90);
        });
    }

    [Fact]
    public void HavingCircleWithY120AndTransformRotateWithYCenter90_WhenSvgIsParsed_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        TestConvertSvgFile("circle-cy-transform-rotate-cx-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = ellipse.RenderTransform as RotateTransform;
            rotateTransform.CenterX.Should().Be(120);
            rotateTransform.CenterY.Should().Be(-40);
        });
    }
}