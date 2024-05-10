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

namespace DustInTheWind.SvgDotnet.Serialization;

internal class TreePath
{
    private Node firstNode;
    private Node lastNode;

    public void AddElement(string name, string id = null)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        ElementNode newElementNode = new(name, id);
        AddNodeInternal(newElementNode);
    }

    public void AddAttribute(string name)
    {
        AttributeNode newAttributeNode = new(name);
        AddNodeInternal(newAttributeNode);
    }

    private void AddNodeInternal(Node newNode)
    {
        switch (lastNode)
        {
            case null:
                firstNode = newNode;
                lastNode = firstNode;
                break;

            case ElementNode elementLastNode:
                elementLastNode.Next = newNode;
                lastNode = newNode;
                break;

            default:
                throw new Exception("Cannot add more element nodes. Last element is an attribute.");
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
            Node previousNode = lastNode.Previous;

            lastNode.Previous = null;
            previousNode.Next = null;

            lastNode = previousNode;
        }
    }

    public override string ToString()
    {
        IEnumerable<Node> nodes = EnumerateNodes();
        return string.Join(".", nodes);
    }

    private IEnumerable<Node> EnumerateNodes()
    {
        Node node = firstNode;

        while (node != null)
        {
            yield return node;
            node = node.Next;
        }
    }
}