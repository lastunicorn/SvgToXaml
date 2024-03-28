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

namespace DustInTheWind.SvgToXaml.Svg;

public record SvgUrl
{
    private static readonly Regex UrlRegex = new(@"^\s*url\s*\(\s*#(.*?)\s*\)\s*$", RegexOptions.Singleline);

    public string ReferencedId { get; }

    public bool IsEmpty => ReferencedId == null;

    public SvgUrl(string text)
    {
        if (text != null)
        {
            Match match = UrlRegex.Match(text);

            if (match.Success)
                ReferencedId = match.Groups[1].Value;
        }
    }

    public override string ToString()
    {
        return $"url(#{ReferencedId})";
    }

    public static implicit operator SvgUrl(string text)
    {
        return new SvgUrl(text);
    }
}