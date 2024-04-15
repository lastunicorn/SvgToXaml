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

using System.Globalization;
using System.Text.RegularExpressions;

namespace DustInTheWind.SvgToXaml.SvgModel;

public readonly struct SvgLength
{
    private static readonly Regex Regex = new(@"^\s*([+-]?[0-9]*[.]?[0-9]+(?:e[+-]?[0-9]+)?)(em|ex|ch|rem|vw|vh|vmin|vmax|cm|mm|Q|in|pc|pt|px|%)?\s*$", RegexOptions.Singleline);

    public static SvgLength Zero { get; } = new(0);

    public double Value { get; }

    public SvgLengthUnit Unit { get; }

    public static SvgLength Parse(string text)
    {
        Match match = Regex.Match(text);

        if (!match.Success)
            throw new ArgumentException("The text is not a length.", nameof(text));

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        SvgLengthUnit unit = match.Groups[2].Value.StringToUnit();

        return new SvgLength(value, unit);
    }

    public static bool TryParse(string text, out SvgLength svgLength)
    {
        Match match = Regex.Match(text);

        if (!match.Success)
        {
            svgLength = null;
            return false;
        }

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        SvgLengthUnit unit = match.Groups[2].Value.StringToUnit();

        svgLength = new SvgLength(value, unit);
        return true;
    }

    public SvgLength(double value, SvgLengthUnit unit = SvgLengthUnit.Unspecified)
    {
        Value = value;
        Unit = unit;
    }

    public override string ToString()
    {
        return Unit == SvgLengthUnit.Unspecified
            ? Value.ToString(CultureInfo.InvariantCulture)
            : Value.ToString(CultureInfo.InvariantCulture) + Unit.UnitToString();
    }

    public static implicit operator SvgLength(double value)
    {
        return new SvgLength(value);
    }

    public static implicit operator SvgLength(float value)
    {
        return new SvgLength(value);
    }

    public static implicit operator double(SvgLength length)
    {
        return length.Unit == SvgLengthUnit.Percentage
            ? length.Value / 100
            : length.Value;
    }

    public static implicit operator SvgLength(string text)
    {
        return Parse(text);
    }

    /// <remarks>
    /// 1 in = 25.4 mm
    /// 1 in = 2.54 cm
    /// 1 in = 96 px
    /// </remarks>
    public SvgLength ToUserUnits()
    {
        switch (Unit)
        {
            case SvgLengthUnit.Unspecified:
                return this;

            case SvgLengthUnit.ElementFontSize:
            case SvgLengthUnit.ElementFontHeight:
            case SvgLengthUnit.CharacterAdvanceOfZero:
            case SvgLengthUnit.RootElementFontSize:
            case SvgLengthUnit.ViewportWidthPercentage:
            case SvgLengthUnit.ViewportHeightPercentage:
            case SvgLengthUnit.ViewportSmallerPercentage:
            case SvgLengthUnit.ViewportLargerPercentage:
                throw new NotImplementedException($"Could not transform {ToString()} into user units.");

            case SvgLengthUnit.Centimeters:
                // 1 in = 96 px
                // 1 in = 2.54 cm
                // 
                // => 2.54 cm = 96 px
                // => 1 cm = (96 / 2.54) px
                return Value * 96 / 2.54;

            case SvgLengthUnit.Millimeters:
                // 1 in = 96 px
                // 1 in = 25.4 mm
                // 
                // => 25.4 mm = 96 px
                // => 1 mm = (96 / 25.4) px
                return Value * 96 / 25.4;

            case SvgLengthUnit.QuarterMillimeters:
                throw new NotImplementedException($"Could not transform {ToString()} into user units.");

            case SvgLengthUnit.Inches:
                // 1 in = 96 px
                return Value * 96;

            case SvgLengthUnit.Picas:
                // 1 pc = 16 px
                return Value * 16;

            case SvgLengthUnit.Points:
                // 1 px = 0.75 pt
                //
                // => 1 pt = (4 / 3) px
                return Value * 4 / 3;

            case SvgLengthUnit.Pixels:
                return Value;

            case SvgLengthUnit.Percentage:
                throw new NotImplementedException($"Could not transform {ToString()} into user units.");

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}