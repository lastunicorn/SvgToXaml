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

internal static class LineExtensions
{
    public static SvgLine ToSvgModel(this XmlLine xmlLine)
    {
        if (xmlLine == null)
            return null;

        SvgLine svgLine = new();
        svgLine.PopulateFromElement(xmlLine);

        svgLine.X1 = xmlLine.X1;
        svgLine.Y1 = xmlLine.Y1;
        svgLine.X2 = xmlLine.X2;
        svgLine.Y2 = xmlLine.Y2;

        return svgLine;
    }
}