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

internal class SvgTreePath
{
    private SvgTreePathNode firstNode;
    private SvgTreePathNode lastNode;

    public void Add(string name, string id = null)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (lastNode == null)
        {
            firstNode = new SvgTreePathNode(1, name, id);
            lastNode = firstNode;
        }
        else
        {
            lastNode.SetNext(name, id);
            lastNode = lastNode.Next;
        }
    }

    public void RemoveLast()
    {
        if (lastNode == null)
            throw new Exception("Path is empty. Cannot remove nodes.");

        if (lastNode.Previous == null)
        {
            firstNode = null;
            lastNode = null;
        }
        else
        {
            lastNode = lastNode.Previous;
            lastNode.RemoveNext();
        }
    }

    public void SetAttributeOnLast(string attributeName)
    {
        if (lastNode == null)
            return;

        lastNode.Attribute = attributeName;
    }

    public override string ToString()
    {
        IEnumerable<SvgTreePathNode> nodes = EnumerateNodes();
        return string.Join(".", nodes);
    }

    private IEnumerable<SvgTreePathNode> EnumerateNodes()
    {
        SvgTreePathNode node = firstNode;

        while (node != null)
        {
            yield return node;
            node = node.Next;
        }
    }
}