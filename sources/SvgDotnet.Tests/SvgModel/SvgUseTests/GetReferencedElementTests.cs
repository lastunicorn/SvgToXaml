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

namespace DustInTheWind.SvgDotnet.Tests.SvgModel.SvgUseTests;

public class GetReferencedElementTests
{
    [Fact]
    public void HavingUseElementReferencingNonexistentElement_WhenSearchingForReferencedElement_ThenReturnsNull()
    {
        // - svg
        //     - use

        Svg rootSvg = new();

        SvgUse svgUse = rootSvg.AddChild(new SvgUse
        {
            Href = "#circle1"
        });

        SvgElement actual = svgUse.GetReferencedElement();

        actual.Should().BeNull();
    }

    [Fact]
    public void HavingUseElementReferencingSiblingElement_WhenSearchingForReferencedElement_ThenItIsSuccessfullyFound()
    {
        // - svg
        //     - circle
        //     - use

        Svg rootSvg = new();

        SvgCircle svgCircle = rootSvg.AddChild(new SvgCircle
        {
            Id = "circle1"
        });

        SvgUse svgUse = rootSvg.AddChild(new SvgUse
        {
            Href = "#circle1"
        });

        SvgElement actual = svgUse.GetReferencedElement();

        actual.Should().BeSameAs(svgCircle);
    }

    [Fact]
    public void HavingUseElementReferencingSiblingChildElement_WhenSearchingForReferencedElement_ThenItIsSuccessfullyFound()
    {
        // - svg
        //     - g
        //         - circle
        //     - use

        Svg rootSvg = new();

        SvgGroup svgGroup = rootSvg.AddChild(new SvgGroup());

        SvgCircle svgCircle = svgGroup.AddChild(new SvgCircle
        {
            Id = "circle1"
        });

        SvgUse svgUse = rootSvg.AddChild(new SvgUse
        {
            Href = "#circle1"
        });

        SvgElement actual = svgUse.GetReferencedElement();

        actual.Should().BeSameAs(svgCircle);
    }

    [Fact]
    public void HavingUseElementReferencingParentSiblingChildElement_WhenSearchingForReferencedElement_ThenItIsSuccessfullyFound()
    {
        // - svg
        //     - g
        //         - circle
        //     - g
        //         - use

        Svg rootSvg = new();

        SvgGroup svgGroup1 = rootSvg.AddChild(new SvgGroup());
        SvgCircle svgCircle = svgGroup1.AddChild(new SvgCircle
        {
            Id = "circle1"
        });

        SvgGroup svgGroup2 = rootSvg.AddChild(new SvgGroup());
        SvgUse svgUse = svgGroup2.AddChild(new SvgUse
        {
            Href = "#circle1"
        });

        SvgElement actual = svgUse.GetReferencedElement();

        actual.Should().BeSameAs(svgCircle);
    }

    [Fact]
    public void HavingUseElementReferencingElementFromAnotherSvg_WhenSearchingForReferencedElement_ThenItIsSuccessfullyFound()
    {
        // - svg
        //     - svg
        //         - circle
        //     - g
        //         - use

        Svg rootSvg = new();

        Svg childSvg = rootSvg.AddChild<Svg>();
        SvgCircle svgCircle = childSvg.AddChild<SvgCircle>(svgCircle =>
        {
            svgCircle.Id = "circle1";
        });

        SvgGroup svgGroup2 = rootSvg.AddChild<SvgGroup>();
        SvgUse svgUse = svgGroup2.AddChild<SvgUse>(svgUse =>
        {
            svgUse.Href = "#circle1";
        });

        SvgElement actual = svgUse.GetReferencedElement();

        actual.Should().BeSameAs(svgCircle);
    }

    [Fact]
    public void HavingUseElementInChildSVGReferencingElementOutsideChildSvg_WhenSearchingForReferencedElement_ThenItIsSuccessfullyFound()
    {
        // - svg
        //     - defs
        //         - circle
        //     - svg
        //         - use

        Svg rootSvg = new();

        SvgDefinitions svgDefinitions = rootSvg.AddChild<SvgDefinitions>();
        SvgCircle svgCircle = svgDefinitions.AddChild<SvgCircle>(svgCircle =>
        {
            svgCircle.Id = "circle1";
        });

        Svg childSvg = rootSvg.AddChild<Svg>();
        SvgUse svgUse = childSvg.AddChild<SvgUse>(svgUse =>
        {
            svgUse.Href = "#circle1";
        });

        SvgElement actual = svgUse.GetReferencedElement();

        actual.Should().BeSameAs(svgCircle);
    }
}