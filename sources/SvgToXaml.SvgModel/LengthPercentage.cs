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

public readonly record struct LengthPercentage
{
    public SvgLength? Length { get; }

    public SvgPercentage? Percentage { get; }

    public bool IsEmpty { get; }

    public static LengthPercentage Empty { get; } = new();

    public LengthPercentage()
    {
        Length = null;
        Percentage = null;
        IsEmpty = true;
    }

    public LengthPercentage(SvgLength svgLength)
    {
        Length = svgLength;
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
        if (text == null)
            return Empty;

        bool isLength = SvgLength.TryParse(text, out SvgLength svgLength);

        if (isLength)
            return new LengthPercentage(svgLength);

        bool isPercentage = SvgPercentage.TryParse(text, out SvgPercentage svgPercentage);

        if (isPercentage)
            return new LengthPercentage(svgPercentage);

        return Empty;
    }

    public static implicit operator LengthPercentage(SvgLength svgLength)
    {
        return new LengthPercentage(svgLength);
    }

    public static implicit operator LengthPercentage(SvgPercentage svgPercentage)
    {
        return new LengthPercentage(svgPercentage);
    }

    public static implicit operator LengthPercentage(double value)
    {
        SvgLength svgLength = new(value);
        return new LengthPercentage(svgLength);
    }

    public static implicit operator LengthPercentage(string text)
    {
        return Parse(text);
    }

    public static implicit operator LengthPercentage?(string text)
    {
        if (text == null)
            return (LengthPercentage?)null;

        return Parse(text);
    }
}