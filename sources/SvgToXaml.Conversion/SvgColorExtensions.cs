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
using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgToXaml.Conversion;

internal static class SvgColorExtensions
{
    public static Color ToColor(this SvgColor svgColor)
    {
        return svgColor.Alpha == null
            ? Color.FromRgb(svgColor.Red, svgColor.Green, svgColor.Blue)
            : Color.FromArgb(svgColor.Alpha.Value, svgColor.Red, svgColor.Green, svgColor.Blue);
    }

    public static Color ToColor(this SvgColor svgColor, double opacity)
    {
        if (opacity < 0)
            opacity = 0;

        if (opacity > 1)
            opacity = 1;

        byte alpha = (byte)(opacity * 255);

        return Color.FromArgb(alpha, svgColor.Red, svgColor.Green, svgColor.Blue);
    }
}