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

namespace DustInTheWind.SvgDotnet;

public class SvgRadialGradient : SvgElement
{
    private Length? radius;

    public SvgGradientUnits? GradientUnits { get; set; }

    public Length? CenterX { get; set; }

    public Length? CenterY { get; set; }

    public Length? Radius
    {
        get => radius;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Radius cannot be a negative value. It must be a positive, finite number.");

            if (double.IsNaN(value))
                throw new ArgumentOutOfRangeException(nameof(value), "Radius must be a positive, finite number.");

            radius = value;
        }
    }

    public Length? Fx { get; set; }

    public Length? Fy { get; set; }

    public List<SvgStop> Stops { get; } = new();

    public TransformCollection GradientTransforms { get; } = new();

    public HypertextReference? Href { get; set; }

    public SvgSpreadMethod? SpreadMethod { get; set; }

    public Length? ComputeCenterX()
    {
        if (CenterX != null)
            return CenterX;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgRadialGradient templateRadialGradient)
                return templateRadialGradient.ComputeCenterX();
        }

        return null;
    }

    public Length? ComputeCenterY()
    {
        if (CenterY != null)
            return CenterY;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgRadialGradient templateRadialGradient)
                return templateRadialGradient.ComputeCenterY();
        }

        return null;
    }

    public Length? ComputeRadius()
    {
        if (Radius != null)
            return Radius;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgRadialGradient templateRadialGradient)
                return templateRadialGradient.ComputeRadius();
        }

        return null;
    }

    public Length? ComputeFx()
    {
        return Fx;
    }

    public Length? ComputeFy()
    {
        return Fy;
    }

    public List<SvgStop> ComputeStops()
    {
        if (Stops.Count > 0)
            return Stops;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgLinearGradient templateLinearGradient)
                return templateLinearGradient.ComputeStops();
        }

        return new List<SvgStop>();
    }
}