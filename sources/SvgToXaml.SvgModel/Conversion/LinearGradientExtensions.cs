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

internal static class LinearGradientExtensions
{
    public static SvgLinearGradient ToSvgModel(this LinearGradient linearGradient)
    {
        if (linearGradient == null)
            return null;

        SvgLinearGradient svgLinearGradient = new();
        svgLinearGradient.PopulateFrom(linearGradient);

        if (linearGradient.X1Specified)
            svgLinearGradient.X1 = linearGradient.X1;

        if (linearGradient.Y1Specified)
            svgLinearGradient.Y1 = linearGradient.Y1;

        if (linearGradient.X2Specified)
            svgLinearGradient.X2 = linearGradient.X2;

        if (linearGradient.Y2Specified)
            svgLinearGradient.Y2 = linearGradient.Y2;

        if (linearGradient.GradientUnitsSpecified)
        {
            svgLinearGradient.GradientUnits = linearGradient.GradientUnits switch
            {
                SvgSerialization.GradientUnits.ObjectBoundingBox => SvgGradientUnits.ObjectBoundingBox,
                SvgSerialization.GradientUnits.UserSpaceOnUse => SvgGradientUnits.UserSpaceOnUse,
                _ => throw new Exception("Invalid gradient unit value")
            };
        }

        if (linearGradient.Stops != null)
        {
            IEnumerable<SvgStop> svgStops = linearGradient.Stops
                .Select(x => x.ToSvgModel());

            foreach (SvgStop svgStop in svgStops)
                svgLinearGradient.Stops.Add(svgStop);
        }

        if (linearGradient.GradientTransform != null)
            svgLinearGradient.GradientTransforms.ParseAndAdd(linearGradient.GradientTransform);

        if (linearGradient.Href != null)
            svgLinearGradient.Href = linearGradient.Href;

        return svgLinearGradient;
    }
}