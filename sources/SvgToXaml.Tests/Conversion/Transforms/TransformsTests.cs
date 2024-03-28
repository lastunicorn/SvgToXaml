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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.Transforms;

public class TransformsTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithScaleTransform_WhenSvgIsParsed_ThenResultedEllipseHasOneScaleTransformWithCorrectValues()
    {
        TestConvertSvgFile("scale.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<ScaleTransform>();

            ScaleTransform scaleTransform = ellipse.RenderTransform as ScaleTransform;

            scaleTransform.ScaleX.Should().Be(-1);
            scaleTransform.ScaleY.Should().Be(2);
        });
    }

    [Fact]
    public void HavingCircleWithTranslateTransform_WhenSvgIsParsed_ThenResultedEllipseHasOneTranslateTransformWithCorrectValues()
    {
        TestConvertSvgFile("translate.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<TranslateTransform>();

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;

            translateTransform.X.Should().Be(-10);
            translateTransform.Y.Should().Be(20);
        });
    }

    [Fact]
    public void HavingCircleWithRotateTransform_WhenSvgIsParsed_ThenResultedEllipseHasOneRotateTransformWithCorrectValues()
    {
        TestConvertSvgFile("rotate.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<RotateTransform>();

            RotateTransform rotateTransform = ellipse.RenderTransform as RotateTransform;

            rotateTransform.CenterX.Should().Be(-10.4);
            rotateTransform.CenterY.Should().Be(20.9);
            rotateTransform.Angle.Should().Be(17.5);
        });
    }

    [Fact]
    public void HavingCircleWithMatrixTransform_WhenSvgIsParsed_ThenResultedEllipseHasOneMatrixTransformWithCorrectValues()
    {
        TestConvertSvgFile("matrix.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<MatrixTransform>();

            MatrixTransform matrixTransform = ellipse.RenderTransform as MatrixTransform;

            matrixTransform.Matrix.M11.Should().Be(1.1);
            matrixTransform.Matrix.M12.Should().Be(2.2);
            matrixTransform.Matrix.M21.Should().Be(3.3);
            matrixTransform.Matrix.M22.Should().Be(4.4);
            matrixTransform.Matrix.OffsetX.Should().Be(5.5);
            matrixTransform.Matrix.OffsetY.Should().Be(6.6);
        });
    }
}