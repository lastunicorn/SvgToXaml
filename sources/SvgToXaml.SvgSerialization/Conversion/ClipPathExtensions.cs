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

using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal static class ClipPathExtensions
{
    public static SvgClipPath ToSvgModel(this XmlClipPath xmlClipPath, DeserializationContext deserializationContext)
    {
        if (xmlClipPath == null)
            return null;

        SvgClipPath svgClipPath = new();
        svgClipPath.PopulateFromElement(xmlClipPath);

        if (xmlClipPath.Children != null)
        {
            foreach (object serializationChild in xmlClipPath.Children)
            {
                if (serializationChild is XmlCircle circle)
                {
                    SvgCircle svgCircle = circle.ToSvgModel(deserializationContext);
                    svgClipPath.Children.Add(svgCircle);
                }
                else if (serializationChild is XmlEllipse ellipse)
                {
                    SvgEllipse svgEllipse = ellipse.ToSvgModel(deserializationContext);
                    svgClipPath.Children.Add(svgEllipse);
                }
                else if (serializationChild is XmlPath path)
                {
                    SvgPath svgPath = path.ToSvgModel();
                    svgClipPath.Children.Add(svgPath);
                }
                else if (serializationChild is XmlLine line)
                {
                    SvgLine svgLine = line.ToSvgModel();
                    svgClipPath.Children.Add(svgLine);
                }
                else if (serializationChild is XmlRect rect)
                {
                    SvgRectangle svgRectangle = rect.ToSvgModel();
                    svgClipPath.Children.Add(svgRectangle);
                }
                else if (serializationChild is XmlPolygon polygon)
                {
                    SvgPolygon svgPolygon = polygon.ToSvgModel();
                    svgClipPath.Children.Add(svgPolygon);
                }
                else if (serializationChild is XmlPolyline polyline)
                {
                    SvgPolyline svgPolyline = polyline.ToSvgModel();
                    svgClipPath.Children.Add(svgPolyline);
                }
                else if (serializationChild is XmlUse use)
                {
                    SvgUse svgUseChild = use.ToSvgModel();
                    svgClipPath.Children.Add(svgUseChild);
                }
                else if (serializationChild is XmlText text)
                {
                    SvgText svgText = text.ToSvgModel();
                    svgClipPath.Children.Add(svgText);
                }
            }
        }

        return svgClipPath;
    }
}