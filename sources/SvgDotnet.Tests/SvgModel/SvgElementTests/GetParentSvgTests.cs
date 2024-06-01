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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.SvgElementTests;

public class GetParentSvgTests
{
    [Fact]
    public void HavingElementInRootSvg_WhenRequestingParentSvg_ThenReturnsRootSvg()
    {
        Svg svg = new();
        
        SvgCircle svgCircle = new();
        svg.Children.Add(svgCircle);

        Svg actual = svgCircle.GetParentSvg();

        actual.Should().BeSameAs(svg);
    }

    [Fact]
    public void HavingOrphanElement_WhenRequestingParentSvg_ThenReturnsNull()
    {
        SvgCircle svgCircle = new();

        Svg actual = svgCircle.GetParentSvg();

        actual.Should().BeNull();
    }

    [Fact]
    public void HavingElementInChildSvg_WhenRequestingParentSvg_ThenReturnsChildSvg()
    {
        Svg svg = new();
        
        Svg childSvg = new();
        svg.Children.Add(childSvg);
        
        SvgCircle svgCircle = new();
        childSvg.Children.Add(svgCircle);

        Svg actual = svgCircle.GetParentSvg();

        actual.Should().BeSameAs(childSvg);
    }
}