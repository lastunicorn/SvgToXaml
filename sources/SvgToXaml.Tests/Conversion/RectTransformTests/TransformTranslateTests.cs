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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectTransformTests;

public class TransformTranslateTests : SvgFileTestsBase
{
    [Fact]
    public void HavingRectWithTransformTranslate_WhenSvgIsParsed_ThenResultedRectangleHasCorrectRenderTransformInformation()
    {
        ConvertSvgFile("transform-translate.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.RenderTransform.Should().BeOfType<TranslateTransform>();

            TranslateTransform translateTransform = rectangle.RenderTransform as TranslateTransform;
            translateTransform.X.Should().Be(10);
            translateTransform.Y.Should().Be(20);
        });
    }
}