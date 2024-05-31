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

/// <summary>
/// This enum is part of the <see cref="PreserveAspectRatio"/> structure.
/// </summary>
public enum Align
{
    None,

    /// <code>
    /// +---+---+---+
    /// | * |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// </code>
    XMinYMin,

    /// <code>
    /// +---+---+---+
    /// |   | * |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// </code>
    XMidYMin,

    /// <code>
    /// +---+---+---+
    /// |   |   | * |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// </code>
    XMaxYMin,

    /// <code>
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// | * |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// </code>
    XMinYMid,

    /// <code>
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   | * |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// </code>
    XMidYMid,

    /// <code>
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   | * |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// </code>
    XMaxYMid,

    /// <code>
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// | * |   |   |
    /// +---+---+---+
    /// </code>
    XMinYMax,

    /// <code>
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   | * |   |
    /// +---+---+---+
    /// </code>
    XMidYMax,

    /// <code>
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   |   |
    /// +---+---+---+
    /// |   |   | * |
    /// +---+---+---+
    /// </code>
    XMaxYMax
}