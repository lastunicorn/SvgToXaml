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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.LinearGradientTests;

public class ChildrenTests : SvgFileTestsBase
{
    [Theory]
    [InlineData("lineargradient-desc.svg", typeof(SvgDescription))]
    [InlineData("lineargradient-title.svg", typeof(SvgTitle))]
    [InlineData("lineargradient-stop.svg", typeof(SvgStop))]
    [InlineData("lineargradient-script.svg", typeof(SvgScript))]
    [InlineData("lineargradient-style.svg", typeof(SvgStyle))]
    [InlineData("lineargradient-text.svg", typeof(SvgText))]
    public void HavingOneSpecificChild_WhenSvgFileIsParsed_ThenEllipseContainsCorrectChildType(string fileName, Type svgElementType)
    {
        ParseSvgFile(fileName, result =>
        {
            SvgLinearGradient svgLinearGradient = result.Svg.Children[0] as SvgLinearGradient;

            svgLinearGradient.Children[0].Should().BeOfType(svgElementType);
        });
    }

    [Fact]
    public void HavingInvalidChild_WhenSvgFileIsParsed_ThenReturnsWarning()
    {
        ParseSvgFile("lineargradient-invalid.svg", context =>
        {
            context.Issues.Should().HaveCount(1);
            context.Issues[0].Level.Should().Be(DeserializationIssueLevel.Warning);
        });
    }
}