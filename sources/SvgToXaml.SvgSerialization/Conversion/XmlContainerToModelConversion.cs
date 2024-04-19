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

using System.Text.RegularExpressions;
using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal abstract class XmlContainerToModelConversion<TXml, TSvg> : XmlElementToModelConversion<TXml, TSvg>
    where TXml : XmlContainer
    where TSvg : SvgContainer
{
    private static readonly Regex Regex = new(@"\.(\w+)\s*{\s*(.*?)\s*}", RegexOptions.Multiline);

    protected XmlContainerToModelConversion(TXml xmlContainer, DeserializationContext deserializationContext)
        : base(xmlContainer, deserializationContext)
    {
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        if (XmlElement.Children != null)
        {
            foreach (object serializationChild in XmlElement.Children)
            {
                if (serializationChild is XmlCircle circle)
                {
                    XmlCircleToModelConversion conversion = new(circle, DeserializationContext);
                    SvgCircle svgCircle = conversion.Execute();
                    SvgElement.Children.Add(svgCircle);
                }
                else if (serializationChild is XmlEllipse ellipse)
                {
                    XmlEllipseToModelConversion conversion = new(ellipse, DeserializationContext);
                    SvgEllipse svgEllipse = conversion.Execute();
                    SvgElement.Children.Add(svgEllipse);
                }
                else if (serializationChild is XmlPath path)
                {
                    XmlPathToModelConversion conversion = new(path, DeserializationContext);
                    SvgPath svgPath = conversion.Execute();
                    SvgElement.Children.Add(svgPath);
                }
                else if (serializationChild is XmlLine line)
                {
                    XmlLineToModelConversion conversion = new(line, DeserializationContext);
                    SvgLine svgLine = conversion.Execute();
                    SvgElement.Children.Add(svgLine);
                }
                else if (serializationChild is XmlRect rect)
                {
                    XmlRectToModelConversion conversion = new(rect, DeserializationContext);
                    SvgRectangle svgRectangle = conversion.Execute();
                    SvgElement.Children.Add(svgRectangle);
                }
                else if (serializationChild is XmlPolygon polygon)
                {
                    XmlPolygonToModelConversion conversion = new(polygon, DeserializationContext);
                    SvgPolygon svgPolygon = conversion.Execute();
                    SvgElement.Children.Add(svgPolygon);
                }
                else if (serializationChild is XmlPolyline polyline)
                {
                    XmlPolylineToModelConversion conversion = new(polyline, DeserializationContext);
                    SvgPolyline svgPolyline = conversion.Execute();
                    SvgElement.Children.Add(svgPolyline);
                }
                else if (serializationChild is XmlDefs defs)
                {
                    XmlDefsToModelConversion conversion = new(defs, DeserializationContext);
                    SvgDefinitions svgDefinitions = conversion.Execute();
                    SvgElement.Children.Add(svgDefinitions);
                }
                else if (serializationChild is XmlG gChild)
                {
                    XmlGroupToModelConversion conversion = new(gChild, DeserializationContext);
                    SvgGroup svgGroupChild = conversion.Execute();
                    SvgElement.Children.Add(svgGroupChild);
                }
                else if (serializationChild is XmlUse use)
                {
                    XmlUseToModelConversion conversion = new(use, DeserializationContext);
                    SvgUse svgUseChild = conversion.Execute();
                    SvgElement.Children.Add(svgUseChild);
                }
                else if (serializationChild is XmlStyle style)
                {
                    IEnumerable<SvgStyleRuleSet> ruleSets = ParseStyles(style.Value);

                    foreach (SvgStyleRuleSet svgStyleRuleSet in ruleSets)
                        SvgElement.StyleSheet.Add(svgStyleRuleSet);
                }
                else if (serializationChild is XmlText text)
                {
                    XmlTextToModelConversion conversion = new(text, DeserializationContext);
                    SvgText svgText = conversion.Execute();
                    SvgElement.Children.Add(svgText);
                }
                else if (serializationChild is XmlLinearGradient linearGradient)
                {
                    XmlLinearGradientToModelConversion conversion = new(linearGradient, DeserializationContext);
                    SvgLinearGradient svgLinearGradient = conversion.Execute();
                    SvgElement.Children.Add(svgLinearGradient);
                }
                else if (serializationChild is XmlRadialGradient radialGradient)
                {
                    XmlRadialGradientToModelConversion conversion = new(radialGradient, DeserializationContext);
                    SvgRadialGradient svgRadialGradient = conversion.Execute();
                    SvgElement.Children.Add(svgRadialGradient);
                }
                else if (serializationChild is XmlClipPath clipPath)
                {
                    XmlClipPathToModelConversion conversion = new(clipPath, DeserializationContext);
                    SvgClipPath svgClipPath = conversion.Execute();
                    SvgElement.Children.Add(svgClipPath);
                }
                else if (serializationChild is XmlSvg childSvg)
                {
                    XmlSvgToModelConversion conversion = new(childSvg, DeserializationContext);
                    Svg svg = conversion.Execute();
                    SvgElement.Children.Add(svg);
                }
            }
        }
    }

    private static IEnumerable<SvgStyleRuleSet> ParseStyles(string text)
    {
        if (text == null)
            return Enumerable.Empty<SvgStyleRuleSet>();

        MatchCollection matches = Regex.Matches(text);

        return matches
            .Select(x => new SvgStyleRuleSet
            {
                Selector = x.Groups[1].Value,
                Declarations = x.Groups[2].Value
            });
    }
}