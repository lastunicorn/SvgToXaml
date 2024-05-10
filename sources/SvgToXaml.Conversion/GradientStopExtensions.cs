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

using System.Windows.Media;
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion;

internal static class GradientStopExtensions
{
    public static GradientStop Transform(this SvgStop svgStop)
    {
        SvgColor stopColor = svgStop.ComputeStopColor();

        if (stopColor.Alpha == null)
        {
            SvgOpacity? stopOpacity = svgStop.ComputeStopOpacity();

            if (stopOpacity.HasValue)
                stopColor = stopColor.SetAlpha(stopOpacity.Value);
        }

        Color color = stopColor.ToColor();
        return new GradientStop(color, svgStop.Offset);
    }
}