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

/// <remarks>
/// Possible Values = none | {color} | {url} [none | {color}]? | context-fill | context-stroke 
/// </remarks>
public class SvgPaint
{
    public SvgColor Color { get; private init; }

    public SvgUrl Url { get; private init; }

    public bool IsNone { get; private init; }

    public static SvgPaint Parse(string text)
    {
        if (text == null)
            return null;

        bool isNone = text.Trim().Equals("none", StringComparison.InvariantCultureIgnoreCase);

        if (isNone)
        {
            return new SvgPaint
            {
                IsNone = true
            };
        }

        bool isColor = SvgColor.TryParse(text, out SvgColor svgColor);

        if (isColor)
        {
            return new SvgPaint
            {
                Color = svgColor
            };
        }

        return new SvgPaint
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

    public static implicit operator SvgPaint(string text)
    {
        return text == null
            ? null
            : Parse(text);
    }
}