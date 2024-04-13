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

public class TopTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithCenterY200AndRadius50_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithY150()
    {
        ConvertSvgFile("circle-radius-smaller-than-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.Y.Should().Be(150);
        });
    }

    [Fact]
    public void HavingCircleWithCenterY200AndRadius200_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithY0()
    {
        ConvertSvgFile("circle-radius-equal-to-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.Y.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithCenterY200AndRadius300_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithYNegative200()
    {
        ConvertSvgFile("circle-radius-greater-than-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.Y.Should().Be(-100);
        });
    }

    [Fact]
    public void HavingCircleWithCenterNotSpecifiedAndRadius500_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithYNegative500()
    {
        ConvertSvgFile("circle-missing-cy.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.Y.Should().Be(-500);
        });
    }
}