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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.GTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Theory]
    [InlineData("g-desc.svg", typeof(SvgDescription))]
    [InlineData("g-title.svg", typeof(SvgTitle))]
    [InlineData("g-lineargradient.svg", typeof(SvgLinearGradient))]
    [InlineData("g-radialgradient.svg", typeof(SvgRadialGradient))]
    [InlineData("g-circle.svg", typeof(SvgCircle))]
    [InlineData("g-ellipse.svg", typeof(SvgEllipse))]
    [InlineData("g-line.svg", typeof(SvgLine))]
    [InlineData("g-path.svg", typeof(SvgPath))]
    [InlineData("g-polygon.svg", typeof(SvgPolygon))]
    [InlineData("g-polyline.svg", typeof(SvgPolyline))]
    [InlineData("g-rect.svg", typeof(SvgRectangle))]
    [InlineData("g-defs.svg", typeof(SvgDefinitions))]
    [InlineData("g-g.svg", typeof(SvgGroup))]
    [InlineData("g-svg.svg", typeof(Svg))]
    [InlineData("g-symbol.svg", typeof(SvgSymbol))]
    [InlineData("g-use.svg", typeof(SvgUse))]
    [InlineData("g-clippath.svg", typeof(SvgClipPath))]
    [InlineData("g-script.svg", typeof(SvgScript))]
    [InlineData("g-style.svg", typeof(SvgStyle))]
    [InlineData("g-text.svg", typeof(SvgText))]
    public void HavingOneSpecificChild_WhenSvgFileIsParsed_ThenEllipseContainsCorrectChildType(string fileName, Type svgElementType)
    {
        ParseSvgFile(fileName, result =>
        {
            SvgGroup svgGroup = result.Svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType(svgElementType);
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("g-invalid.svg", result =>
        {
            result.Issues.Should().HaveCount(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}