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

using DustInTheWind.SvgDotnet.Serialization;

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.RectTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Theory]
    [InlineData("rect-desc.svg", typeof(SvgDescription))]
    [InlineData("rect-title.svg", typeof(SvgTitle))]
    [InlineData("rect-lineargradient.svg", typeof(SvgLinearGradient))]
    [InlineData("rect-radialgradient.svg", typeof(SvgRadialGradient))]
    [InlineData("rect-clippath.svg", typeof(SvgClipPath))]
    [InlineData("rect-script.svg", typeof(SvgScript))]
    [InlineData("rect-style.svg", typeof(SvgStyle))]
    public void HavingOneSpecificChild_WhenSvgFileIsParsed_ThenEllipseContainsCorrectChildType(string fileName, Type svgElementType)
    {
        ParseSvgFile(fileName, result =>
        {
            SvgRectangle svgRectangle = result.Svg.Children[0] as SvgRectangle;

            svgRectangle.Children[0].Should().BeOfType(svgElementType);
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("rect-invalid.svg", result =>
        {
            result.Issues.Should().HaveCount(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}