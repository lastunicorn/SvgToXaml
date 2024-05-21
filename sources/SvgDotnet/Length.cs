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

namespace DustInTheWind.SvgDotnet;

public readonly struct Length
{
    private static readonly Regex Regex = new(@"^\s*([+-]?[0-9]*[.]?[0-9]+(?:e[+-]?[0-9]+)?)(em|ex|ch|rem|vw|vh|vmin|vmax|cm|mm|Q|in|pc|pt|px)?\s*$", RegexOptions.Singleline);

    public static Length Zero { get; } = new(0);

    public double Value { get; }

    public SvgLengthUnit Unit { get; }

    public Length(double value, SvgLengthUnit unit = SvgLengthUnit.Unspecified)
    {
        Value = value;
        Unit = unit;
    }

    public static Length Parse(string text)
    {
        if (text == null)
            return Zero;

        Match match = Regex.Match(text);

        if (!match.Success)
            throw new ArgumentException("The text is not a length.", nameof(text));

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        SvgLengthUnit unit = match.Groups[2].Value.StringToUnit();

        return new Length(value, unit);
    }

    public static bool TryParse(string text, out Length length)
    {
        Match match = Regex.Match(text);

        if (!match.Success)
        {
            length = null;
            return false;
        }

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        SvgLengthUnit unit = match.Groups[2].Value.StringToUnit();

        length = new Length(value, unit);
        return true;
    }

    public override string ToString()
    {
        return Unit == SvgLengthUnit.Unspecified
            ? Value.ToString(CultureInfo.InvariantCulture)
            : Value.ToString(CultureInfo.InvariantCulture) + Unit.UnitToString();
    }

    public static implicit operator Length(double value)
    {
        return new Length(value);
    }

    public static implicit operator Length(float value)
    {
        return new Length(value);
    }

    public static implicit operator double(Length length)
    {
        return length.ToUserUnits().Value;
    }

    public static implicit operator double(Length? length)
    {
        return length?.ToUserUnits().Value ?? 0;
    }

    public static implicit operator Length(string text)
    {
        return text == null
            ? Zero
            : Parse(text);
    }

    public static implicit operator Length?(string text)
    {
        return text == null
            ? (Length?)null
            : Parse(text);
    }

    /// <remarks>
    /// 1 in = 25.4 mm
    /// 1 in = 2.54 cm
    /// 1 in = 96 px
    /// </remarks>
    public Length ToUserUnits()
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

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}