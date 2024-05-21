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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.UseTests;

public class LocationTests :SvgFileTestsBase
{
    [Fact]
    public void HavingXYSpecifiedForUseElement_WhenSvgIsParsed_ThenTranslateTransformIsCreated()
    {
        ConvertSvgFile("xy.svg", canvas =>
        {
            Rectangle ellipse = canvas.GetElementByIndex<Rectangle>(0);

            ellipse.RenderTransform.Should().BeOfType<TranslateTransform>();

            TranslateTransform translateTransform = ellipse.RenderTransform as TranslateTransform;
            translateTransform.X.Should().Be(123);
            translateTransform.Y.Should().Be(321);
        });
    }

    [Fact]
    public void HavingTranslateAndXYSpecifiedForUseElement_WhenSvgIsParsed_ThenTranslateTransformIsFirstInTransformGroup()
    {
        ConvertSvgFile("xy-transform.svg", canvas =>
        {
            Rectangle ellipse = canvas.GetElementByIndex<Rectangle>(0);

            ellipse.RenderTransform.Should().BeOfType<TransformGroup>();
            TransformGroup transformGroup = ellipse.RenderTransform as TransformGroup;

            transformGroup.Children[0].Should().BeOfType<TranslateTransform>();

            TranslateTransform translateTransform = transformGroup.Children[0] as TranslateTransform;
            translateTransform.X.Should().Be(123);
            translateTransform.Y.Should().Be(321);
        });
    }
}