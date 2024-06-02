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

internal class XmlClipPathToModelConversion : XmlElementToModelConversion<XmlClipPath, SvgClipPath>
{
    protected override string ElementName => "clipPath";

    public XmlClipPathToModelConversion(XmlClipPath xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgClipPath CreateSvgElement()
    {
        return new SvgClipPath();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        if (SvgElement.TabIndex != null)
        {
            SvgElement.TabIndex = null;

            string path = DeserializationContext.Path.ToString();
            DeserializationContext.Issues.AddWarning(path, $"[{ElementName}] 'tabIndex' attribute is not allowed.");
        }

        ConvertChildren();
    }

    private void ConvertChildren()
    {
        if (XmlElement.Children != null)
        {
            IEnumerable<IToModelConversion<SvgElement>> conversions = CreateConversionsForAllChildren();

            foreach (IToModelConversion<SvgElement> conversion in conversions)
            {
                SvgElement childSvgElement = conversion.Execute();
                SvgElement.Children.Add(childSvgElement);
            }
        }
    }

    protected IEnumerable<IToModelConversion<SvgElement>> CreateConversionsForAllChildren()
    {
        foreach (object serializationChild in XmlElement.Children)
        {
            switch (serializationChild)
            {
                case XmlDesc desc:
                    yield return new XmlDescToModelConversion(desc, DeserializationContext);
                    break;

                case XmlTitle title:
                    yield return new XmlTitleToModelConversion(title, DeserializationContext);
                    break;

                case XmlCircle circle:
                    yield return new XmlCircleToModelConversion(circle, DeserializationContext);
                    break;

                case XmlEllipse ellipse:
                    yield return new XmlEllipseToModelConversion(ellipse, DeserializationContext);
                    break;

                case XmlLine line:
                    yield return new XmlLineToModelConversion(line, DeserializationContext);
                    break;

                case XmlPath xmlPath:
                    yield return new XmlPathToModelConversion(xmlPath, DeserializationContext);
                    break;

                case XmlPolygon polygon:
                    yield return new XmlPolygonToModelConversion(polygon, DeserializationContext);
                    break;

                case XmlPolyline polyline:
                    yield return new XmlPolylineToModelConversion(polyline, DeserializationContext);
                    break;

                case XmlRect rect:
                    yield return new XmlRectToModelConversion(rect, DeserializationContext);
                    break;

                case XmlText text:
                    yield return new XmlTextToModelConversion(text, DeserializationContext);
                    break;

                case XmlUse use:
                    yield return new XmlUseToModelConversion(use, DeserializationContext);
                    break;

                default:
                    string path = DeserializationContext.Path.ToString();
                    DeserializationContext.Issues.AddError(path, $"[{ElementName}] Unknown element type {serializationChild.GetType().Name}.");
                    break;
            }
        }
    }
}