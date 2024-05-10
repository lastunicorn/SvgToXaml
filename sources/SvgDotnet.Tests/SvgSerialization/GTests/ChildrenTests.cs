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

using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.GTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingCircleChild_WhenSvgFileIsParsed_ThenSvgContainsCircle()
    {
        ParseSvgFile("g-circle.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgCircle>();
        });
    }

    [Fact]
    public void HavingEllipseChild_WhenSvgFileIsParsed_ThenSvgContainsEllipse()
    {
        ParseSvgFile("g-ellipse.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgEllipse>();
        });
    }

    [Fact]
    public void HavingLineChild_WhenSvgFileIsParsed_ThenSvgContainsLine()
    {
        ParseSvgFile("g-line.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgLine>();
        });
    }

    [Fact]
    public void HavingPathChild_WhenSvgFileIsParsed_ThenSvgContainsPath()
    {
        ParseSvgFile("g-path.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgPath>();
        });
    }

    [Fact]
    public void HavingPolygonChild_WhenSvgFileIsParsed_ThenSvgContainsPolygon()
    {
        ParseSvgFile("g-polygon.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgPolygon>();
        });
    }

    [Fact]
    public void HavingPolylineChild_WhenSvgFileIsParsed_ThenSvgContainsPolygon()
    {
        ParseSvgFile("g-polyline.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgPolyline>();
        });
    }

    [Fact]
    public void HavingRectChild_WhenSvgFileIsParsed_ThenSvgContainsRectangle()
    {
        ParseSvgFile("g-rect.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgRectangle>();
        });
    }

    [Fact]
    public void HavingDefsChild_WhenSvgFileIsParsed_ThenSvgContainsDefinitions()
    {
        ParseSvgFile("g-defs.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgDefinitions>();
        });
    }

    [Fact]
    public void HavingGChild_WhenSvgFileIsParsed_ThenSvgContainsGroup()
    {
        ParseSvgFile("g-g.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgGroup>();
        });
    }

    [Fact]
    public void HavingUseChild_WhenSvgFileIsParsed_ThenSvgContainsUse()
    {
        ParseSvgFile("g-use.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgUse>();
        });
    }

    [Fact]
    public void HavingStyleChild_WhenSvgFileIsParsed_ThenSvgContainsStyle()
    {
        ParseSvgFile("g-style.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;
            SvgStyle svgStyle = svgGroup.Children[0] as SvgStyle;

            svgStyle.Should().NotBeNull();
        });
    }

    [Fact]
    public void HavingTextChild_WhenSvgFileIsParsed_ThenSvgContainsText()
    {
        ParseSvgFile("g-text.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgText>();
        });
    }

    [Fact]
    public void HavingLinearGradientChild_WhenSvgFileIsParsed_ThenSvgContainsLinearGradient()
    {
        ParseSvgFile("g-lineargradient.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgLinearGradient>();
        });
    }

    [Fact]
    public void HavingRadialGradientChild_WhenSvgFileIsParsed_ThenSvgContainsRadialGradient()
    {
        ParseSvgFile("g-radialgradient.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgRadialGradient>();
        });
    }

    [Fact]
    public void HavingClipPathChild_WhenSvgFileIsParsed_ThenSvgContainsClipPath()
    {
        ParseSvgFile("g-clippath.svg", svg =>
        {
            SvgGroup svgGroup = svg.Children[0] as SvgGroup;

            svgGroup.Children[0].Should().BeOfType<SvgClipPath>();
        });
    }
}