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

using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal static class PolylineExtensions
{
    public static SvgPolyline ToSvgModel(this XmlPolyline xmlPolyline)
    {
        if (xmlPolyline == null)
            return null;

        SvgPolyline svgPolygon = new();
        svgPolygon.PopulateFromElement(xmlPolyline);

        if (xmlPolyline.Points != null)
        {
            IEnumerable<SvgPoint> points = SvgPoint.ParseMany(xmlPolyline.Points);

            foreach (SvgPoint point in points)
                svgPolygon.Points.Add(point);
        }

        return svgPolygon;
    }
}