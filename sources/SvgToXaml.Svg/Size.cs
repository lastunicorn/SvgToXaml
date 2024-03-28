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

namespace DustInTheWind.SvgToXaml.Svg;

public class Size
{
    public double Value { get; init; }

    public UnitOfMeasurement UnitOfMeasurement { get; init; }

    public static Size Zero { get; } = new()
    {
        Value = 0,
        UnitOfMeasurement = UnitOfMeasurement.None
    };

    public static implicit operator Size(double value)
    {
        return new Size
        {
            Value = value
        };
    }

    public static implicit operator Size(double? value)
    {
        if (value == null)
            return null;

        return new Size
        {
            Value = value.Value
        };
    }

    public static implicit operator Size(string text)
    {
        if (text == null)
            return Zero;

        string trimmedText = text.Trim();

        if (trimmedText.Length == 0)
            return Zero;

        if (trimmedText.EndsWith("px", StringComparison.OrdinalIgnoreCase))
        {
            return new Size
            {
                Value = double.Parse(trimmedText[..^2], CultureInfo.InvariantCulture),
                UnitOfMeasurement = UnitOfMeasurement.Pixels
            };
        }

        if (trimmedText.EndsWith("pt", StringComparison.OrdinalIgnoreCase))
        {
            return new Size
            {
                Value = double.Parse(trimmedText[..^2], CultureInfo.InvariantCulture),
                UnitOfMeasurement = UnitOfMeasurement.Points
            };
        }

        return new Size
        {
            Value = double.Parse(trimmedText, CultureInfo.InvariantCulture),
            UnitOfMeasurement = UnitOfMeasurement.None
        };
    }
}