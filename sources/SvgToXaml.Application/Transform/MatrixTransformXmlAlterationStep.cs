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

using System.Xml;
using System.Xml.XPath;

namespace DustInTheWind.SvgToXaml.Application.Transform;

internal class MatrixTransformXmlAlterationStep : IXmlAlterationStep
{
    private readonly XmlDocument xmlDocument;

    public MatrixTransformXmlAlterationStep(XmlDocument xmlDocument)
    {
        this.xmlDocument = xmlDocument ?? throw new ArgumentNullException(nameof(xmlDocument));
    }

    public void Execute()
    {
        XPathNodeIterator xPathNodeIterator = SelectMatrixNodes();

        while (xPathNodeIterator.MoveNext())
        {
            if (xPathNodeIterator.Current == null)
                continue;

            MoveBodyToAttribute(xPathNodeIterator.Current);
        }
    }

    private XPathNodeIterator SelectMatrixNodes()
    {
        XPathNavigator navigator = xmlDocument.CreateNavigator();

        XPathExpression xPathExpression = navigator.Compile("//xaml:MatrixTransform");

        XmlNamespaceManager xmlNamespaceManager = new(navigator.NameTable);
        xmlNamespaceManager.AddNamespace("xaml", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
        xPathExpression.SetContext(xmlNamespaceManager);

        return navigator.Select(xPathExpression);
    }

    private static void MoveBodyToAttribute(XPathNavigator xPathNavigator)
    {
        string value = xPathNavigator.Value;

        if (string.IsNullOrWhiteSpace(value))
            return;

        xPathNavigator.CreateAttribute(string.Empty, "Matrix", string.Empty, value);
        xPathNavigator.SetValue(string.Empty);
    }
}