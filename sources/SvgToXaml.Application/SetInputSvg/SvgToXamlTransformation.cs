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

namespace DustInTheWind.SvgToXaml.Application.SetInputSvg;

internal class SvgToXamlTransformation
{
    public string Svg { get; set; }

    public bool PerformOptimizations { get; set; }

    public List<string> IgnoredNamespaces { get; set; }

    public string Xaml { get; private set; }

    public ProcessingIssueCollection Issues { get; } = new();

    public void Execute()
    {
        try
        {
            if (!string.IsNullOrEmpty(Svg))
            {
                Svg svg = Deserialize(Svg);

                if (svg == null)
                    return;

                Canvas canvas = Convert(svg);

                if (canvas == null)
                    return;

                if (PerformOptimizations)
                    canvas = Optimize(canvas);

                string xaml = Serialize(canvas);

                if (string.IsNullOrEmpty(xaml))
                    return;

                xaml = XmlAlter(xaml);

                Xaml = xaml;
            }
        }
        catch (Exception ex)
        {
            ProcessingIssue processingIssue = new()
            {
                Category = "General",
                Level = ProcessingIssueLevel.Error,
                Message = ex.ToString()
            };
            Issues.Add(processingIssue);
        }
    }

    private Svg Deserialize(string svgText)
    {
        SvgSerializer serializer = new();

        if (IgnoredNamespaces is { Count: > 0 })
            serializer.Options.IgnoredNamespaces.AddRange(IgnoredNamespaces);

        DeserializationResult deserializationResult = serializer.Deserialize(svgText);

        IEnumerable<ProcessingIssue> issues = deserializationResult.Issues
            .Select(x => new ProcessingIssue(x));

        Issues.AddRange(issues);

        return deserializationResult.Svg;
    }

    private Canvas Convert(Svg svg)
    {
        SvgToXamlConvertor svgToXamlConvertor = new();
        ConversionResult conversionResult = svgToXamlConvertor.ConvertToXaml(svg);

        IEnumerable<ProcessingIssue> issues = conversionResult.Issues
            .Select(x => new ProcessingIssue(x));

        Issues.AddRange(issues);

        return conversionResult.Canvas;
    }

    private Canvas Optimize(Canvas canvas)
    {
        CanvasOptimization canvasOptimization = new(canvas);
        canvasOptimization.Execute();

        if (canvasOptimization.Issues.Count > 0)
            Issues.AddRange(canvasOptimization.Issues);

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