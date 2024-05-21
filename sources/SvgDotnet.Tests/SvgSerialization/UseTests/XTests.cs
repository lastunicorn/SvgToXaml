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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.UseTests;

public class XTests : SvgFileTestsBase
{
    [Fact]
    public void HavingUseWithPositiveX_WhenSvgFileIsParsed_ThenUseHasThatX()
    {
        ParseSvgFile("x-positive.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new Length(10);
            svgUse.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithNegativeX_WhenSvgFileIsParsed_ThenUseHasThatX()
    {
        ParseSvgFile("x-negative.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new Length(-10);
            svgUse.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithZeroX_WhenSvgFileIsParsed_ThenUseHasZeroX()
    {
        ParseSvgFile("x-zero.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new Length(0);
            svgUse.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithMissingX_WhenSvgFileIsParsed_ThenUseHasNullX()
    {
        ParseSvgFile("x-missing.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            svgUse.X.Should().BeNull();
        });
    }

    [Fact]
    public void HavingUseWithPercentageX_WhenSvgFileIsParsed_ThenUseHasThatX()
    {
        ParseSvgFile("x-percentage.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new SvgPercentage(25);
            svgUse.X.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingUseWithPositiveXInPixels_WhenSvgFileIsParsed_ThenUseHasThatX()
    {
        ParseSvgFile("x-positivepx.svg", svg =>
        {
            SvgUse svgUse = svg.Children[0] as SvgUse;

            LengthPercentage expected = new Length(42, SvgLengthUnit.Pixels);
            svgUse.X.Should().Be(expected);
        });
    }
}