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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.DefsTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Theory]
    [InlineData("defs-desc.svg", typeof(SvgDescription))]
    [InlineData("defs-title.svg", typeof(SvgTitle))]
    [InlineData("defs-lineargradient.svg", typeof(SvgLinearGradient))]
    [InlineData("defs-radialgradient.svg", typeof(SvgRadialGradient))]
    [InlineData("defs-circle.svg", typeof(SvgCircle))]
    [InlineData("defs-ellipse.svg", typeof(SvgEllipse))]
    [InlineData("defs-line.svg", typeof(SvgLine))]
    [InlineData("defs-path.svg", typeof(SvgPath))]
    [InlineData("defs-polygon.svg", typeof(SvgPolygon))]
    [InlineData("defs-polyline.svg", typeof(SvgPolyline))]
    [InlineData("defs-rect.svg", typeof(SvgRectangle))]
    [InlineData("defs-defs.svg", typeof(SvgDefinitions))]
    [InlineData("defs-g.svg", typeof(SvgGroup))]
    [InlineData("defs-svg.svg", typeof(Svg))]
    [InlineData("defs-symbol.svg", typeof(SvgSymbol))]
    [InlineData("defs-use.svg", typeof(SvgUse))]
    [InlineData("defs-clippath.svg", typeof(SvgClipPath))]
    [InlineData("defs-script.svg", typeof(SvgScript))]
    [InlineData("defs-style.svg", typeof(SvgStyle))]
    [InlineData("defs-text.svg", typeof(SvgText))]
    public void HavingOneSpecificChild_WhenSvgFileIsParsed_ThenEllipseContainsCorrectChildType(string fileName, Type svgElementType)
    {
        ParseSvgFile(fileName, result =>
        {
            SvgDefinitions svgDefinitions = result.Svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType(svgElementType);
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("defs-invalid.svg", result =>
        {
            result.Issues.Should().HaveCount(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}