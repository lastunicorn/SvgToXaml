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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Theory]
    [InlineData("svg-desc.svg", typeof(SvgDescription))]
    [InlineData("svg-title.svg", typeof(SvgTitle))]
    [InlineData("svg-lineargradient.svg", typeof(SvgLinearGradient))]
    [InlineData("svg-radialgradient.svg", typeof(SvgRadialGradient))]
    [InlineData("svg-circle.svg", typeof(SvgCircle))]
    [InlineData("svg-ellipse.svg", typeof(SvgEllipse))]
    [InlineData("svg-line.svg", typeof(SvgLine))]
    [InlineData("svg-path.svg", typeof(SvgPath))]
    [InlineData("svg-polygon.svg", typeof(SvgPolygon))]
    [InlineData("svg-polyline.svg", typeof(SvgPolyline))]
    [InlineData("svg-rect.svg", typeof(SvgRectangle))]
    [InlineData("svg-defs.svg", typeof(SvgDefinitions))]
    [InlineData("svg-g.svg", typeof(SvgGroup))]
    [InlineData("svg-svg.svg", typeof(Svg))]
    [InlineData("svg-symbol.svg", typeof(SvgSymbol))]
    [InlineData("svg-use.svg", typeof(SvgUse))]
    [InlineData("svg-clippath.svg", typeof(SvgClipPath))]
    [InlineData("svg-script.svg", typeof(SvgScript))]
    [InlineData("svg-style.svg", typeof(SvgStyle))]
    [InlineData("svg-text.svg", typeof(SvgText))]
    public void HavingOneSpecificChild_WhenSvgFileIsParsed_ThenEllipseContainsCorrectChildType(string fileName, Type svgElementType)
    {
        ParseSvgFile(fileName, result =>
        {
            result.Svg.Children[0].Should().BeOfType(svgElementType);
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("svg-invalid.svg", result =>
        {
            result.Issues.Should().HaveCount(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}