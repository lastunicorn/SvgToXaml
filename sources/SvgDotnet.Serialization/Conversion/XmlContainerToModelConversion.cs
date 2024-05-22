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

using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

internal abstract class XmlContainerToModelConversion<TXml, TSvg> : XmlElementToModelConversion<TXml, TSvg>
    where TXml : XmlContainer
    where TSvg : SvgContainer
{
    protected List<Type> AllowedChildTypes { get; } = new();

    protected XmlContainerToModelConversion(TXml xmlContainer, DeserializationContext deserializationContext)
        : base(xmlContainer, deserializationContext)
    {
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertChildren();
    }

    private void ConvertChildren()
    {
        if (XmlElement.Children == null)
            return;

        foreach (XmlElement xmlElement in XmlElement.Children)
        {
            Type elementType = xmlElement.GetType();
            bool isAllowed = AllowedChildTypes.Contains(elementType);

            if (isAllowed)
            {
                IToModelConversion<SvgElement> conversion = CreateConversionFor(xmlElement);
                SvgElement svgElement = conversion.Execute();
                SvgElement.Children.Add(svgElement);
            }
            else
            {
                DeserializationIssue deserializationIssue = new("Xml deserialization", $"Child type '{elementType.Name}' is not allowed in '{ElementName}'. Child is ignored.");
                DeserializationContext.Errors.Add(deserializationIssue);
            }
        }
    }

    private IToModelConversion<SvgElement> CreateConversionFor(object objectToConvert)
    {
        switch (objectToConvert)
        {
            case XmlDesc xmlDesc:
                return new XmlDescToModelConversion(xmlDesc, DeserializationContext);

            case XmlTitle xmlTitle:
                return new XmlTitleToModelConversion(xmlTitle, DeserializationContext);

            case XmlLinearGradient xmlLinearGradient:
                return new XmlLinearGradientToModelConversion(xmlLinearGradient, DeserializationContext);

            case XmlRadialGradient xmlRadialGradient:
                return new XmlRadialGradientToModelConversion(xmlRadialGradient, DeserializationContext);

            case XmlCircle xmlCircle:
                return new XmlCircleToModelConversion(xmlCircle, DeserializationContext);

            case XmlEllipse xmlEllipse:
                return new XmlEllipseToModelConversion(xmlEllipse, DeserializationContext);

            case XmlLine xmlLine:
                return new XmlLineToModelConversion(xmlLine, DeserializationContext);

            case XmlPath xmlPath:
                return new XmlPathToModelConversion(xmlPath, DeserializationContext);

            case XmlPolygon xmlPolygon:
                return new XmlPolygonToModelConversion(xmlPolygon, DeserializationContext);

            case XmlPolyline xmlPolyline:
                return new XmlPolylineToModelConversion(xmlPolyline, DeserializationContext);

            case XmlRect xmlRect:
                return new XmlRectToModelConversion(xmlRect, DeserializationContext);

            case XmlDefs xmlDefs:
                return new XmlDefsToModelConversion(xmlDefs, DeserializationContext);

            case XmlG childXmlG:
                return new XmlGroupToModelConversion(childXmlG, DeserializationContext);

            case XmlSvg childXmlSvg:
                return new XmlSvgToModelConversion(childXmlSvg, DeserializationContext);

            case XmlSymbol xmlSymbol:
                return new XmlSymbolToModelConversion(xmlSymbol, DeserializationContext);

            case XmlUse xmlUse:
                return new XmlUseToModelConversion(xmlUse, DeserializationContext);

            case XmlClipPath xmlClipPath:
                return new XmlClipPathToModelConversion(xmlClipPath, DeserializationContext);

            case XmlStyle xmlStyle:
                return new XmlStyleToModelConversion(xmlStyle, DeserializationContext);

            case XmlText xmlText:
                return new XmlTextToModelConversion(xmlText, DeserializationContext);

            default:
                DeserializationIssue deserializationIssue = new("Xml deserialization", $"Unknown element type {objectToConvert.GetType().Name} in {ElementName}.");
                DeserializationContext.Errors.Add(deserializationIssue);
                return null;
        }
    }
}