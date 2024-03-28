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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleTests;

/*
 * |   M11     M12     0 |
 * |                     |
 * |   M21     M22     0 |
 * |                     |
 * | OffsetX OffsetY   1 |
 *
 * - OffsetX and OffsetY => Translate
 *
 */

public class CircleTransformTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithTransformMatrix_WhenSvgIsParsed_ThenResultedEllipseHasCorrectRenderTransformInformation()
    {
        TestConvertSvgFile("circle-transform.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.RenderTransform.Should().BeOfType<MatrixTransform>();

            MatrixTransform matrixTransform = ellipse.RenderTransform as MatrixTransform;
            matrixTransform.Matrix.M11.Should().Be(1);
            matrixTransform.Matrix.M12.Should().Be(2);
            matrixTransform.Matrix.M21.Should().Be(3);
            matrixTransform.Matrix.M22.Should().Be(4);
            matrixTransform.Matrix.OffsetX.Should().Be(5);
            matrixTransform.Matrix.OffsetY.Should().Be(6);
        });
    }
}