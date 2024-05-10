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

public readonly record struct SvgPercentage
{
    private static readonly Regex Regex = new(@"^\s*([+-]?[0-9]*[.]?[0-9]+)%\s*$", RegexOptions.Singleline);

    public double Value { get; }

    public bool IsEmpty { get; }

    public static SvgPercentage Empty { get; } = new();

    public SvgPercentage()
    {
        Value = 0;
        IsEmpty = true;
    }

    public SvgPercentage(double value)
    {
        Value = value;
        IsEmpty = false;
    }

    public static SvgPercentage Parse(string text)
    {
        Match match = Regex.Match(text);

        if (!match.Success)
            throw new ArgumentException("The text is not a percentage.", nameof(text));

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        return new SvgPercentage(value);
    }

    public static bool TryParse(string text, out SvgPercentage svgPercentage)
    {
        Match match = Regex.Match(text);

        if (!match.Success)
        {
            svgPercentage = null;
            return false;
        }

        double value = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
        svgPercentage = new SvgPercentage(value);
        return true;
    }

    public static implicit operator SvgPercentage(double value)
    {
        return new SvgPercentage(value);
    }

    public static implicit operator SvgPercentage(string text)
    {
        return Parse(text);
    }
}