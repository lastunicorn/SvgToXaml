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

using DustInTheWind.SvgToXaml.Svg.Serialization;

namespace DustInTheWind.SvgToXaml.Svg;

public class SvgLinearGradient : SvgElement
{
    public SvgLength? X1 { get; set; }

    public SvgLength? Y1 { get; set; }

    public SvgLength? X2 { get; set; }

    public SvgLength? Y2 { get; set; }

    public SvgGradientUnits? GradientUnits { get; set; }

    public List<SvgStop> Stops { get; } = new();

    public SvgTransformList GradientTransforms { get; } = new();

    public HypertextReference? Href { get; set; }

    public SvgLinearGradient(LinearGradient linearGradient)
        : base(linearGradient)
    {
        if (linearGradient.X1Specified)
            X1 = linearGradient.X1;

        if (linearGradient.Y1Specified)
            Y1 = linearGradient.Y1;

        if (linearGradient.X2Specified)
            X2 = linearGradient.X2;

        if (linearGradient.Y2Specified)
            Y2 = linearGradient.Y2;

        if (linearGradient.GradientUnitsSpecified)
        {
            GradientUnits = linearGradient.GradientUnits switch
            {
                Serialization.GradientUnits.ObjectBoundingBox => SvgGradientUnits.ObjectBoundingBox,
                Serialization.GradientUnits.UserSpaceOnUse => SvgGradientUnits.UserSpaceOnUse,
                _ => throw new Exception("Invalid gradient unit value")
            };
        }

        if (linearGradient.Stops != null)
        {
            Stops = linearGradient.Stops
                .Select(x => new SvgStop(x))
                .ToList();
        }

        if (linearGradient.GradientTransform != null)
            GradientTransforms.ParseAndAdd(linearGradient.GradientTransform);

        if (linearGradient.Href != null)
            Href = linearGradient.Href;
    }

    public SvgLength? ComputeX1()
    {
        if (X1 != null)
            return X1;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgLinearGradient templateLinearGradient)
                return templateLinearGradient.ComputeX1();
        }

        return null;
    }

    public SvgLength? ComputeX2()
    {
        if (X2 != null)
            return X2;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgLinearGradient templateLinearGradient)
                return templateLinearGradient.ComputeX2();
        }

        return null;
    }

    public SvgLength? ComputeY1()
    {
        if (Y1 != null)
            return Y1;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgLinearGradient templateLinearGradient)
                return templateLinearGradient.ComputeY1();
        }

        return null;
    }

    public SvgLength? ComputeY2()
    {
        if (Y2 != null)
            return Y2;

        if (Href != null)
        {
            Svg svg = GetParentSvg();
            SvgElement svgElement = svg?.FindChild(Href.Value.Id);

            if (svgElement is SvgLinearGradient templateLinearGradient)
                return templateLinearGradient.ComputeY2();
        }

        return null;
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

        return null;
    }
}