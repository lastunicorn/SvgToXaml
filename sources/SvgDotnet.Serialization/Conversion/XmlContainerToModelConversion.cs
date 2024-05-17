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
    protected XmlContainerToModelConversion(TXml xmlContainer, DeserializationContext deserializationContext)
        : base(xmlContainer, deserializationContext)
    {
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        if (XmlElement.Children != null)
        {
            IEnumerable<SvgElement> elements = XmlElement.Children
                .Select(CreateConversionFor)
                .Where(x => x != null)
                .Select(x => x.Execute());

            foreach (SvgElement svgElement in elements)
                SvgElement.Children.Add(svgElement);
        }
    }

    private IToModelConversion<SvgElement> CreateConversionFor(object objectToConvert)
    {
        switch (objectToConvert)
        {
            case XmlDesc desc:
                return new XmlDescToModelConversion(desc, DeserializationContext);

            case XmlTitle title:
                return new XmlTitleToModelConversion(title, DeserializationContext);

            case XmlLinearGradient linearGradient:
                return new XmlLinearGradientToModelConversion(linearGradient, DeserializationContext);

            case XmlRadialGradient radialGradient:
                return new XmlRadialGradientToModelConversion(radialGradient, DeserializationContext);

            case XmlCircle circle:
                return new XmlCircleToModelConversion(circle, DeserializationContext);

            case XmlEllipse ellipse:
                return new XmlEllipseToModelConversion(ellipse, DeserializationContext);

            case XmlLine line:
                return new XmlLineToModelConversion(line, DeserializationContext);

            case XmlPath path:
                return new XmlPathToModelConversion(path, DeserializationContext);

            case XmlPolygon polygon:
                return new XmlPolygonToModelConversion(polygon, DeserializationContext);

            case XmlPolyline polyline:
                return new XmlPolylineToModelConversion(polyline, DeserializationContext);

            case XmlRect rect:
                return new XmlRectToModelConversion(rect, DeserializationContext);

            case XmlDefs defs:
                return new XmlDefsToModelConversion(defs, DeserializationContext);

            case XmlG childG:
                return new XmlGroupToModelConversion(childG, DeserializationContext);

            case XmlSvg childSvg:
                return new XmlSvgToModelConversion(childSvg, DeserializationContext);

            case XmlSymbol symbol:
                return new XmlSymbolToModelConversion(symbol, DeserializationContext);

            case XmlUse use:
                return new XmlUseToModelConversion(use, DeserializationContext);

            case XmlClipPath clipPath:
                return new XmlClipPathToModelConversion(clipPath, DeserializationContext);

            case XmlStyle style:
                return new XmlStyleToModelConversion(style, DeserializationContext);

            case XmlText text:
                return new XmlTextToModelConversion(text, DeserializationContext);

            default:
                DeserializationIssue deserializationIssue = new("Xml deserialization", $"Unknown element type {objectToConvert.GetType().Name} in {ElementName}.");
                DeserializationContext.Errors.Add(deserializationIssue);
                return null;
        }
    }
}