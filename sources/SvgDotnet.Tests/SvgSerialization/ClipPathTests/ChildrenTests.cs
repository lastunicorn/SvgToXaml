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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.ClipPathTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Fact]
    public void HavingDescChild_WhenSvgFileIsParsed_ThenClipPathContainsDescription()
    {
        ParseSvgFile("clippath-desc.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgDescription>();
        });
    }

    [Fact]
    public void HavingTitleChild_WhenSvgFileIsParsed_ThenClipPathContainsTitle()
    {
        ParseSvgFile("clippath-title.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgTitle>();
        });
    }

    [Fact]
    public void HavingCircleChild_WhenSvgFileIsParsed_ThenClipPathContainsCircle()
    {
        ParseSvgFile("clippath-circle.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgCircle>();
        });
    }

    [Fact]
    public void HavingEllipseChild_WhenSvgFileIsParsed_ThenClipPathContainsEllipse()
    {
        ParseSvgFile("clippath-ellipse.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgEllipse>();
        });
    }

    [Fact]
    public void HavingLineChild_WhenSvgFileIsParsed_ThenClipPathContainsLine()
    {
        ParseSvgFile("clippath-line.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgLine>();
        });
    }

    [Fact]
    public void HavingPathChild_WhenSvgFileIsParsed_ThenClipPathContainsPath()
    {
        ParseSvgFile("clippath-path.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgPath>();
        });
    }

    [Fact]
    public void HavingPolygonChild_WhenSvgFileIsParsed_ThenClipPathContainsPolygon()
    {
        ParseSvgFile("clippath-polygon.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgPolygon>();
        });
    }

    [Fact]
    public void HavingPolylineChild_WhenSvgFileIsParsed_ThenClipPathContainsPolygon()
    {
        ParseSvgFile("clippath-polyline.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgPolyline>();
        });
    }

    [Fact]
    public void HavingRectChild_WhenSvgFileIsParsed_ThenClipPathContainsRectangle()
    {
        ParseSvgFile("clippath-rect.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgRectangle>();
        });
    }

    [Fact]
    public void HavingTextChild_WhenSvgFileIsParsed_ThenClipPathContainsText()
    {
        ParseSvgFile("clippath-text.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgText>();
        });
    }

    [Fact]
    public void HavingUseChild_WhenSvgFileIsParsed_ThenClipPathContainsUse()
    {
        ParseSvgFile("clippath-use.svg", svg =>
        {
            SvgClipPath svgClipPath = svg.Children[0] as SvgClipPath;

            svgClipPath.Children[0].Should().BeOfType<SvgUse>();
        });
    }
}