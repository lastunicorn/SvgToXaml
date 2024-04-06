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

using System.Text.RegularExpressions;

namespace DustInTheWind.SvgToXaml.SvgModel;

public record SvgColor
{
    private static readonly Regex Regex = new(@"^\s*#([\dabcdef]{3}|[\dabcdef]{6}|[\dabcdef]{8})\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);

    public bool IsEmpty { get; }

    public byte Red { get; }

    public byte Green { get; }

    public byte Blue { get; }

    public byte? Alpha { get; }

    public static SvgColor Empty { get; } = new();

    private SvgColor()
    {
        IsEmpty = true;
    }

    public SvgColor(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public SvgColor(byte red, byte green, byte blue, byte alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }

    public static bool TryParse(string text, out SvgColor value)
    {
        if (string.IsNullOrEmpty(text))
        {
            value = null;
            return false;
        }

        SvgColor namedColor = SvgNamedColors.Get(text);

        if (namedColor != null)
        {
            value = namedColor;
            return true;
        }

        Match match = Regex.Match(text);

        if (!match.Success)
        {
            value = null;
            return false;
        }

        string rawValue = match.Groups[1].Value;

        SvgColor svgColor = ParseRgb(rawValue);

        if (svgColor == null)
        {
            value = null;
            return false;
        }

        value = svgColor;
        return true;
    }

    public static SvgColor Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Empty;

        SvgColor namedColor = SvgNamedColors.Get(text);

        if (namedColor != null)
            return namedColor;

        Match match = Regex.Match(text);

        if (!match.Success)
            throw new NotAColorException(text);

        string rawValue = match.Groups[1].Value;

        SvgColor svgColor = ParseRgb(rawValue);

        if (svgColor == null)
            throw new NotAColorException(text);

        return svgColor;
    }

    private static SvgColor ParseRgb(string rawValue)
    {
        switch (rawValue.Length)
        {
            case 3:
                {
                    byte red = GetChanelValue(rawValue[0]);
                    byte green = GetChanelValue(rawValue[1]);
                    byte blue = GetChanelValue(rawValue[2]);
                    return new SvgColor(red, green, blue);
                }

            case 6:
                {
                    byte red = GetChanelValue(rawValue.Substring(0, 2));
                    byte green = GetChanelValue(rawValue.Substring(2, 2));
                    byte blue = GetChanelValue(rawValue.Substring(4, 2));
                    return new SvgColor(red, green, blue);
                }

            case 8:
                {
                    byte red = GetChanelValue(rawValue.Substring(0, 2));
                    byte green = GetChanelValue(rawValue.Substring(2, 2));
                    byte blue = GetChanelValue(rawValue.Substring(4, 2));
                    byte alpha = GetChanelValue(rawValue.Substring(6, 2));
                    return new SvgColor(red, green, blue, alpha);
                }

            default:
                return null;
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

    public SvgColor SetAlpha(SvgOpacity opacity)
    {
        return new SvgColor(Red, Green, Blue, opacity);
    }

    public override string ToString()
    {
        if (IsEmpty)
            return string.Empty;

        return Alpha.HasValue
            ? "#" + Convert.ToHexString(new[] { Red, Green, Blue, Alpha.Value })
            : "#" + Convert.ToHexString(new[] { Red, Green, Blue });
    }

    public static implicit operator SvgColor(string text)
    {
        return Parse(text);
    }
}