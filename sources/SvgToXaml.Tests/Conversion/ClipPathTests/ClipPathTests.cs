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

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.ClipPathTests;

public class ClipPathTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleReferencingClipPathWithContainingRectangle_WhenSvgIsConverted_ThenEllipseContainsRectangleGeometryWithCorrectValues()
    {
        TestConvertSvgFile("circle-clippath.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Clip.Should().BeOfType<RectangleGeometry>();

            RectangleGeometry rectangleGeometry = ellipse.Clip as RectangleGeometry;

            Rect expectedRect = new(0, 0, 200, 100);
            rectangleGeometry.Rect.Should().Be(expectedRect);
        });
    }
}