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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SymbolTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Theory]
    [InlineData("symbol-desc.svg", typeof(SvgDescription))]
    [InlineData("symbol-title.svg", typeof(SvgTitle))]
    [InlineData("symbol-lineargradient.svg", typeof(SvgLinearGradient))]
    [InlineData("symbol-radialgradient.svg", typeof(SvgRadialGradient))]
    [InlineData("symbol-circle.svg", typeof(SvgCircle))]
    [InlineData("symbol-ellipse.svg", typeof(SvgEllipse))]
    [InlineData("symbol-line.svg", typeof(SvgLine))]
    [InlineData("symbol-path.svg", typeof(SvgPath))]
    [InlineData("symbol-polygon.svg", typeof(SvgPolygon))]
    [InlineData("symbol-polyline.svg", typeof(SvgPolyline))]
    [InlineData("symbol-rect.svg", typeof(SvgRectangle))]
    [InlineData("symbol-defs.svg", typeof(SvgDefinitions))]
    [InlineData("symbol-g.svg", typeof(SvgGroup))]
    [InlineData("symbol-svg.svg", typeof(Svg))]
    [InlineData("symbol-symbol.svg", typeof(SvgSymbol))]
    [InlineData("symbol-use.svg", typeof(SvgUse))]
    [InlineData("symbol-clippath.svg", typeof(SvgClipPath))]
    [InlineData("symbol-script.svg", typeof(SvgScript))]
    [InlineData("symbol-style.svg", typeof(SvgStyle))]
    [InlineData("symbol-text.svg", typeof(SvgText))]
    public void HavingOneSpecificChild_WhenSvgFileIsParsed_ThenEllipseContainsCorrectChildType(string fileName, Type svgElementType)
    {
        ParseSvgFile(fileName, result =>
        {
            SvgSymbol svgSymbol = result.Svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType(svgElementType);
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("symbol-invalid.svg", result =>
        {
            result.Issues.Should().HaveCount(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}