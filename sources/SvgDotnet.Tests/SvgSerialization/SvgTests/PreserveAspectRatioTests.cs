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

namespace DustInTheWind.SvgDotnet.Tests.SvgSerialization.SvgTests;

public class PreserveAspectRatioTests : SvgFileTestsBase
{
    [Fact]
    public void HavingNoPreserveAspectRationSpecified_WhenSvgIsParsed_ThenPreserveAspectRatioIsDefault()
    {
        ParseSvgFile("preserveaspectratio-missing.svg", result =>
        {
            result.Svg.PreserveAspectRatio.Should().Be(PreserveAspectRatio.Default);
        });
    }

    [Fact]
    public void HavingAlignSpecified_WhenSvgIsParsed_ThenPreserveAspectRatioContainSpecifiedAlignAndDefaultMeet()
    {
        ParseSvgFile("preserveaspectratio-align.svg", result =>
        {
            PreserveAspectRatio expected = new(Align.XMinYMin);
            result.Svg.PreserveAspectRatio.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingAlignAndMeetSpecified_WhenSvgIsParsed_ThenPreserveAspectRatioContainSpecifiedAlignAndMeet()
    {
        ParseSvgFile("preserveaspectratio-align-meet.svg", result =>
        {
            PreserveAspectRatio expected = new(Align.XMinYMin, MeetOrSlice.Meet);
            result.Svg.PreserveAspectRatio.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingAlignAndSliceSpecified_WhenSvgIsParsed_ThenPreserveAspectRatioContainSpecifiedAlignAndSlice()
    {
        ParseSvgFile("preserveaspectratio-align-slice.svg", result =>
        {
            PreserveAspectRatio expected = new(Align.XMinYMin, MeetOrSlice.Slice);
            result.Svg.PreserveAspectRatio.Should().Be(expected);
        });
    }

    [Fact]
    public void HavingInvalidValue_WhenSvgIsParsed_ThenPreserveAspectRatioContainDefaultValueAndErrorIsReturned()
    {
        ParseSvgFile("preserveaspectratio-invalid.svg", result =>
        {
            PreserveAspectRatio expected = PreserveAspectRatio.Default;
            result.Svg.PreserveAspectRatio.Should().Be(expected);

            result.Issues.Count.Should().Be(1);
            result.Issues[0].Level.Should().Be(DeserializationIssueLevel.Error);
        });
    }
}