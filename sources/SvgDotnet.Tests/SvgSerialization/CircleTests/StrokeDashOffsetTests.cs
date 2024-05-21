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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.CircleTests;

public class StrokeDashOffsetTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithStrokeDashOffsetPositive_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatStrokeDashOffset()
    {
        ParseSvgFile("circle-strokedashoffset-positive.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new Length(10);
            svgCircle.StrokeDashOffset.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingCircleWithStrokeDashOffsetNegative_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatStrokeDashOffset()
    {
        ParseSvgFile("circle-strokedashoffset-negative.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new Length(-10);
            svgCircle.StrokeDashOffset.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingCircleWithStrokeDashOffsetZero_WhenSvgFileIsParsed_ThenSvgContainsCircleWithStrokeDashOffsetZero()
    {
        ParseSvgFile("circle-strokedashoffset-zero.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new Length(0);
            svgCircle.StrokeDashOffset.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingCircleWithStrokeDashOffsetMissing_WhenSvgFileIsParsed_ThenSvgContainsCircleWithStrokeDashOffsetNull()
    {
        ParseSvgFile("circle-strokedashoffset-missing.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.StrokeDashOffset.Should().BeNull();
        });
    }

    [Fact]
    public void HavingCircleWithStrokeDashOffsetPercentage_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatStrokeDashOffsetPercentage()
    {
        ParseSvgFile("circle-strokedashoffset-percentage.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new SvgPercentage(12);
            svgCircle.StrokeDashOffset.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingCircleWithStrokeDashOffsetPositiveInPixels_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatStrokeDashOffset()
    {
        ParseSvgFile("circle-strokedashoffset-positivepx.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new Length(14, SvgLengthUnit.Pixels);
            svgCircle.StrokeDashOffset.Should().Be(expected);
        });
    }
}