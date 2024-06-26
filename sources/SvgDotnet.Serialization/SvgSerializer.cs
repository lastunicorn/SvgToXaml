﻿// SvgToXaml
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

using System.Xml.Serialization;
using DustInTheWind.SvgDotnet.Serialization.Conversion;
using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization;

public class SvgSerializer
{
    private readonly XmlSerializer xmlSerializer;

    private DeserializationContext deserializationContext;

    public SvgDeserializationOptions Options { get; } = new();

    public SvgSerializer()
    {
        xmlSerializer = new XmlSerializer(typeof(XmlSvg));

        xmlSerializer.UnknownNode += HandleUnknownNode;
    }

    private void HandleUnknownNode(object sender, XmlNodeEventArgs e)
    {
        bool isNamespaceIgnored = Options.IgnoredNamespaces.Contains(e.NamespaceURI);
        if (isNamespaceIgnored)
        {
            string path = deserializationContext.Path.ToString();
            deserializationContext.Issues.AddInfo(path, $"[{e.Name}] Ignoring XML {e.NodeType}. Line: {e.LineNumber}:{e.LinePosition}");
        }
        else
        {
            string path = deserializationContext.Path.ToString();
            deserializationContext.Issues.AddWarning(path, $"[{e.Name}] Unknown XML {e.NodeType}. Line: {e.LineNumber}:{e.LinePosition}");
        }
    }

    public DeserializationResult Deserialize(string svg)
    {
        using StringReader stringReader = new(svg);
        return DeserializeInternal(stringReader);
    }

    public DeserializationResult Deserialize(Stream stream)
    {
        using StreamReader streamReader = new(stream);
        return DeserializeInternal(streamReader);
    }

    public DeserializationResult Deserialize(TextReader textReader)
    {
        return DeserializeInternal(textReader);
    }

    private DeserializationResult DeserializeInternal(TextReader textReader)
    {
        deserializationContext = new DeserializationContext();

        XmlSvg xmlSvg = DeserializeSvgObject(textReader);

        Svg svg = xmlSvg == null
            ? null
            : Convert(xmlSvg);

        return BuildResult(svg);
    }

    private XmlSvg DeserializeSvgObject(TextReader textReader)
    {
        try
        {
            XmlSvg xmlSvg = xmlSerializer.Deserialize(textReader) as XmlSvg;

            if (xmlSvg == null)
            {
                string path = deserializationContext.Path.ToString();
                deserializationContext.Issues.AddError(path, "The text is not a valid svg document.");
            }

            return xmlSvg;
        }
        catch (Exception ex)
        {
            string path = deserializationContext.Path.ToString();
            deserializationContext.Issues.AddError(path, ex.ToString());

            return null;
        }
    }

    private Svg Convert(XmlSvg xmlSvg)
    {
        XmlSvgToModelConversion conversion = new(xmlSvg, deserializationContext);
        return conversion.Execute();
    }

    private DeserializationResult BuildResult(Svg svg)
    {
        return new DeserializationResult
        {
            Svg = svg,
            Issues = deserializationContext.Issues.ToList()
        };
    }
}