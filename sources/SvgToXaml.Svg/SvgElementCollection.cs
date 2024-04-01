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

namespace DustInTheWind.SvgToXaml.Svg;

public class SvgElementCollection<T> : Collection<T>
    where T : SvgElement
{
    private readonly SvgContainer parent;

    public SvgElementCollection(SvgContainer parent)
    {
        this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

    protected override void InsertItem(int index, T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        item.Parent = parent;
        base.InsertItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
        Items[index].Parent = null;

        base.RemoveItem(index);
    }

    protected override void SetItem(int index, T item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        Items[index].Parent = null;
        item.Parent = parent;

        base.SetItem(index, item);
    }

    protected override void ClearItems()
    {
        foreach (T item in Items)
            item.Parent = null;

        base.ClearItems();
    }

    public SvgElement FindChild(string id)
    {
        foreach (T svgElement in Items)
        {
            if (svgElement.Id == id)
                return svgElement;

            if (svgElement is SvgContainer svgContainer)
            {
                SvgElement child = svgContainer.Children.FindChild(id);

                if (child != null)
                    return child;
            }
        }

        return null;
    }
}