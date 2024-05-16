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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenSvgContainsTitle()
    {
        ParseSvgFile("svg-title.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingLinearGradientChild_WhenSvgFileIsParsed_ThenSvgContainsLinearGradient()
    {
        ParseSvgFile("svg-lineargradient.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgLinearGradient>();
        });
    }

    [Fact]
    public void HavingRadialGradientChild_WhenSvgFileIsParsed_ThenSvgContainsRadialGradient()
    {
        ParseSvgFile("svg-radialgradient.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgRadialGradient>();
        });
    }

    [Fact]
    public void HavingCircleChild_WhenSvgFileIsParsed_ThenSvgContainsCircle()
    {
        ParseSvgFile("svg-circle.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgCircle>();
        });
    }

    [Fact]
    public void HavingEllipseChild_WhenSvgFileIsParsed_ThenSvgContainsEllipse()
    {
        ParseSvgFile("svg-ellipse.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgEllipse>();
        });
    }

    [Fact]
    public void HavingLineChild_WhenSvgFileIsParsed_ThenSvgContainsLine()
    {
        ParseSvgFile("svg-line.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgLine>();
        });
    }

    [Fact]
    public void HavingPathChild_WhenSvgFileIsParsed_ThenSvgContainsPath()
    {
        ParseSvgFile("svg-path.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgPath>();
        });
    }

    [Fact]
    public void HavingPolygonChild_WhenSvgFileIsParsed_ThenSvgContainsPolygon()
    {
        ParseSvgFile("svg-polygon.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgPolygon>();
        });
    }

    [Fact]
    public void HavingPolylineChild_WhenSvgFileIsParsed_ThenSvgContainsPolygon()
    {
        ParseSvgFile("svg-polyline.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgPolyline>();
        });
    }

    [Fact]
    public void HavingRectChild_WhenSvgFileIsParsed_ThenSvgContainsRectangle()
    {
        ParseSvgFile("svg-rect.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgRectangle>();
        });
    }

    [Fact]
    public void HavingDefsChild_WhenSvgFileIsParsed_ThenSvgContainsDefinitions()
    {
        ParseSvgFile("svg-defs.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgDefinitions>();
        });
    }

    [Fact]
    public void HavingGChild_WhenSvgFileIsParsed_ThenSvgContainsGroup()
    {
        ParseSvgFile("svg-g.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgGroup>();
        });
    }

    [Fact]
    public void HavingSvgChild_WhenSvgFileIsParsed_ThenSvgContainsSvg()
    {
        ParseSvgFile("svg-svg.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<Svg>();
        });
    }

    [Fact]
    public void HavingSymbolChild_WhenSvgFileIsParsed_ThenSvgContainsSymbol()
    {
        ParseSvgFile("svg-symbol.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgSymbol>();
        });
    }

    [Fact]
    public void HavingUseChild_WhenSvgFileIsParsed_ThenSvgContainsUse()
    {
        ParseSvgFile("svg-use.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgUse>();
        });
    }

    [Fact]
    public void HavingClipPathChild_WhenSvgFileIsParsed_ThenSvgContainsClipPath()
    {
        ParseSvgFile("svg-clippath.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgClipPath>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenSvgContainsStyle()
    {
        ParseSvgFile("svg-style.svg", svg =>
        {
            SvgStyle svgStyle = svg.Children[0] as SvgStyle;

            svgStyle.Should().NotBeNull();
        });
    }

    [Fact]
    public void HavingTextChild_WhenSvgFileIsParsed_ThenSvgContainsText()
    {
        ParseSvgFile("svg-text.svg", svg =>
        {
            svg.Children[0].Should().BeOfType<SvgText>();
        });
    }
}