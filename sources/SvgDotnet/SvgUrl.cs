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

public record SvgUrl
{
    private static readonly Regex UrlRegex = new(@"^\s*url\s*\(\s*#(.*?)\s*\)\s*$", RegexOptions.Singleline);

    public string ReferencedId { get; }

    public bool IsEmpty { get; }

    public static SvgUrl Empty { get; } = new();

    private SvgUrl()
    {
        IsEmpty = true;
    }

    public SvgUrl(string referencedId)
    {
        ReferencedId = referencedId ?? throw new ArgumentNullException(nameof(referencedId));
        IsEmpty = false;
    }

    public static SvgUrl Parse(string text)
    {
        if (text == null)
            return Empty;

        Match match = UrlRegex.Match(text);

        if (match.Success)
        {
            string referencedId = match.Groups[1].Value;
            return new SvgUrl(referencedId);
        }

        return Empty;
    }

    public override string ToString()
    {
        return IsEmpty
            ? string.Empty
            : $"url(#{ReferencedId})";
    }

    public static implicit operator SvgUrl(string text)
    {
        return Parse(text);
    }
}