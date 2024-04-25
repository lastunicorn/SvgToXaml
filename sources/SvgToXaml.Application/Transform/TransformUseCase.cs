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
                DeserializationResult deserializationResult = Parse(svgText);

                IEnumerable<ErrorInfo> errorInfos = deserializationResult.Errors
                    .Concat(deserializationResult.Warnings)
                    .Select(x => new ErrorInfo
                    {
                        Message = $"{x.Path} : {x.Message}"
                    });
                xamlTextChangedEvent.Errors.AddRange(errorInfos);

                if (deserializationResult.Svg == null)
                    return;

                ConversionContext conversionContext = ConvertToXaml(deserializationResult.Svg);
                Canvas canvas = conversionContext.Canvas;

                IEnumerable<ErrorInfo> conversionErrorInfos = conversionContext.Errors
                    .Select(x => new ErrorInfo
                    {
                        Message = $"{x.Path} : {x.Message}"
                    });
                xamlTextChangedEvent.Errors.AddRange(conversionErrorInfos);

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

    public static DeserializationResult Parse(string svg)
    {
        SvgSerializer serializer = new();
        return serializer.Deserialize(svg);
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
        int i = 0;

        while (i < canvas.Children.Count)
        {
            UIElement child = canvas.Children[i];

            if (child is Canvas childCanvas)
            {
                Optimize(childCanvas);

                if (childCanvas.Children.Count == 0)
                {
                    canvas.Children.RemoveAt(i);

                    ErrorInfo errorInfo = new()
                    {
                        Message = $"Optimization: UIElement removed - {child.GetType().Name}"
                    };
                    xamlTextChangedEvent.Info.Add(errorInfo);

                    continue;
                }

                bool containsTransformations = childCanvas.RenderTransform != null && childCanvas.RenderTransform != System.Windows.Media.Transform.Identity;
                if (!containsTransformations)
                {
                    bool containsLanguage = childCanvas.Language != null && childCanvas.Language != XmlLanguage.Empty && childCanvas.Language != XmlLanguage.GetLanguage("en-US");
                    if (!containsLanguage)
                    {
                        int grandChildrenCount = childCanvas.Children.Count;

                        while (childCanvas.Children.Count > 0)
                        {
                            UIElement grandChild = childCanvas.Children[0];

                            grandChild.RemoveFromParent(out DependencyObject _, out int? _);
                            grandChild.AddToParent(canvas, i);

                            i++;
                        }

                        canvas.Children.RemoveAt(i);

                        ErrorInfo errorInfo = new()
                        {
                            Message = $"Optimization: {grandChildrenCount} children moved outside of container. Container removed - {child.GetType().Name}."
                        };
                        xamlTextChangedEvent.Info.Add(errorInfo);

                        continue;
                    }
                }
            }
            else if (child is TextBlock textBlock)
            {
                if (string.IsNullOrEmpty(textBlock.Text))
                {
                    canvas.Children.RemoveAt(i);

                    ErrorInfo errorInfo = new()
                    {
                        Message = $"Optimization: UIElement removed - {child.GetType().Name}"
                    };
                    xamlTextChangedEvent.Info.Add(errorInfo);

                    continue;
                }
            }

            i++;
        }

        return canvas;
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