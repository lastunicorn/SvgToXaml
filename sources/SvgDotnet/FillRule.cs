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

namespace DustInTheWind.SvgToXaml.SvgModel;

public enum FillRule
{
    /// <summary>
    /// This rule determines the "insideness" of a point on the canvas by drawing a ray from that
    /// point to infinity in any direction and counting the number of path segments from the given
    /// shape that the ray crosses. If this number is odd, the point is inside; if even, the point
    /// is outside.
    /// </summary>
    EvenOdd = 0,

    /// <summary>
    /// This rule determines the "insideness" of a point on the canvas by drawing a ray from that
    /// point to infinity in any direction and then examining the places where a segment of the
    /// shape crosses the ray. Starting with a count of zero, add one each time a path segment
    /// crosses the ray from left to right and subtract one each time a path segment crosses the
    /// ray from right to left. After counting the crossings, if the result is zero then the point
    /// is outside the path. Otherwise, it is inside.
    /// </summary>
    Nonzero = 1
}