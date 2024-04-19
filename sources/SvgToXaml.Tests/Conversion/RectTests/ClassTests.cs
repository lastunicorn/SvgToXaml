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

namespace DustInTheWind.SvgToXaml.Tests.Conversion.RectTests;

public class ClassTests : SvgFileTestsBase
{
    [Fact]
    public void HavingStyleWithTypeCss_WhenSvgIsParsed_ThenResultedRectangleHasFillFromStyles()
    {
        ConvertSvgFile("class-defs-style-css.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#0066CC");
        });
    }

    [Fact]
    public void HavingStyleWithTypeHtml_WhenSvgIsParsed_ThenResultedRectangleHasDefaultBlackFill()
    {
        ConvertSvgFile("class-defs-style-html.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#000000");
        });
    }

    [Fact]
    public void HavingStyleWithNoType_WhenSvgIsParsed_ThenResultedRectangleHasFillFromStyles()
    {
        ConvertSvgFile("class-defs-style-notype.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#0066CC");
        });
    }

    [Fact]
    public void HavingNonexistentClass_WhenSvgIsParsed_ThenResultedRectangleHasDefaultBlackFill()
    {
        ConvertSvgFile("class-nonexistent.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#000000");
        });
    }

    [Fact]
    public void HavingRectangleReferTwoClasses_WhenSvgIsParsed_ThenResultedRectangleHasPropertiesFromBothClasses()
    {
        ConvertSvgFile("class-2-defs.svg", canvas =>
        {
            Rectangle rectangle = canvas.GetElementByIndex<Rectangle>(0);

            rectangle.Fill.Should().Be("#0066CC");
            rectangle.Stroke.Should().Be("#112233");
        });
    }
}