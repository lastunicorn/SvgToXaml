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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgElementTests;

public abstract class DashArrayTestsBase<T> : SvgFileTestsBase
    where T : SvgShape
{
    [Fact]
    public void HavingNoDashArraySpecified_WhenSvgIsParsed_ThenStrokeDashArrayIsNull()
    {
        ParseSvgFile("01-dasharray-missing.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            svgElement.StrokeDashArray.Should().BeNull();
        });
    }

    [Fact]
    public void HavingDashArrayOneNumber_WhenSvgIsParsed_ThenStrokeDashArrayContainsOneNumber()
    {
        ParseSvgFile("02-dasharray-1number.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            LengthPercentage[] expected =
            {
                new LengthPercentage(new Length(14))
            };
            svgElement.StrokeDashArray.Should().BeEquivalentTo(expected);
        });
    }

    [Fact]
    public void HavingDashArrayTwoNumbers_WhenSvgIsParsed_ThenStrokeDashArrayContainsTwoNumbers()
    {
        ParseSvgFile("03-dasharray-2number.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            LengthPercentage[] expected =
            {
                new LengthPercentage(new Length(14)),
                new LengthPercentage(new Length(3))
            };
            svgElement.StrokeDashArray.Should().BeEquivalentTo(expected);
        });
    }

    [Fact]
    public void HavingDashArrayOnePixelValue_WhenSvgIsParsed_ThenStrokeDashArrayContainsOnePixelValue()
    {
        ParseSvgFile("04-dasharray-1pixel.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            LengthPercentage[] expected =
            {
                new LengthPercentage(new Length(14, SvgLengthUnit.Pixels))
            };
            svgElement.StrokeDashArray.Should().BeEquivalentTo(expected);
        });
    }

    [Fact]
    public void HavingDashArrayTwoPixelValues_WhenSvgIsParsed_ThenStrokeDashArrayContainsTwoPixelValues()
    {
        ParseSvgFile("05-dasharray-2pixel.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            LengthPercentage[] expected =
            {
                new LengthPercentage(new Length(14, SvgLengthUnit.Pixels)),
                new LengthPercentage(new Length(3, SvgLengthUnit.Pixels))
            };
            svgElement.StrokeDashArray.Should().BeEquivalentTo(expected);
        });
    }

    [Fact]
    public void HavingDashArrayOnePercentageValue_WhenSvgIsParsed_ThenStrokeDashArrayContainsOnePercentageValue()
    {
        ParseSvgFile("06-dasharray-1percentage.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            LengthPercentage[] expected =
            {
                new LengthPercentage(new SvgPercentage(14))
            };
            svgElement.StrokeDashArray.Should().BeEquivalentTo(expected);
        });
    }

    [Fact]
    public void HavingDashArrayTwoPercentageValues_WhenSvgIsParsed_ThenStrokeDashArrayContainsTwoPercentageValues()
    {
        ParseSvgFile("07-dasharray-2percentage.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            LengthPercentage[] expected =
            {
                new LengthPercentage(new SvgPercentage(14)),
                new LengthPercentage(new SvgPercentage(3))
            };
            svgElement.StrokeDashArray.Should().BeEquivalentTo(expected);
        });
    }

    [Fact]
    public void HavingDashArrayTwoNumberSeparatedByComma_WhenSvgIsParsed_ThenStrokeDashArrayContainsTwoNumbers()
    {
        ParseSvgFile("08-dasharray-2number-comma.svg", result =>
        {
            T svgElement = SelectElementToTest(result.Svg);

            LengthPercentage[] expected =
            {
                new LengthPercentage(new Length(14)),
                new LengthPercentage(new Length(3))
            };
            svgElement.StrokeDashArray.Should().BeEquivalentTo(expected);
        });
    }

    protected virtual T SelectElementToTest(Svg svg)
    {
        return svg.Children[0] as T;
    }
}