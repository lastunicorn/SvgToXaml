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

using System.Text.RegularExpressions;

namespace DustInTheWind.SvgToXaml.Svg;

public record SvgColor
{
    private static readonly Regex Regex = new(@"^\s*#([\dabcdef]{3}|[\dabcdef]{6}|[\dabcdef]{8})\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);

    private readonly string rawValue;

    public byte A { get; set; }

    public byte R { get; set; }

    public byte G { get; set; }

    public byte B { get; set; }

    public bool IsEmpty => rawValue == null;

    public SvgColor(string text)
    {
        if (text != null)
        {
            Match match = Regex.Match(text);

            if (match.Success)
            {
                rawValue = match.Groups[1].Value;

                if (rawValue.Length == 3)
                {
                    A = byte.MaxValue;
                    R = GetChanelValue(rawValue[0]);
                    G = GetChanelValue(rawValue[1]);
                    B = GetChanelValue(rawValue[2]);
                }
                else if (rawValue.Length == 6)
                {
                    A = byte.MaxValue;
                    R = GetChanelValue(rawValue.Substring(0, 2));
                    G = GetChanelValue(rawValue.Substring(2, 2));
                    B = GetChanelValue(rawValue.Substring(4, 2));
                }
                else if (rawValue.Length == 8)
                {
                    A = GetChanelValue(rawValue.Substring(0, 2));
                    R = GetChanelValue(rawValue.Substring(2, 2));
                    G = GetChanelValue(rawValue.Substring(4, 2));
                    B = GetChanelValue(rawValue.Substring(6, 2));
                }
            }
        }
    }

    private static byte GetChanelValue(char c)
    {
        string hexValue = new(c, 2);
        return Convert.ToByte(hexValue, 16);
    }

    private static byte GetChanelValue(string hexValue)
    {
        return Convert.ToByte(hexValue, 16);
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(rawValue)
            ? string.Empty
            : $"#{rawValue}";
    }

    public static implicit operator SvgColor(string text)
    {
        return new SvgColor(text);
    }
}