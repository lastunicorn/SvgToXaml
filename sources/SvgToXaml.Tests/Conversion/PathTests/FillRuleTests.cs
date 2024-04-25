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

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.PathTests;

public class FillRuleTests : SvgFileTestsBase
{
    [Fact]
    public void HavingPathWithNoFillRule_WhenSvgIsConverted_ThenSvgContainsPathWithFillRuleNull()
    {
        ConvertSvgFile("path-fillrule-missing.svg", canvas =>
        {
            Path path = canvas.GetElementByIndex<Path>(0);
            StreamGeometry streamGeometry = path.Data as StreamGeometry;

            streamGeometry.FillRule.Should().Be(FillRule.Nonzero);
        });
    }

    [Fact]
    public void HavingPathWithFillRuleNonZero_WhenSvgIsConverted_ThenSvgContainsPathWithFillRuleNonZero()
    {
        ConvertSvgFile("path-fillrule-nonzero.svg", canvas =>
        {
            Path path = canvas.GetElementByIndex<Path>(0);
            StreamGeometry streamGeometry = path.Data as StreamGeometry;

            streamGeometry.FillRule.Should().Be(FillRule.Nonzero);
        });
    }

    [Fact]
    public void HavingPathWithFillRuleEvenOdd_WhenSvgIsConverted_ThenSvgContainsPathWithFillRuleEvenOdd()
    {
        ConvertSvgFile("path-fillrule-evenodd.svg", canvas =>
        {
            Path path = canvas.GetElementByIndex<Path>(0);
            StreamGeometry streamGeometry = path.Data as StreamGeometry;

            streamGeometry.FillRule.Should().Be(FillRule.EvenOdd);
        });
    }

    [Fact]
    public void HavingPathWithFillRuleEvenOddInGroupWithFillRuleNonZero_WhenSvgIsConverted_ThenSvgContainsPathWithFillRuleEvenOdd()
    {
        ConvertSvgFile("g^-path^.svg", canvas =>
        {
            Path path = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Path>(0);

            StreamGeometry streamGeometry = path.Data as StreamGeometry;

            streamGeometry.FillRule.Should().Be(FillRule.EvenOdd);
        });
    }

    [Fact]
    public void HavingPathWithNoFillRuleInGroupWithFillRuleEvenOdd_WhenSvgIsConverted_ThenSvgContainsPathWithFillRuleEvenOdd()
    {
        ConvertSvgFile("g^-path.svg", canvas =>
        {
            Path path = canvas
                .GetElementByIndex<Canvas>(0)
                .GetElementByIndex<Path>(0);

            StreamGeometry streamGeometry = path.Data as StreamGeometry;

            streamGeometry.FillRule.Should().Be(FillRule.EvenOdd);
        });
    }
}