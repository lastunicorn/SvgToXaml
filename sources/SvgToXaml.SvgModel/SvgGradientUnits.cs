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

public enum SvgGradientUnits
{
    /// <summary>
    /// The user coordinate system for attributes ‘cx’, ‘cy’, ‘r’, ‘fx’, ‘fy’, and ‘fr’ is
    /// established using the bounding box of the element to which the gradient is applied.
    /// This is the default value.
    /// </summary>
    ObjectBoundingBox = 0,

    /// <summary>
    /// ‘cx’, ‘cy’, ‘r’, ‘fx’, ‘fy’, and ‘fr’ represent values in the coordinate system that
    /// results from taking the current user coordinate system in place at the time when the
    /// gradient element is referenced.
    /// </summary>
    UserSpaceOnUse
}