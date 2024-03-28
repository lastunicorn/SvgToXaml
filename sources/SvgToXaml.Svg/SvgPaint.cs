// Country Flags
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

namespace DustInTheWind.SvgToXaml.Svg;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Possible Values = none | <color> | <url> [none | <color>]? | context-fill | context-stroke 
/// </remarks>
public class SvgPaint
{
    public SvgColor Color { get; }

    public SvgUrl Url { get; }

    public bool IsNone { get; }

    public SvgPaint(string text)
    {
        if (text?.Trim() == "none")
        {
            IsNone = true;
        }
        else
        {
            Color = text;
            Url = text;
        }
    }

    public static implicit operator SvgPaint(string text)
    {
        return text == null
            ? null
            : new SvgPaint(text);
    }
}