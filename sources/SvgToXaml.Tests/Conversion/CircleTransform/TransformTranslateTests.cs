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

public class TransformTranslateTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithTranslateValuesPositive_WhenSvgIsConverted_ThenResultedEllipseHasCorrectRenderTransformInformation()
    {
        ConvertSvgFile("transform-translate-positive.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform0 = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform0.X.Should().Be(270);
            translateTransform0.Y.Should().Be(170);

            TranslateTransform translateTransform1 = ellipse.GetTransform(1) as TranslateTransform;
            translateTransform1.X.Should().Be(10);
            translateTransform1.Y.Should().Be(20);
        });
    }

    [Fact]
    public void HavingCircleWithTranslateXNegative_WhenSvgIsConverted_ThenResultedEllipseHasOneTranslateTransformWithCorrectValues()
    {
        ConvertSvgFile("transform-translate-x-negative.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform0 = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform0.X.Should().Be(270);
            translateTransform0.Y.Should().Be(170);

            TranslateTransform translateTransform1 = ellipse.GetTransform(1) as TranslateTransform;
            translateTransform1.X.Should().Be(-10);
            translateTransform1.Y.Should().Be(20);
        });
    }

    [Fact]
    public void HavingCircleWithTranslateYNegative_WhenSvgIsConverted_ThenResultedEllipseHasOneTranslateTransformWithCorrectValues()
    {
        ConvertSvgFile("transform-translate-y-negative.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform0 = ellipse.GetTransform(0) as TranslateTransform;
            translateTransform0.X.Should().Be(270);
            translateTransform0.Y.Should().Be(170);

            TranslateTransform translateTransform1 = ellipse.GetTransform(1) as TranslateTransform;
            translateTransform1.X.Should().Be(10);
            translateTransform1.Y.Should().Be(-20);
        });
    }
}