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
using DustInTheWind.SvgDotnet;
using DustInTheWind.SvgDotnet.Serialization;
using DustInTheWind.SvgToXaml.Conversion;
using DustInTheWind.SvgToXaml.Infrastructure;
using MediatR;

namespace DustInTheWind.SvgToXaml.Application.Transform;

internal class TransformUseCase : IRequestHandler<TransformRequest>
{
    private readonly EventBus eventBus;
    private XamlTextChangedEvent xamlTextChangedEvent;

    public TransformUseCase(EventBus eventBus)
    {
        this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    public async Task Handle(TransformRequest request, CancellationToken cancellationToken)
    {
        xamlTextChangedEvent = new XamlTextChangedEvent();

        Transform(request.SvgText, request.ShouldOptimize);
        await eventBus.Publish(xamlTextChangedEvent, cancellationToken);
    }

    private void Transform(string svgText, bool shouldOptimize)
    {
        try
        {
            if (!string.IsNullOrEmpty(svgText))
            {
                Svg svg = Deserialize(svgText);

                if (svg == null)
                    return;

                Canvas canvas = Convert(svg);

                if (canvas == null)
                    return;

                if (shouldOptimize)
                    canvas = Optimize(canvas);

                string xaml = Serialize(canvas);

                if (string.IsNullOrEmpty(xaml))
                    return;

                xaml = XmlAlter(xaml);

                xamlTextChangedEvent.XamlText = xaml;
            }
        }
        catch (Exception ex)
        {
            ErrorInfo errorInfo = new()
            {
                Message = ex.ToString()
            };
            xamlTextChangedEvent.Errors.Add(errorInfo);
        }
    }

    private Svg Deserialize(string svgText)
    {
        SvgSerializer serializer = new();
        DeserializationResult deserializationResult = serializer.Deserialize(svgText);

        IEnumerable<ErrorInfo> errorInfos = deserializationResult.Errors
            .Select(x => new ErrorInfo
            {
                Message = $"{x.Path} : {x.Message}"
            });
        xamlTextChangedEvent.Errors.AddRange(errorInfos);

        IEnumerable<ErrorInfo> warningsInfos = deserializationResult.Warnings
            .Select(x => new ErrorInfo
            {
                Message = $"{x.Path} : {x.Message}"
            });
        xamlTextChangedEvent.Warning.AddRange(warningsInfos);

        return deserializationResult.Svg;
    }

    private Canvas Convert(Svg svg)
    {
        ConversionContext conversionContext = ConvertToXaml(svg);

        IEnumerable<ErrorInfo> errorInfos = conversionContext.Errors
            .Select(x => new ErrorInfo
            {
                Message = $"{x.Path} : {x.Message}"
            });
        xamlTextChangedEvent.Errors.AddRange(errorInfos);

        IEnumerable<ErrorInfo> warningInfos = conversionContext.Warnings
            .Select(x => new ErrorInfo
            {
                Message = $"{x.Path} : {x.Message}"
            });
        xamlTextChangedEvent.Warning.AddRange(warningInfos);

        return conversionContext.Canvas;
    }

    private static ConversionContext ConvertToXaml(Svg svg)
    {
        ConversionContext conversionContext = new();

        SvgToXamlConversion svgToXamlConversion = new(svg, conversionContext);
        conversionContext.Canvas = svgToXamlConversion.Execute();

        return conversionContext;
    }

    private Canvas Optimize(Canvas canvas)
    {
        CanvasOptimization canvasOptimization = new(canvas);
        canvasOptimization.Execute();

        if (canvasOptimization.Errors.Count > 0)
            xamlTextChangedEvent.Errors.AddRange(canvasOptimization.Errors);

        if (canvasOptimization.Infos.Count > 0)
            xamlTextChangedEvent.Info.AddRange(canvasOptimization.Infos);

        return canvasOptimization.Canvas;
    }

    private static string Serialize(Canvas canvas)
    {
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

    private static string XmlAlter(string xml)
    {
        XmlAlteration xmlAlteration = new(xml);
        xmlAlteration.AddStep(typeof(MatrixTransformXmlAlterationStep));
        xmlAlteration.Execute();
        return xmlAlteration.SerializeResult();
    }
}