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
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion;

internal static class RadialGradientExtension
{
    public static RadialGradientBrush Transform(this SvgRadialGradient svgRadialGradient)
    {
        IEnumerable<GradientStop> gradientStops = svgRadialGradient.ComputeStops()
            .Select(x => x.Transform());

        GradientStopCollection gradientStopCollection = new(gradientStops);
        RadialGradientBrush radialGradientBrush = new(gradientStopCollection);

        double cx = svgRadialGradient.ComputeCenterX() ?? Length.Zero;
        double cy = svgRadialGradient.ComputeCenterY() ?? Length.Zero;
        double r = svgRadialGradient.ComputeRadius() ?? Length.Zero;
        double fx = svgRadialGradient.ComputeFx() ?? cx;
        double fy = svgRadialGradient.ComputeFy() ?? cy;

        radialGradientBrush.Center = new Point(cx, cy);
        radialGradientBrush.GradientOrigin = new Point(fx, fy);
        radialGradientBrush.RadiusX = r;
        radialGradientBrush.RadiusY = r;

        if (svgRadialGradient.GradientUnits != null)
        {
            radialGradientBrush.MappingMode = svgRadialGradient.GradientUnits switch
            {
                SvgGradientUnits.ObjectBoundingBox => BrushMappingMode.RelativeToBoundingBox,
                SvgGradientUnits.UserSpaceOnUse => BrushMappingMode.Absolute,
                _ => throw new Exception("Unknown gradient units.")
            };
        }

        if (svgRadialGradient.GradientTransforms.Count > 0)
            radialGradientBrush.Transform = svgRadialGradient.GradientTransforms.ToXaml(radialGradientBrush.Transform);

        if (svgRadialGradient.SpreadMethod != null)
        {
            radialGradientBrush.SpreadMethod = svgRadialGradient.SpreadMethod switch
            {
                SvgSpreadMethod.Pad => GradientSpreadMethod.Pad,
                SvgSpreadMethod.Reflect => GradientSpreadMethod.Reflect,
                SvgSpreadMethod.Repeat => GradientSpreadMethod.Repeat,
                _ => throw new Exception("Unknown spread method value.")
            };
        }

        return radialGradientBrush;
    }
}