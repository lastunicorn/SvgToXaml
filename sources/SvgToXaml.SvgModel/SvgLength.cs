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
    private static readonly Regex Regex = new(@"^\s*([+-]?[0-9]*[.]?[0-9]+)(em|ex|ch|rem|vw|vh|vmin|vmax|cm|mm|Q|in|pc|pt|px|%)?\s*$", RegexOptions.Singleline);

    public static SvgLength Zero { get; } = new(0);

    public double Value { get; }

    public SvgLengthUnit Unit { get; }

    public SvgLength(string text)
    {
        Match match = Regex.Match(text);

        if (match.Success)
        {
            Value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
            Unit = match.Groups[2].Value.StringToUnit();
        }
        else
        {
            throw new ArgumentException("The text is not a length.", nameof(text));
        }
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
        return new SvgLength(text);
    }
}