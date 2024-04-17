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

namespace DustInTheWind.SvgToXaml.SvgSerialization;

internal class SvgTreePathNode
{
    private readonly int index;
    private readonly string name;
    private readonly string id;
    private int childCount;

    public SvgTreePathNode Previous { get; init; }

    public SvgTreePathNode Next { get; private set; }

    public string Attribute { get; set; }

    public SvgTreePathNode(int index, string name, string id = null)
    {
        if (index <= 0) throw new ArgumentOutOfRangeException(nameof(index));

        this.index = index;
        this.name = name ?? throw new ArgumentNullException(nameof(name));
        this.id = id;
    }

    public void SetNext(string childName, string childId = null)
    {
        childCount++;

        Next = new SvgTreePathNode(childCount, childName, childId)
        {
            Previous = this
        };
    }

    public override string ToString()
    {
        string text = id == null
            ? $"{index}({name})"
            : $"{index}({name}:{id})";

        if (Attribute != null)
            text += $"@{Attribute}";

        return text;
    }

    public void RemoveNext()
    {
        Next = null;
    }
}