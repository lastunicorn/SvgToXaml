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

using DustInTheWind.SvgToXaml.SvgSerialization;

namespace DustInTheWind.SvgToXaml.SvgModel.Conversion;

internal static class LineExtensions
{
    public static SvgLine ToSvgModel(this Line line)
    {
        if (line == null)
            return null;

        SvgLine svgLine = new();
        svgLine.PopulateFrom(line);

        svgLine.X1 = line.X1;
        svgLine.Y1 = line.Y1;
        svgLine.X2 = line.X2;
        svgLine.Y2 = line.Y2;

        return svgLine;
    }
}