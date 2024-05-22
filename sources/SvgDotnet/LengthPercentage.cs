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

public readonly record struct LengthPercentage
{
    public Length? Length { get; }

    public SvgPercentage? Percentage { get; }

    public bool IsEmpty { get; }

    public bool IsNegative => Length?.Value < 0 || Percentage?.Value < 0;

    public static LengthPercentage Empty { get; } = new();

    public static LengthPercentage Zero { get; } = new(SvgDotnet.Length.Zero);

    public LengthPercentage()
    {
        Length = null;
        Percentage = null;
        IsEmpty = true;
    }

    public LengthPercentage(Length length)
    {
        Length = length;
        Percentage = null;
        IsEmpty = false;
    }

    public LengthPercentage(SvgPercentage svgPercentage)
    {
        Length = null;
        Percentage = svgPercentage;
        IsEmpty = false;
    }

    public double ComputeValue()
    {
        if (IsEmpty)
            return 0;

        if (Length != null)
            return Length.Value.Value;

        if (Percentage != null)
            return Percentage.Value.Value;

        return 0;
    }

    public static LengthPercentage Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Empty;

        bool isLength = SvgDotnet.Length.TryParse(text, out Length svgLength);

        if (isLength)
            return new LengthPercentage(svgLength);

        bool isPercentage = SvgPercentage.TryParse(text, out SvgPercentage svgPercentage);

        if (isPercentage)
            return new LengthPercentage(svgPercentage);

        return Empty;
    }

    public override string ToString()
    {
        if (Length != null)
            return Length.ToString();

        if (Percentage != null)
            return Percentage.ToString();

        return string.Empty;
    }

    public static implicit operator LengthPercentage(Length length)
    {
        return new LengthPercentage(length);
    }

    public static implicit operator LengthPercentage(SvgPercentage svgPercentage)
    {
        return new LengthPercentage(svgPercentage);
    }

    public static implicit operator LengthPercentage(double value)
    {
        Length length = new(value);
        return new LengthPercentage(length);
    }

    public static implicit operator LengthPercentage(string text)
    {
        return text == null
            ? Empty
            : Parse(text);
    }

    public static implicit operator LengthPercentage?(string text)
    {
        if (text == null)
            return null;

        return Parse(text);
    }
}