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

using System.Windows;
using System.Windows.Media;
using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgToXaml.Conversion;

internal static class LinearGradientExtension
{
    public static LinearGradientBrush Transform(this SvgLinearGradient svgLinearGradient)
    {
        IEnumerable<GradientStop> gradientStops = svgLinearGradient.ComputeStops()
            .Select(x => x.Transform())
            .ToList();

        GradientStopCollection gradientStopCollection = new(gradientStops);
        LinearGradientBrush linearGradientBrush = new(gradientStopCollection);

        double x1 = svgLinearGradient.ComputeX1() ?? SvgLength.Zero;
        double x2 = svgLinearGradient.ComputeX2() ?? SvgLength.Zero;
        double y1 = svgLinearGradient.ComputeY1() ?? new SvgLength(1);
        double y2 = svgLinearGradient.ComputeY2() ?? new SvgLength(1);

        linearGradientBrush.StartPoint = new Point(x1, y1);
        linearGradientBrush.EndPoint = new Point(x2, y2);

        if (svgLinearGradient.GradientUnits != null)
        {
            linearGradientBrush.MappingMode = svgLinearGradient.GradientUnits switch
            {
                SvgGradientUnits.ObjectBoundingBox => BrushMappingMode.RelativeToBoundingBox,
                SvgGradientUnits.UserSpaceOnUse => BrushMappingMode.Absolute,
                _ => throw new Exception("Unknown gradient units.")
            };
        }

        if (svgLinearGradient.GradientTransforms.Count > 0)
            linearGradientBrush.Transform = svgLinearGradient.GradientTransforms.ToXaml(linearGradientBrush.Transform);

        if (svgLinearGradient.SpreadMethod != null)
        {
            linearGradientBrush.SpreadMethod = svgLinearGradient.SpreadMethod switch
            {
                SvgSpreadMethod.Pad => GradientSpreadMethod.Pad,
                SvgSpreadMethod.Reflect => GradientSpreadMethod.Reflect,
                SvgSpreadMethod.Repeat => GradientSpreadMethod.Repeat,
                _ => throw new Exception("Unknown spread method value.")
            };
        }

        return linearGradientBrush;
    }
}