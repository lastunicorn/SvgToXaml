﻿// SvgToXaml
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

using System.Windows.Shapes;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.UseTests;

public class UseHrefCircleFromDefsTests : SvgFileTestsBase
{
    [Fact]
    public void HavingUseReferencingCircleFromDefs_WhenSvgIsParsed_ThenOneEllipseIsCreated()
    {
        ConvertSvgFile("use-href-defs-circle.svg", canvas =>
        {
            canvas.Should().ContainExact(typeof(Ellipse));
        });
    }

    [Fact]
    public void HavingUseReferencingCircleFromGroupFromDefs_WhenSvgIsParsed_ThenOneEllipseIsCreated()
    {
        ConvertSvgFile("use-href-defs-group-circle.svg", canvas =>
        {
            canvas.Should().ContainExact(typeof(Ellipse));
        });
    }
}