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

namespace DustInTheWind.SvgDotnet;

public class StyleRuleSetCollection : Collection<StyleRuleSet>
{
    protected override void InsertItem(int index, StyleRuleSet item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, StyleRuleSet item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        base.SetItem(index, item);
    }

    public void AddRange(IEnumerable<StyleRuleSet> styleRuleSets)
    {
        if (styleRuleSets == null)
            throw new ArgumentNullException(nameof(styleRuleSets));

        IEnumerable<StyleRuleSet> styleRuleSetsNotNull = styleRuleSets.Where(x => x != null);

        foreach (StyleRuleSet styleRuleSet in styleRuleSetsNotNull)
            Items.Add(styleRuleSet);
    }
    public StyleRuleSet GetRuleSet(string selector)
    {
        if (selector == null)
            throw new ArgumentNullException(nameof(selector));

        return Items.FirstOrDefault(x => x.Selector == selector);
    }
}