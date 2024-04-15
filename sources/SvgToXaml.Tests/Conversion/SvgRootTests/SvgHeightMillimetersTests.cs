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

using DustInTheWind.SvgToXaml.Conversion;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.SvgRootTests;

public class SvgHeightMillimetersTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSvgHeightSetToZeroMillimeters_WhenSvgIsParsed_ThenCanvasHasHeightZero()
    {
        ConvertSvgFile("svg-height-zero.svg", canvas =>
        {
            canvas.Height.Should().Be(0);
        });
    }

    /// <summary>
    /// 1mm ~= 3.7795px or user units
    /// </summary>
    [Fact]
    public void HavingSvgHeightSetTo100Millimeters_WhenSvgIsParsed_ThenCanvasHasHeight377point95()
    {
        ConvertSvgFile("svg-height-positive-value.svg", canvas =>
        {
            // 1 mm = 3.779527559055118 px
            canvas.Height.Should().Be(377.9527559055118);
        });
    }

    [Fact]
    public void HavingSvgHeightSetToNegativeMillimetersValue_WhenSvgIsParsed_ThenThrows()
    {
        Action action = () =>
        {
            ConvertSvgFile("svg-height-negative-value.svg");
        };

        action.Should().Throw<StaEnvironmentException>()
            .WithInnerException<SvgConversionException>();
    }
}