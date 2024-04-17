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

internal static class GroupExtensions
{
    private static readonly Regex Regex = new(@"\.(\w+)\s*{\s*(.*?)\s*}", RegexOptions.Multiline);

    public static SvgGroup ToSvgModel(this XmlG xmlG, DeserializationContext deserializationContext)
    {
        if (xmlG == null)
            return null;

        SvgGroup svgGroup = new();
        svgGroup.PopulateFromContainer(xmlG, deserializationContext);

        return svgGroup;
    }

    public static void PopulateFromContainer(this SvgContainer svgContainer, XmlContainer xmlContainer, DeserializationContext deserializationContext)
    {
        svgContainer.PopulateFromElement(xmlContainer);

        if (xmlContainer.Children != null)
        {
            foreach (object serializationChild in xmlContainer.Children)
            {
                if (serializationChild is XmlCircle circle)
                {
                    SvgCircle svgCircle = circle.ToSvgModel(deserializationContext);
                    svgContainer.Children.Add(svgCircle);
                }
                else if (serializationChild is XmlEllipse ellipse)
                {
                    SvgEllipse svgEllipse = ellipse.ToSvgModel(deserializationContext);
                    svgContainer.Children.Add(svgEllipse);
                }
                else if (serializationChild is XmlPath path)
                {
                    SvgPath svgPath = path.ToSvgModel();
                    svgContainer.Children.Add(svgPath);
                }
                else if (serializationChild is XmlLine line)
                {
                    SvgLine svgLine = line.ToSvgModel();
                    svgContainer.Children.Add(svgLine);
                }
                else if (serializationChild is XmlRect rect)
                {
                    SvgRectangle svgRectangle = rect.ToSvgModel();
                    svgContainer.Children.Add(svgRectangle);
                }
                else if (serializationChild is XmlPolygon polygon)
                {
                    SvgPolygon svgPolygon = polygon.ToSvgModel();
                    svgContainer.Children.Add(svgPolygon);
                }
                else if (serializationChild is XmlPolyline polyline)
                {
                    SvgPolyline svgPolyline = polyline.ToSvgModel();
                    svgContainer.Children.Add(svgPolyline);
                }
                else if (serializationChild is XmlDefs defs)
                {
                    SvgDefinitions svgDefinitions = defs.ToSvgModel(deserializationContext);
                    svgContainer.Children.Add(svgDefinitions);
                }
                else if (serializationChild is XmlG gChild)
                {
                    SvgGroup svgGroupChild = gChild.ToSvgModel(deserializationContext);
                    svgContainer.Children.Add(svgGroupChild);
                }
                else if (serializationChild is XmlUse use)
                {
                    SvgUse svgUseChild = use.ToSvgModel();
                    svgContainer.Children.Add(svgUseChild);
                }
                else if (serializationChild is XmlStyle style)
                {
                    IEnumerable<SvgStyleRuleSet> ruleSets = ParseStyles(style.Value);

                    foreach (SvgStyleRuleSet svgStyleRuleSet in ruleSets)
                        svgContainer.StyleSheet.Add(svgStyleRuleSet);
                }
                else if (serializationChild is XmlText text)
                {
                    SvgText svgText = text.ToSvgModel();
                    svgContainer.Children.Add(svgText);
                }
                else if (serializationChild is XmlLinearGradient linearGradient)
                {
                    SvgLinearGradient svgLinearGradient = linearGradient.ToSvgModel();
                    svgContainer.Children.Add(svgLinearGradient);
                }
                else if (serializationChild is XmlRadialGradient radialGradient)
                {
                    SvgRadialGradient svgRadialGradient = radialGradient.ToSvgModel();
                    svgContainer.Children.Add(svgRadialGradient);
                }
                else if (serializationChild is XmlClipPath clipPath)
                {
                    SvgClipPath svgClipPath = clipPath.ToSvgModel(deserializationContext);
                    svgContainer.Children.Add(svgClipPath);
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