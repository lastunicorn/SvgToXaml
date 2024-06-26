﻿// SvgToXaml
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

/// <remarks>
/// Possible Values = none | {color} | {url} [none | {color}]? | context-fill | context-stroke 
/// </remarks>
public class Paint : IEquatable<Paint>
{
    public SvgColor Color { get; private init; }

    public SvgUrl Url { get; private init; }

    public bool IsNone { get; private init; }

    private Paint()
    {
    }

    public Paint(SvgColor svgColor)
    {
        Color = svgColor ?? throw new ArgumentNullException(nameof(svgColor));
    }

    public Paint(SvgUrl svgUrl)
    {
        Url = svgUrl ?? throw new ArgumentNullException(nameof(svgUrl));
    }

    public static Paint Parse(string text)
    {
        if (text == null)
            return null;

        bool isNone = text.Trim().Equals("none", StringComparison.InvariantCultureIgnoreCase);

        if (isNone)
        {
            return new Paint
            {
                IsNone = true
            };
        }

        bool isColor = SvgColor.TryParse(text, out SvgColor svgColor);

        if (isColor)
        {
            return new Paint
            {
                Color = svgColor
            };
        }

        return new Paint
        {
            Url = text
        };
    }

    public override string ToString()
    {
        if (IsNone)
            return "none";

        if (!Color.IsEmpty)
            return Color.ToString();

        if (!Url.IsEmpty)
            return Url.ToString();

        return string.Empty;
    }

    public static implicit operator Paint(string text)
    {
        return text == null
            ? null
            : Parse(text);
    }

    public bool Equals(Paint other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(Color, other.Color) && Equals(Url, other.Url) && IsNone == other.IsNone;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Paint)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Color, Url, IsNone);
    }

    public static bool operator ==(Paint left, Paint right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Paint left, Paint right)
    {
        return !Equals(left, right);
    }
}