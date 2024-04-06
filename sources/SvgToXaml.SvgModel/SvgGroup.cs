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

using DustInTheWind.SvgToXaml.SvgSerialization;
using Path = DustInTheWind.SvgToXaml.SvgSerialization.Path;

namespace DustInTheWind.SvgToXaml.SvgModel;

public class SvgGroup : SvgContainer
{
    public SvgStyleSheet StyleSheet { get; }

    public SvgGroup()
    {
    }

    internal SvgGroup(G g)
        : base(g)
    {
        if (g == null) throw new ArgumentNullException(nameof(g));

        if (g.Children != null)
        {
            foreach (object serializationChild in g.Children)
            {
                if (serializationChild is Circle serializationCircle)
                {
                    SvgCircle circle = new(serializationCircle);
                    Children.Add(circle);
                }
                else if (serializationChild is Ellipse serializationEllipse)
                {
                    SvgEllipse ellipse = new(serializationEllipse);
                    Children.Add(ellipse);
                }
                else if (serializationChild is Path serializationPath)
                {
                    SvgPath path = new(serializationPath);
                    Children.Add(path);
                }
                else if (serializationChild is Line serializationLine)
                {
                    SvgLine line = new(serializationLine);
                    Children.Add(line);
                }
                else if (serializationChild is Rect serializationRect)
                {
                    SvgRectangle rectangle = new(serializationRect);
                    Children.Add(rectangle);
                }
                else if (serializationChild is Polygon serializationPolygon)
                {
                    SvgPolygon polygon = new(serializationPolygon);
                    Children.Add(polygon);
                }
                else if (serializationChild is Polyline serializationPolyline)
                {
                    SvgPolyline polyline = new(serializationPolyline);
                    Children.Add(polyline);
                }
                else if (serializationChild is Defs serializationDefs)
                {
                    SvgDefinitions svgDefinitions = new(serializationDefs);
                    Children.Add(svgDefinitions);
                }
                else if (serializationChild is G serializationGChild)
                {
                    SvgGroup svgGroupChild = new(serializationGChild);
                    Children.Add(svgGroupChild);
                }
                else if (serializationChild is Use serializationUseChild)
                {
                    SvgUse svgUseChild = new(serializationUseChild);
                    Children.Add(svgUseChild);
                }
                else if (serializationChild is Style style)
                {
                    StyleSheet = style.Value;
                }
                else if (serializationChild is Text serializationText)
                {
                    SvgText svgText = new(serializationText);
                    Children.Add(svgText);
                }
                else if (serializationChild is LinearGradient linearGradient)
                {
                    SvgLinearGradient svgLinearGradient = new(linearGradient);
                    Children.Add(svgLinearGradient);
                }
                else if (serializationChild is ClipPath clipPath)
                {
                    SvgClipPath svgClipPath = new(clipPath);
                    Children.Add(svgClipPath);
                }
            }
        }
    }
}