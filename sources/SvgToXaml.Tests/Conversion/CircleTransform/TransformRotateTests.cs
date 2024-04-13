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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleTransform;

public class TransformRotateTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithRotateAngle15_WhenSvgIsConverted_ThenResultedRectangleHasTranslateTransformAndRotateTransform()
    {
        ConvertSvgFile("01-transform-rotate-angle.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform.X.Should().Be(-30);
            translateTransform.Y.Should().Be(-30);

            RotateTransform rotateTransform = ellipse.GetTransform(1) as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(0);
            rotateTransform.CenterY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithRotateAngle15AndRotationPoint_WhenSvgIsConverted_ThenResultedRectangleHasTranslateTransformAndRotateTransform()
    {
        ConvertSvgFile("02-transform-rotate-angle-cx-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform.X.Should().Be(-30);
            translateTransform.Y.Should().Be(-30);

            RotateTransform rotateTransform = ellipse.GetTransform(1) as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(90);
            rotateTransform.CenterY.Should().Be(50);
        });
    }

    [Fact]
    public void HavingCircleWithCx150AndRotateAngle15_WhenSvgIsConverted_ThenResultedRectangleHasTranslateTransformAndRotateTransform()
    {
        ConvertSvgFile("03-circle-cx-transform-rotate.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform.X.Should().Be(120);
            translateTransform.Y.Should().Be(-30);

            RotateTransform rotateTransform = ellipse.GetTransform(1) as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(0);
            rotateTransform.CenterY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithCx150AndRotateAngle15AndRotateCx90_WhenSvgIsConverted_ThenResultedRectangleHasTranslateTransformAndRotateTransform()
    {
        ConvertSvgFile("04-circle-cx-transform-rotate-cx-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform.X.Should().Be(120);
            translateTransform.Y.Should().Be(-30);

            RotateTransform rotateTransform = ellipse.GetTransform(1) as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(90);
            rotateTransform.CenterY.Should().Be(50);
        });
    }

    [Fact]
    public void HavingCircleWithY120AndRotateAngle15_WhenSvgIsConverted_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        ConvertSvgFile("05-circle-cy-transform-rotate.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform.X.Should().Be(-30);
            translateTransform.Y.Should().Be(90);

            RotateTransform rotateTransform = ellipse.GetTransform(1) as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(0);
            rotateTransform.CenterY.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithY120AndTransformRotateWithYCenter90_WhenSvgIsConverted_ThenResultedRectangleHasTransformWithCorrectedRotationPoint()
    {
        ConvertSvgFile("06-circle-cy-transform-rotate-cx-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform.X.Should().Be(-30);
            translateTransform.Y.Should().Be(90);

            RotateTransform rotateTransform = ellipse.GetTransform(1) as RotateTransform;
            rotateTransform.Angle.Should().Be(15);
            rotateTransform.CenterX.Should().Be(90);
            rotateTransform.CenterY.Should().Be(50);
        });
    }
}