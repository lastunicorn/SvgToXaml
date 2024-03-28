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

using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace DustInTheWind.SvgToXaml.Svg;

public class SvgStyleSheet : Collection<SvgStyleRuleSet>
{
    private static readonly Regex Regex = new(@"\.(\w+)\s*{\s*(.*?)\s*}", RegexOptions.Multiline);

    public SvgStyleRuleSet this[string name] => Items.FirstOrDefault(x => x.Selector == name);

    public static implicit operator SvgStyleSheet(string text)
    {
        if (text == null)
            return null;

        MatchCollection matches = Regex.Matches(text);

        IEnumerable<SvgStyleRuleSet> items = matches
            .Select(x => new SvgStyleRuleSet
            {
                Selector = x.Groups[1].Value,
                Declarations = x.Groups[2].Value
            });

        SvgStyleSheet svgClasses = new();

        foreach (SvgStyleRuleSet item in items)
            svgClasses.Add(item);

        return svgClasses;
    }
}