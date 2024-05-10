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
using System.Globalization;
using System.Text.RegularExpressions;

namespace DustInTheWind.SvgDotnet;

public readonly record struct AlphaValue
{
    private static readonly Regex Regex = new(@"^\s*([+-]?[0-9]*[.]?[0-9]+(?:e[+-]?[0-9]+)?)(%)?\s*$", RegexOptions.Singleline);

    public double Value { get; }

    public AlphaValueUnit Unit { get; }

    public static AlphaValue Zero = new();

    public AlphaValue(double Value, AlphaValueUnit Unit)
    {
        if (!Enum.IsDefined(typeof(AlphaValueUnit), Unit)) throw new InvalidEnumArgumentException(nameof(Unit), (int)Unit, typeof(AlphaValueUnit));

        this.Value = Value;
        this.Unit = Unit;
    }

    public double NumberValue => Unit switch
    {
        AlphaValueUnit.Number => Value,
        AlphaValueUnit.Percentage => Value / 100,
        _ => throw new ArgumentOutOfRangeException(nameof(Value), "Invalid Unit value.")
    };

    public double PercentageValue => Unit switch
    {
        AlphaValueUnit.Number => Value * 100,
        AlphaValueUnit.Percentage => Value,
        _ => throw new ArgumentOutOfRangeException(nameof(Value), "Invalid Unit value.")
    };

    public static AlphaValue Parse(string text)
    {
        if (text == null)
            return Zero;

        if (string.IsNullOrWhiteSpace(text))
            return Zero;

        Match match = Regex.Match(text);

        if (!match.Success)
            throw new ArgumentException("The text is not an alpha value.", nameof(text));

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        AlphaValueUnit unit = match.Groups[2].Value.ToAlphaValueUnit();

        return new AlphaValue(value, unit);
    }

    public static bool TryParse(string text, out AlphaValue alphaValue)
    {
        if (text == null)
        {
            alphaValue = Zero;
            return false;
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            alphaValue = Zero;
            return true;
        }

        Match match = Regex.Match(text);

        if (!match.Success)
        {
            alphaValue = Zero;
            return false;
        }

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        AlphaValueUnit unit = match.Groups[2].Value.ToAlphaValueUnit();

        alphaValue = new AlphaValue(value, unit);
        return true;
    }

    public static implicit operator AlphaValue(double value)
    {
        return new AlphaValue(value, AlphaValueUnit.Number);
    }

    public static implicit operator double(AlphaValue alphaValue)
    {
        return alphaValue.Value;
    }

    public static implicit operator AlphaValue(string text)
    {
        return Parse(text);
    }

    public static implicit operator AlphaValue?(string text)
    {
        if (text == null)
            return null;

        return Parse(text);
    }
}