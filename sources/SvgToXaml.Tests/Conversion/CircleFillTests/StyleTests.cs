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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.CircleFillTests;

public class StyleTests : SvgFileTestsBase
{
    [Fact]
    public void HavingFillDeclaredInAttribute_WhenSvgIsConverted_ThenResultedEllipseHasValueFromAttribute()
    {
        ConvertSvgFile("01-attr^-style-class.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Fill.Should().Be("#111111");
        });
    }

    [Fact]
    public void HavingFillDeclaredInStyle_WhenSvgIsConverted_ThenResultedEllipseHasValueFromStyle()
    {
        ConvertSvgFile("02-attr-style^-class.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Fill.Should().Be("#222222");
        });
    }

    [Fact]
    public void HavingFillDeclaredInClass_WhenSvgIsConverted_ThenResultedEllipseHasValueFromClass()
    {
        ConvertSvgFile("03-attr-style-class^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Fill.Should().Be("#333333");
        });
    }

    [Fact]
    public void HavingFillDeclaredInAttributeAndInStyle_WhenSvgIsConverted_ThenResultedEllipseHasValueFromStyle()
    {
        ConvertSvgFile("04-attr^-style^-class.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Fill.Should().Be("#222222");
        });
    }

    [Fact]
    public void HavingFillDeclaredInAttributeAndInClass_WhenSvgIsConverted_ThenResultedEllipseHasValueFromClass()
    {
        ConvertSvgFile("05-attr^-style-class^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Fill.Should().Be("#333333");
        });
    }

    [Fact]
    public void HavingFillDeclaredInStyleAndInClass_WhenSvgIsConverted_ThenResultedEllipseHasValueFromStyle()
    {
        ConvertSvgFile("06-attr-style^-class^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Fill.Should().Be("#222222");
        });
    }

    [Fact]
    public void HavingFillDeclaredInAttributeInStyleAndInClass_WhenSvgIsConverted_ThenResultedEllipseHasValueFromStyle()
    {
        ConvertSvgFile("07-attr^-style^-class^.svg", canvas =>
        {
            Ellipse ellipse = canvas.GetElementByIndex<Ellipse>(0);

            ellipse.Fill.Should().Be("#222222");
        });
    }
}