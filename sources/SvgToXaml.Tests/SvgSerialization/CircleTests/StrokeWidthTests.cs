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

using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgToXaml.Tests.SvgSerialization.CircleTests;

public class StrokeWidthTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleWithStrokeWidthPositive_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatStrokeWidth()
    {
        ParseSvgFile("circle-strokewidth-positive.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new SvgLength(10);
            svgCircle.StrokeWidth.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingCircleWithStrokeWidthNegative_WhenSvgFileIsParsed_ThenSvgContainsCircleWithStrokeZeroAndAnError()
    {
        ParseSvgFile("circle-strokewidth-negative.svg", result =>
        {
            SvgCircle svgCircle = result.Svg.Children[0] as SvgCircle;

            LengthPercentage expected = new SvgLength(0);
            svgCircle.StrokeWidth.Should().Be(expected);

            result.Errors.Count.Should().Be(1);
            result.Errors[0].Path.Should().Be("svg.(1)circle.@stroke-width");
        });
    }

    [Fact]
    public void HavingCircleWithStrokeWidthZero_WhenSvgFileIsParsed_ThenSvgContainsCircleWithStrokeWidthZero()
    {
        ParseSvgFile("circle-strokewidth-zero.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new SvgLength(0);
            svgCircle.StrokeWidth.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingCircleWithStrokeWidthMissing_WhenSvgFileIsParsed_ThenSvgContainsCircleWithStrokeWidthNull()
    {
        ParseSvgFile("circle-strokewidth-missing.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            svgCircle.StrokeWidth.Should().BeNull();
        });
    }

    [Fact]
    public void HavingCircleWithStrokeWidthPercentage_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatStrokeWidthPercentage()
    {
        ParseSvgFile("circle-strokewidth-percentage.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new SvgPercentage(12);
            svgCircle.StrokeWidth.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingCircleWithStrokeWidthPositiveInPixels_WhenSvgFileIsParsed_ThenSvgContainsCircleWithThatStrokeWidth()
    {
        ParseSvgFile("circle-strokewidth-positivepx.svg", svg =>
        {
            SvgCircle svgCircle = svg.Children[0] as SvgCircle;

            LengthPercentage expected = new SvgLength(14, SvgLengthUnit.Pixels);
            svgCircle.StrokeWidth.Should().Be(expected);
        });
    }
}