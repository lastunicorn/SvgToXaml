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

using System.ComponentModel;

namespace DustInTheWind.SvgDotnet;

internal static class SvgLengthUnitExtensions
{
    public static SvgLengthUnit StringToUnit(this string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        return text switch
        {
            "em" => SvgLengthUnit.ElementFontSize,
            "ex" => SvgLengthUnit.ElementFontHeight,
            "ch" => SvgLengthUnit.CharacterAdvanceOfZero,
            "rem" => SvgLengthUnit.RootElementFontSize,
            "vw" => SvgLengthUnit.ViewportWidthPercentage,
            "vh" => SvgLengthUnit.ViewportHeightPercentage,
            "vmin" => SvgLengthUnit.ViewportSmallerPercentage,
            "vmax" => SvgLengthUnit.ViewportLargerPercentage,
            "cm" => SvgLengthUnit.Centimeters,
            "mm" => SvgLengthUnit.Millimeters,
            "Q" => SvgLengthUnit.QuarterMillimeters,
            "in" => SvgLengthUnit.Inches,
            "pc" => SvgLengthUnit.Picas,
            "pt" => SvgLengthUnit.Points,
            "px" => SvgLengthUnit.Pixels,
            "" => SvgLengthUnit.Unspecified,
            _ => throw new Exception("Invalid unit.")
        };
    }

    public static string UnitToString(this SvgLengthUnit unit)
    {
        return unit switch
        {
            SvgLengthUnit.ElementFontSize => "em",
            SvgLengthUnit.ElementFontHeight => "ex",
            SvgLengthUnit.CharacterAdvanceOfZero => "ch",
            SvgLengthUnit.RootElementFontSize => "rem",
            SvgLengthUnit.ViewportWidthPercentage => "vw",
            SvgLengthUnit.ViewportHeightPercentage => "vh",
            SvgLengthUnit.ViewportSmallerPercentage => "vmin",
            SvgLengthUnit.ViewportLargerPercentage => "vmax",
            SvgLengthUnit.Centimeters => "cm",
            SvgLengthUnit.Millimeters => "mm",
            SvgLengthUnit.QuarterMillimeters => "Q",
            SvgLengthUnit.Inches => "in",
            SvgLengthUnit.Picas => "pc",
            SvgLengthUnit.Points => "pt",
            SvgLengthUnit.Pixels => "px",
            SvgLengthUnit.Unspecified => "",
            _ => throw new InvalidEnumArgumentException(nameof(unit), (int)unit, typeof(SvgLengthUnit))
        };
    }
}