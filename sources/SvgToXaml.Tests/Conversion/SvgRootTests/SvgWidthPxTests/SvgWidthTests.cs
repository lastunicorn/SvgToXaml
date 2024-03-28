// Country Flags
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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.SvgRootTests.SvgWidthPxTests;

public class SvgWidthPxTests : SvgFileTestsBase
{
    [Fact]
    public void HavingSvgWidthSetToZeroPx_WhenSvgIsParsed_ThenCanvasHasWidthZero()
    {
        TestConvertSvgFile("svg-width-zero.svg", canvas =>
        {
            canvas.Width.Should().Be(0);
        });
    }

    [Fact]
    public void HavingSvgWidthSetToPositivePxValue_WhenSvgIsParsed_ThenCanvasHasWidthSpecified()
    {
        TestConvertSvgFile("svg-width-positive-value.svg", canvas =>
        {
            canvas.Width.Should().Be(142);
        });
    }

    [Fact]
    public void HavingSvgWidthSetToNegativePxValue_WhenSvgIsParsed_ThenThrows()
    {
        Action action = () =>
        {
            TestConvertSvgFile("svg-width-negative-value.svg");
        };

        action.Should().Throw<StaEnvironmentException>()
            .WithInnerException<SvgConversionException>();
    }
}