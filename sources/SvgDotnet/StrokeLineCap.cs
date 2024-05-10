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

namespace DustInTheWind.SvgDotnet;

public enum StrokeLineCap
{
    /// <summary>
    /// This value indicates that the stroke for each subpath does not extend beyond its two
    /// endpoints. A zero length subpath will therefore not have any stroke.
    /// </summary>
    Butt,

    /// <summary>
    /// This value indicates that at each end of each subpath, the shape representing the stroke
    /// will be extended by a half circle with a diameter equal to the stroke width. If a subpath,
    /// whether open or closed, has zero length, then the resulting effect is that the stroke for
    /// that subpath consists solely of a full circle centered at the subpath's point.
    /// </summary>
    Round,

    /// <summary>
    /// This value indicates that at the end of each subpath, the shape representing the stroke
    /// will be extended by a rectangle with the same width as the stroke width and whose length is
    /// half of the stroke width. If a subpath, whether open or closed, has zero length, then the
    /// resulting effect is that the stroke for that subpath consists solely of a square with side
    /// length equal to the stroke width, centered at the subpath's point, and oriented such that
    /// two of its sides are parallel to the effective tangent at that subpath's point. See ‘path’
    /// element implementation notes for details on how to determine the tangent at a zero-length
    /// subpath.
    /// </summary>
    Square
}