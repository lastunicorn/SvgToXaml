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

using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleTests;

public class HeightTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithRadius0_WhenSvgIsConverted_ThenResultedEllipseHasHeight0()
    {
        ConvertSvgFile("circle-radius-zero.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Height.Should().Be(0);
        });
    }

    [Fact]
    public void HavingCircleWithRadius50_WhenSvgIsConverted_ThenResultedEllipseHasHeight100()
    {
        ConvertSvgFile("circle-radius-positive.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Height.Should().Be(100);
        });
    }

    [Fact]
    public void HavingCircleWithRadiusMinus50_WhenSvgIsConverted_ThenResultedEllipseHasHeight0()
    {
        ConvertSvgFile("circle-radius-negative.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Height.Should().Be(0);
        });
    }
}