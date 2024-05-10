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

namespace DustInTheWind.SvgDotnet;

public record SvgColor
{
    private static readonly Regex HashtagRegex = new(@"^\s*#([\dabcdef]{3}|[\dabcdef]{6}|[\dabcdef]{8})\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);
    private static readonly Regex RgbRegex = new(@"^\s*rgb\s*\(\s*(\d+)\s*,\s*(\d+)\s*,\s*(\d+)\s*\)\s*$", RegexOptions.Singleline | RegexOptions.IgnoreCase);

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

        Match hashTagMatch = HashtagRegex.Match(text);

        if (hashTagMatch.Success)
        {
            string rawValue = hashTagMatch.Groups[1].Value;

            SvgColor svgColor = ParseHashtagRgb(rawValue);

            if (svgColor != null)
            {
                value = svgColor;
                return true;
            }
        }

        Match rgbMatch = RgbRegex.Match(text);

        if (rgbMatch.Success)
        {
            string redRaw = rgbMatch.Groups[1].Value;
            string greenRaw = rgbMatch.Groups[2].Value;
            string blueRaw = rgbMatch.Groups[3].Value;

            bool success = byte.TryParse(redRaw, out byte red);

            if (!success)
            {
                value = null;
                return true;
            }

            success = byte.TryParse(greenRaw, out byte green);

            if (!success)
            {
                value = null;
                return true;
            }

            success = byte.TryParse(blueRaw, out byte blue);

            if (!success)
            {
                value = null;
                return true;
            }

            value = new SvgColor(red, green, blue);
            return true;
        }

        value = null;
        return false;
    }

    public static SvgColor Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Empty;

        SvgColor namedColor = SvgNamedColors.Get(text);

        if (namedColor != null)
            return namedColor;

        Match hashtagMatch = HashtagRegex.Match(text);

        if (hashtagMatch.Success)
        {
            string rawValue = hashtagMatch.Groups[1].Value;

            SvgColor svgColor = ParseHashtagRgb(rawValue);

            if (svgColor != null)
                return svgColor;
        }

        Match rgbMatch = RgbRegex.Match(text);

        if (rgbMatch.Success)
        {
            string redRaw = rgbMatch.Groups[1].Value;
            string greenRaw = rgbMatch.Groups[2].Value;
            string blueRaw = rgbMatch.Groups[3].Value;

            byte red = byte.Parse(redRaw);
            byte green = byte.Parse(greenRaw);
            byte blue = byte.Parse(blueRaw);

            return new SvgColor(red, green, blue);
        }

        throw new NotAColorException(text);
    }

    private static SvgColor ParseHashtagRgb(string rawValue)
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