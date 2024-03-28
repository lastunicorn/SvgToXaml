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

namespace DustInTheWind.SvgToXaml.Svg;

/// <summary>
/// Specifies the shape to be used at the corners of paths or basic shapes when
/// they are stroked.
/// 
/// Applies to shapes and text content elements.
/// Shapes:  ‘circle’, ‘ellipse’, ‘line’, ‘path’, ‘polygon’, ‘polyline’ and ‘rect’.
/// Text content elements: ‘text’, ‘textPath’ and ‘tspan’. 
/// </summary>
public enum StrokeLineJoin
{
    /// <summary>
    /// This value indicates that a sharp corner is to be used to join path segments. The corner is
    /// formed by extending the outer edges of the stroke at the tangents of the path segments
    /// until they intersect.
    /// If the strokemiterlimit is exceeded, the line join falls back to bevel.
    /// Default Value
    /// </summary>
    Miter,

    /// <summary>
    /// This value is the same as miter but if the stroke-miterlimit is exceeded, the miter is
    /// clipped at a distance equal to half the stroke-miterlimit value multiplied by the stroke
    /// width from the intersection of the path segments.
    ///
    /// This value is new in SVG 2.
    /// </summary>
    MiterClip,

    /// <summary>
    /// This value indicates that a round corner is to be used to join path segments. The corner
    /// is a circular sector centered on the join point.
    /// </summary>
    Round,

    /// <summary>
    /// This value indicates that a bevelled corner is to be used to join path segments. The bevel
    /// shape is a triangle that fills the area between the two stroked segments.
    /// </summary>
    Bevel,

    /// <summary>
    /// This value indicates that an arcs corner is to be used to join path segments. The arcs
    /// shape is formed by extending the outer edges of the stroke at the join point with arcs that
    /// have the same curvature as the outer edges at the join point.
    ///
    /// This value is new in SVG 2.
    /// </summary>
    Arcs
}