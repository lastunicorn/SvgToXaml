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

public enum SvgLengthUnit
{
    Unspecified = 0,

    /// <summary>
    /// em - font relative length
    /// </summary>
    ElementFontSize,

    /// <summary>
    /// ex - font relative length
    /// </summary>
    ElementFontHeight,

    /// <summary>
    /// ch - font relative length
    /// </summary>
    CharacterAdvanceOfZero,

    /// <summary>
    /// rem - font relative length
    /// </summary>
    RootElementFontSize,

    /// <summary>
    /// vw - viewport-percentage relative length
    /// </summary>
    ViewportWidthPercentage,

    /// <summary>
    /// vh - viewport-percentage relative length
    /// </summary>
    ViewportHeightPercentage,

    /// <summary>
    /// vmin - viewport-percentage relative length
    /// </summary>
    ViewportSmallerPercentage,

    /// <summary>
    /// vmax - viewport-percentage relative length
    /// </summary>
    ViewportLargerPercentage,

    /// <summary>
    /// cm - absolute length
    /// </summary>
    Centimeters,

    /// <summary>
    /// mm - absolute length
    /// </summary>
    Millimeters,

    /// <summary>
    /// Q - absolute length
    /// </summary>
    QuarterMillimeters,

    /// <summary>
    /// in - absolute length
    /// </summary>
    Inches,

    /// <summary>
    /// pc - absolute length
    /// </summary>
    Picas,

    /// <summary>
    /// pt - absolute length
    /// </summary>
    Points,

    /// <summary>
    /// px - absolute length
    /// </summary>
    Pixels
}