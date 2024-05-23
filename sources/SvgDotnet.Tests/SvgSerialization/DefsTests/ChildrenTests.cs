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
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenDefinitionsContainsDescription()
    {
        ParseSvgFile("defs-desc.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenDefinitionsContainsTitle()
    {
        ParseSvgFile("defs-title.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingLinearGradientChild_WhenSvgFileIsParsed_ThenDefinitionsContainsLinearGradient()
    {
        ParseSvgFile("defs-lineargradient.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgLinearGradient>();
        });
    }

    [Fact]
    public void HavingRadialGradientChild_WhenSvgFileIsParsed_ThenDefinitionsContainsRadialGradient()
    {
        ParseSvgFile("defs-radialgradient.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgRadialGradient>();
        });
    }

    [Fact]
    public void HavingCircleChild_WhenSvgFileIsParsed_ThenDefinitionsContainsCircle()
    {
        ParseSvgFile("defs-circle.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgCircle>();
        });
    }

    [Fact]
    public void HavingEllipseChild_WhenSvgFileIsParsed_ThenDefinitionsContainsEllipse()
    {
        ParseSvgFile("defs-ellipse.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgEllipse>();
        });
    }

    [Fact]
    public void HavingLineChild_WhenSvgFileIsParsed_ThenDefinitionsContainsLine()
    {
        ParseSvgFile("defs-line.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgLine>();
        });
    }

    [Fact]
    public void HavingPathChild_WhenSvgFileIsParsed_ThenDefinitionsContainsPath()
    {
        ParseSvgFile("defs-path.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgPath>();
        });
    }

    [Fact]
    public void HavingPolygonChild_WhenSvgFileIsParsed_ThenDefinitionsContainsPolygon()
    {
        ParseSvgFile("defs-polygon.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgPolygon>();
        });
    }

    [Fact]
    public void HavingPolylineChild_WhenSvgFileIsParsed_ThenDefinitionsContainsPolygon()
    {
        ParseSvgFile("defs-polyline.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgPolyline>();
        });
    }

    [Fact]
    public void HavingRectChild_WhenSvgFileIsParsed_ThenDefinitionsContainsRectangle()
    {
        ParseSvgFile("defs-rect.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgRectangle>();
        });
    }

    [Fact]
    public void HavingDefsChild_WhenSvgFileIsParsed_ThenDefinitionsContainsDefinitions()
    {
        ParseSvgFile("defs-defs.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgDefinitions>();
        });
    }

    [Fact]
    public void HavingGChild_WhenSvgFileIsParsed_ThenDefinitionsContainsGroup()
    {
        ParseSvgFile("defs-g.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgGroup>();
        });
    }

    [Fact]
    public void HavingSvgChild_WhenSvgFileIsParsed_ThenDefinitionsContainsSvg()
    {
        ParseSvgFile("defs-svg.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<Svg>();
        });
    }

    [Fact]
    public void HavingSymbolChild_WhenSvgFileIsParsed_ThenDefinitionsContainsSymbol()
    {
        ParseSvgFile("defs-symbol.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgSymbol>();
        });
    }

    [Fact]
    public void HavingUseChild_WhenSvgFileIsParsed_ThenDefinitionsContainsUse()
    {
        ParseSvgFile("defs-use.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgUse>();
        });
    }

    [Fact]
    public void HavingClipPathChild_WhenSvgFileIsParsed_ThenDefinitionsContainsClipPath()
    {
        ParseSvgFile("defs-clippath.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgClipPath>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenDefinitionsContainsStyle()
    {
        ParseSvgFile("defs-style.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;
            SvgStyle svgStyle = svgDefinitions.Children[0] as SvgStyle;

            svgStyle.Should().NotBeNull();
        });
    }

    [Fact]
    public void HavingTextChild_WhenSvgFileIsParsed_ThenDefinitionsContainsText()
    {
        ParseSvgFile("defs-text.svg", svg =>
        {
            SvgDefinitions svgDefinitions = svg.Children[0] as SvgDefinitions;

            svgDefinitions.Children[0].Should().BeOfType<SvgText>();
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("defs-invalid.svg", context =>
        {
            context.Issues.Should().HaveCount(1);
            context.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}