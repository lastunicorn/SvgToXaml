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

using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion.PathTests;

public class PathTests : SvgFileTestsBase
{
    [Fact]
    public void HavingPathWithDataValueNone_WhenSvgIsParsed_ThenResultedPathHasEmptyGeometry()
    {
        ConvertSvgFile("path-data-none.svg", canvas =>
        {
            Path path = canvas.GetElementByIndex<Path>(0);

            path.Data.Should().Be(Geometry.Empty);
        });
    }

    [Fact]
    public void HavingPathWithoutData_WhenSvgIsParsed_ThenResultedPathHasEmptyGeometry()
    {
        ConvertSvgFile("path-data-absent.svg", canvas =>
        {
            Path path = canvas.GetElementByIndex<Path>(0);

            path.Data.Should().Be(Geometry.Empty);
        });
    }

    [Fact]
    public void HavingPathWithData_WhenSvgIsParsed_ThenResultedPathHasPathGeometry()
    {
        ConvertSvgFile("path-data.svg", canvas =>
        {
            Path path = canvas.GetElementByIndex<Path>(0);

            path.Data.Should().NotBe(Geometry.Empty);
        });
    }
}