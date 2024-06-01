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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.SvgElementTests;

public class ComputeFillTests
{
    [Fact]
    public void HavingNoFill_WhenComputingFillValue_ThenReturnsNull()
    {
        Svg svg = new();
        SvgCircle svgCircle = svg.AddChild<SvgCircle>();

        Paint actual = svgCircle.ComputeFill();

        actual.Should().BeNull();
    }

    [Fact]
    public void HavingFillAttribute_WhenComputingFillValue_ThenReturnsAttributeValue()
    {
        Svg svg = new();
        SvgCircle svgCircle = svg.AddChild<SvgCircle>(circle =>
        {
            circle.Fill = new Paint((SvgColor)"#aaaaaa");
        });

        Paint actual = svgCircle.ComputeFill();

        Paint expected = new((SvgColor)"#aaaaaa");
        actual.Should().Be(expected);
    }

    [Fact]
    public void HavingFillValueInStyle_WhenComputingFillValue_ThenReturnsFillValueFromStyle()
    {
        Svg svg = new();
        SvgCircle svgCircle = svg.AddChild<SvgCircle>(circle =>
        {
            circle.Style.Add("fill", "#bbbbbb");
        });

        Paint actual = svgCircle.ComputeFill();

        Paint expected = new((SvgColor)"#bbbbbb");
        actual.Should().Be(expected);
    }

    [Fact]
    public void HavingFillValueInClass_WhenComputingFillValue_ThenReturnsFillValueFromClass()
    {
        Svg svg = new();
        svg.AddChild<SvgStyle>(style =>
        {
            style.RuleSets.Add(".circle1", new[]
            {
                new StyleDeclaration("fill", "#cccccc")
            });
        });
        SvgCircle svgCircle = svg.AddChild<SvgCircle>(circle =>
        {
            circle.ClassNames.Add("circle1");
        });

        Paint actual = svgCircle.ComputeFill();

        Paint expected = new((SvgColor)"#cccccc");
        actual.Should().Be(expected);
    }

    [Fact]
    public void HavingFillAttributeAndStyle_WhenComputingFillValue_ThenReturnsStyleValue()
    {
        Svg svg = new();
        SvgCircle svgCircle = svg.AddChild<SvgCircle>(circle =>
        {
            circle.Fill = new Paint((SvgColor)"#aaaaaa");
            circle.Style.Add("fill", "#bbbbbb");
        });

        Paint actual = svgCircle.ComputeFill();

        Paint expected = new((SvgColor)"#bbbbbb");
        actual.Should().Be(expected);
    }

    [Fact]
    public void HavingFillValueInAttributeAndClass_WhenComputingFillValue_ThenReturnsFillValueFromClass()
    {
        Svg svg = new();
        svg.AddChild<SvgStyle>(style =>
        {
            style.RuleSets.Add(".circle1", new[]
            {
                new StyleDeclaration("fill", "#cccccc")
            });
        });
        SvgCircle svgCircle = svg.AddChild<SvgCircle>(circle =>
        {
            circle.Fill = new Paint((SvgColor)"#aaaaaa");
            circle.ClassNames.Add("circle1");
        });

        Paint actual = svgCircle.ComputeFill();

        Paint expected = new((SvgColor)"#cccccc");
        actual.Should().Be(expected);
    }

    [Fact]
    public void HavingFillValueInStyleAndClass_WhenComputingFillValue_ThenReturnsFillValueFromStyle()
    {
        Svg svg = new();
        svg.AddChild<SvgStyle>(style =>
        {
            style.RuleSets.Add(".circle1", new[]
            {
                new StyleDeclaration("fill", "#cccccc")
            });
        });
        SvgCircle svgCircle = svg.AddChild<SvgCircle>(circle =>
        {
            circle.Style.Add("fill", "#bbbbbb");
            circle.ClassNames.Add("circle1");
        });

        Paint actual = svgCircle.ComputeFill();

        Paint expected = new((SvgColor)"#bbbbbb");
        actual.Should().Be(expected);
    }

    [Fact]
    public void HavingFillValueInAttributeAndStyleAndClass_WhenComputingFillValue_ThenReturnsFillValueFromStyle()
    {
        Svg svg = new();
        svg.AddChild<SvgStyle>(style =>
        {
            style.RuleSets.Add(".circle1", new[]
            {
                new StyleDeclaration("fill", "#cccccc")
            });
        });
        SvgCircle svgCircle = svg.AddChild<SvgCircle>(circle =>
        {
            circle.Fill = new Paint((SvgColor)"#aaaaaa");
            circle.Style.Add("fill", "#bbbbbb");
            circle.ClassNames.Add("circle1");
        });

        Paint actual = svgCircle.ComputeFill();

        Paint expected = new((SvgColor)"#bbbbbb");
        actual.Should().Be(expected);
    }
}