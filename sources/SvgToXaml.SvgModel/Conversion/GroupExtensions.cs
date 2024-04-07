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
using DustInTheWind.SvgToXaml.SvgSerialization;
using Path = DustInTheWind.SvgToXaml.SvgSerialization.Path;

namespace DustInTheWind.SvgToXaml.SvgModel.Conversion;

internal static class GroupExtensions
{
    private static readonly Regex Regex = new(@"\.(\w+)\s*{\s*(.*?)\s*}", RegexOptions.Multiline);

    public static SvgGroup ToSvgModel(this G g)
    {
        if (g == null)
            return null;

        SvgGroup svgGroup = new();
        svgGroup.PopulateFrom(g);

        return svgGroup;
    }

    public static void PopulateFrom(this SvgGroup svgGroup, G g)
    {
        if (g.Children != null)
        {
            foreach (object serializationChild in g.Children)
            {
                if (serializationChild is Circle circle)
                {
                    SvgCircle svgCircle = circle.ToSvgModel();
                    svgGroup.Children.Add(svgCircle);
                }
                else if (serializationChild is Ellipse ellipse)
                {
                    SvgEllipse svgEllipse = ellipse.ToSvgModel();
                    svgGroup.Children.Add(svgEllipse);
                }
                else if (serializationChild is Path path)
                {
                    SvgPath svgPath = path.ToSvgModel();
                    svgGroup.Children.Add(svgPath);
                }
                else if (serializationChild is Line line)
                {
                    SvgLine svgLine = line.ToSvgModel();
                    svgGroup.Children.Add(svgLine);
                }
                else if (serializationChild is Rect rect)
                {
                    SvgRectangle svgRectangle = rect.ToSvgModel();
                    svgGroup.Children.Add(svgRectangle);
                }
                else if (serializationChild is Polygon polygon)
                {
                    SvgPolygon svgPolygon = polygon.ToSvgModel();
                    svgGroup.Children.Add(svgPolygon);
                }
                else if (serializationChild is Polyline polyline)
                {
                    SvgPolyline svgPolyline = polyline.ToSvgModel();
                    svgGroup.Children.Add(svgPolyline);
                }
                else if (serializationChild is Defs defs)
                {
                    SvgDefinitions svgDefinitions = defs.ToSvgModel();
                    svgGroup.Children.Add(svgDefinitions);
                }
                else if (serializationChild is G gChild)
                {
                    SvgGroup svgGroupChild = gChild.ToSvgModel();
                    svgGroup.Children.Add(svgGroupChild);
                }
                else if (serializationChild is Use use)
                {
                    SvgUse svgUseChild = use.ToSvgModel();
                    svgGroup.Children.Add(svgUseChild);
                }
                else if (serializationChild is Style style)
                {
                    IEnumerable<SvgStyleRuleSet> ruleSets = ParseStyles(style.Value);

                    foreach (SvgStyleRuleSet svgStyleRuleSet in ruleSets)
                        svgGroup.StyleSheet.Add(svgStyleRuleSet);
                }
                else if (serializationChild is Text text)
                {
                    SvgText svgText = text.ToSvgModel();
                    svgGroup.Children.Add(svgText);
                }
                else if (serializationChild is LinearGradient linearGradient)
                {
                    SvgLinearGradient svgLinearGradient = linearGradient.ToSvgModel();
                    svgGroup.Children.Add(svgLinearGradient);
                }
                else if (serializationChild is ClipPath clipPath)
                {
                    SvgClipPath svgClipPath = clipPath.ToSvgModel();
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