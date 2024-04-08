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
        svgGroup.PopulateFromGroup(xmlG, deserializationContext);

        return svgGroup;
    }

    public static void PopulateFromGroup(this SvgGroup svgGroup, XmlG xmlG, DeserializationContext deserializationContext)
    {
        svgGroup.PopulateFromElement(xmlG);

        if (xmlG.Children != null)
        {
            foreach (object serializationChild in xmlG.Children)
            {
                if (serializationChild is XmlCircle circle)
                {
                    SvgCircle svgCircle = circle.ToSvgModel(deserializationContext);
                    svgGroup.Children.Add(svgCircle);
                }
                else if (serializationChild is XmlEllipse ellipse)
                {
                    SvgEllipse svgEllipse = ellipse.ToSvgModel();
                    svgGroup.Children.Add(svgEllipse);
                }
                else if (serializationChild is XmlPath path)
                {
                    SvgPath svgPath = path.ToSvgModel();
                    svgGroup.Children.Add(svgPath);
                }
                else if (serializationChild is XmlLine line)
                {
                    SvgLine svgLine = line.ToSvgModel();
                    svgGroup.Children.Add(svgLine);
                }
                else if (serializationChild is XmlRect rect)
                {
                    SvgRectangle svgRectangle = rect.ToSvgModel();
                    svgGroup.Children.Add(svgRectangle);
                }
                else if (serializationChild is XmlPolygon polygon)
                {
                    SvgPolygon svgPolygon = polygon.ToSvgModel();
                    svgGroup.Children.Add(svgPolygon);
                }
                else if (serializationChild is XmlPolyline polyline)
                {
                    SvgPolyline svgPolyline = polyline.ToSvgModel();
                    svgGroup.Children.Add(svgPolyline);
                }
                else if (serializationChild is XmlDefs defs)
                {
                    SvgDefinitions svgDefinitions = defs.ToSvgModel(deserializationContext);
                    svgGroup.Children.Add(svgDefinitions);
                }
                else if (serializationChild is XmlG gChild)
                {
                    SvgGroup svgGroupChild = gChild.ToSvgModel(deserializationContext);
                    svgGroup.Children.Add(svgGroupChild);
                }
                else if (serializationChild is XmlUse use)
                {
                    SvgUse svgUseChild = use.ToSvgModel();
                    svgGroup.Children.Add(svgUseChild);
                }
                else if (serializationChild is XmlStyle style)
                {
                    IEnumerable<SvgStyleRuleSet> ruleSets = ParseStyles(style.Value);

                    foreach (SvgStyleRuleSet svgStyleRuleSet in ruleSets)
                        svgGroup.StyleSheet.Add(svgStyleRuleSet);
                }
                else if (serializationChild is XmlText text)
                {
                    SvgText svgText = text.ToSvgModel();
                    svgGroup.Children.Add(svgText);
                }
                else if (serializationChild is XmlLinearGradient linearGradient)
                {
                    SvgLinearGradient svgLinearGradient = linearGradient.ToSvgModel();
                    svgGroup.Children.Add(svgLinearGradient);
                }
                else if (serializationChild is XmlClipPath clipPath)
                {
                    SvgClipPath svgClipPath = clipPath.ToSvgModel(deserializationContext);
                    svgGroup.Children.Add(svgClipPath);
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