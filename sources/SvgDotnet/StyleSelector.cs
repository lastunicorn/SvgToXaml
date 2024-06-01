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

using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DustInTheWind.SvgDotnet;

public class StyleSelector : IEquatable<StyleSelector>
{
    private static readonly Regex Regex = new(@"^\s*(\.|#)?(\w+)\s*", RegexOptions.Multiline);

    public StyleSelectorType Type { get; }

    public string Name { get; }

    public StyleSelector(StyleSelectorType type, string name)
    {
        if (!Enum.IsDefined(typeof(StyleSelectorType), type)) throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(StyleSelectorType));

        Type = type;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public static StyleSelector Parse(string text)
    {
        if (text == null)
            return null;

        Match match = Regex.Match(text);

        if (!match.Success)
            return null;

        StyleSelectorType styleSelectorType = match.Groups[1].Value switch
        {
            "" => StyleSelectorType.Element,
            "." => StyleSelectorType.Class,
            "#" => StyleSelectorType.Id,
            _ => StyleSelectorType.None
        };
        string selectorName = match.Groups[2].Value;

        return new StyleSelector(styleSelectorType, selectorName);
    }

    public override string ToString()
    {
        return Type switch
        {
            StyleSelectorType.None => Name,
            StyleSelectorType.Element => Name,
            StyleSelectorType.Id => "#" + Name,
            StyleSelectorType.Class => "." + Name,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public bool Equals(StyleSelector other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Type == other.Type && Name == other.Name;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((StyleSelector)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Type, Name);
    }

    public static bool operator ==(StyleSelector left, StyleSelector right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(StyleSelector left, StyleSelector right)
    {
        return !Equals(left, right);
    }
}