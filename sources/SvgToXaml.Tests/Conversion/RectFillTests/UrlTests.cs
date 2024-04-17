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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectFillTests;

public class UrlTests : SvgFileTestsBase
{
    [Fact]
    public void HavingFillReferenceGradientFromDefs_WhenSvgIsConverted_ThenResultedRectangleHasGradient()
    {
        ConvertSvgFile("rect-fill-url-gradient.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().BeOfType<LinearGradientBrush>();
            LinearGradientBrush linearGradientBrush = rectangle.Fill as LinearGradientBrush;

            linearGradientBrush.GradientStops[0].Offset.Should().Be(0);
            linearGradientBrush.GradientStops[0].Color.Should().Be("#87ceff");

            linearGradientBrush.GradientStops[1].Offset.Should().Be(1);
            linearGradientBrush.GradientStops[1].Color.Should().Be("#eac102");
        });
    }

    [Fact]
    public void HavingFillReferencingGradientFromDefsWhichReferencesAnotherGradient_WhenSvgIsConverted_ThenResultedRectangleHasGradient()
    {
        ConvertSvgFile("rect-fill-url-gradient-href-gradient.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().BeOfType<LinearGradientBrush>();
            LinearGradientBrush linearGradientBrush = rectangle.Fill as LinearGradientBrush;

            linearGradientBrush.GradientStops[0].Offset.Should().Be(0);
            linearGradientBrush.GradientStops[0].Color.Should().Be("#87ceff");

            linearGradientBrush.GradientStops[1].Offset.Should().Be(1);
            linearGradientBrush.GradientStops[1].Color.Should().Be("#eac102");
        });
    }
}