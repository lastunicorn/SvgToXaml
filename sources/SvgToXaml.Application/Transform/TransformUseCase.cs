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

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using DustInTheWind.SvgToXaml.Conversion;
using DustInTheWind.SvgToXaml.Infrastructure;
using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization;
using MediatR;

namespace DustInTheWind.SvgToXaml.Application.Transform;

internal class TransformUseCase : IRequestHandler<TransformRequest>
{
    private readonly EventBus eventBus;

    public TransformUseCase(EventBus eventBus)
    {
        this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    public async Task Handle(TransformRequest request, CancellationToken cancellationToken)
    {
        string xamlText = Transform(request.SvgText);

        XamlTextChangedEvent ev = new()
        {
            XamlText = xamlText
        };
        await eventBus.Publish(ev, cancellationToken);
    }

    private static string Transform(string svgText)
    {
        try
        {
            if (string.IsNullOrEmpty(svgText))
            {
                return null;
            }

            Svg svg = Parse(svgText);
            Canvas canvas = ConvertToXaml(svg);
            string xaml = Serialize(canvas);
            xaml = Alter(xaml);
            return xaml;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public static Svg Parse(string svg)
    {
        using StringReader stringReader = new(svg);

        SvgSerializer serializer = new();
        DeserializationResult deserializationResult = serializer.Deserialize(stringReader);
        return deserializationResult.Svg;
    }

    private static Canvas ConvertToXaml(Svg svg)
    {
        SvgConversion svgConversion = new(svg);
        return svgConversion.Execute();
    }

    private static string Serialize(Canvas canvas)
    {
        if (canvas == null)
            return string.Empty;

        using MemoryStream ms = new();
        XmlWriterSettings xmlWriterSettings = new()
        {
            Indent = true,
            NewLineOnAttributes = true,
            OmitXmlDeclaration = true
        };
        using XmlWriter xmlWriter = XmlWriter.Create(ms, xmlWriterSettings);

        ResourceDictionary resourceDictionary = new()
        {
            { "SvgTransform", canvas }
        };

        XamlWriter.Save(resourceDictionary, xmlWriter);

        ms.Position = 0;
        using StreamReader sr = new(ms);

        return sr.ReadToEnd();
    }

    private static string Alter(string xml)
    {
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml);

        MatrixTransformXmlAlteration xmlAlteration = new(xmlDocument);
        xmlAlteration.Execute();

        return SerializeXmlDocument(xmlDocument);
    }

    private static string SerializeXmlDocument(XmlDocument xmlDocument)
    {
        using MemoryStream ms = new();

        XmlWriterSettings settings = new()
        {
            Indent = true,
            NewLineOnAttributes = true
        };

        using XmlWriter xmlWriter = XmlWriter.Create(ms, settings);

        xmlDocument.WriteTo(xmlWriter);
        xmlWriter.Flush();

        ms.Position = 0;

        using StreamReader sr = new(ms);
        return sr.ReadToEnd();
    }
}