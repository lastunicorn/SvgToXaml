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

public class LeftTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithCenterX300AndRadius50_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithX250()
    {
        ConvertSvgFile("circle-radius-smaller-than-cx.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.X.Should().Be(250);
        });
    }

    [Fact]
    public void HavingCircleWithCenterX300AndRadius300_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithX0()
    {
        ConvertSvgFile("circle-radius-equal-to-cx.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.X.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithCenterX300AndRadius400_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithXNegative100()
    {
        ConvertSvgFile("circle-radius-greater-than-cx.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.X.Should().Be(-100);
        });
    }

    [Fact]
    public void HavingCircleWithCenterNotSpecifiedAndRadius500_WhenSvgIsConverted_ThenResultedEllipseHasTranslateTransformWithXNegative500()
    {
        ConvertSvgFile("circle-missing-cx.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.X.Should().Be(-500);
        });
    }
}