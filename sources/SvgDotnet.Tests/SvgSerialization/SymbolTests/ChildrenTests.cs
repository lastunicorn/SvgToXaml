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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SymbolTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenSymbolContainsDescription()
    {
        ParseSvgFile("symbol-desc.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingSymbolWithTitleChild_WhenSvgFileIsParsed_ThenSymbolContainsTitle()
    {
        ParseSvgFile("symbol-title.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingSymbolWithLinearGradientChild_WhenSvgFileIsParsed_ThenSymbolContainsLinearGradient()
    {
        ParseSvgFile("symbol-lineargradient.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgLinearGradient>();
        });
    }

    [Fact]
    public void HavingSymbolWithRadialGradientChild_WhenSvgFileIsParsed_ThenSymbolContainsRadialGradient()
    {
        ParseSvgFile("symbol-radialgradient.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgRadialGradient>();
        });
    }

    [Fact]
    public void HavingSymbolWithCircleChild_WhenSvgFileIsParsed_ThenSymbolContainsCircle()
    {
        ParseSvgFile("symbol-circle.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgCircle>();
        });
    }

    [Fact]
    public void HavingSymbolWithEllipseChild_WhenSvgFileIsParsed_ThenSymbolContainsEllipse()
    {
        ParseSvgFile("symbol-ellipse.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgEllipse>();
        });
    }

    [Fact]
    public void HavingSymbolWithLineChild_WhenSvgFileIsParsed_ThenSymbolContainsLine()
    {
        ParseSvgFile("symbol-line.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgLine>();
        });
    }

    [Fact]
    public void HavingSymbolWithPathChild_WhenSvgFileIsParsed_ThenSymbolContainsPath()
    {
        ParseSvgFile("symbol-path.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgPath>();
        });
    }

    [Fact]
    public void HavingSymbolWithPolygonChild_WhenSvgFileIsParsed_ThenSymbolContainsPolygon()
    {
        ParseSvgFile("symbol-polygon.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgPolygon>();
        });
    }

    [Fact]
    public void HavingSymbolWithPolylineChild_WhenSvgFileIsParsed_ThenSymbolContainsPolygon()
    {
        ParseSvgFile("symbol-polyline.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgPolyline>();
        });
    }

    [Fact]
    public void HavingSymbolWithRectChild_WhenSvgFileIsParsed_ThenSymbolContainsRectangle()
    {
        ParseSvgFile("symbol-rect.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgRectangle>();
        });
    }

    [Fact]
    public void HavingSymbolWithDefsChild_WhenSvgFileIsParsed_ThenSymbolContainsDefinitions()
    {
        ParseSvgFile("symbol-defs.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgDefinitions>();
        });
    }

    [Fact]
    public void HavingSymbolWithGChild_WhenSvgFileIsParsed_ThenSymbolContainsGroup()
    {
        ParseSvgFile("symbol-g.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgGroup>();
        });
    }

    [Fact]
    public void HavingSymbolWithSvgChild_WhenSvgFileIsParsed_ThenSymbolContainsChildSvg()
    {
        ParseSvgFile("symbol-svg.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<Svg>();
        });
    }

    [Fact]
    public void HavingSymbolChild_WhenSvgFileIsParsed_ThenSymbolContainsSymbol()
    {
        ParseSvgFile("symbol-symbol.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgSymbol>();
        });
    }

    [Fact]
    public void HavingSymbolWithUseChild_WhenSvgFileIsParsed_ThenSymbolContainsUse()
    {
        ParseSvgFile("symbol-use.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgUse>();
        });
    }

    [Fact]
    public void HavingSymbolWithClipPathChild_WhenSvgFileIsParsed_ThenSymbolContainsClipPath()
    {
        ParseSvgFile("symbol-clippath.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgClipPath>();
        });
    }

    [Fact]
    public void HavingSymbolWithStyleChild_WhenSvgFileIsParsed_ThenSymbolContainsStyle()
    {
        ParseSvgFile("symbol-style.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgStyle>();
        });
    }

    [Fact]
    public void HavingSymbolWithTextChild_WhenSvgFileIsParsed_ThenSymbolContainsText()
    {
        ParseSvgFile("symbol-text.svg", svg =>
        {
            SvgSymbol svgSymbol = svg.Children[0] as SvgSymbol;

            svgSymbol.Children[0].Should().BeOfType<SvgText>();
        });
    }
}