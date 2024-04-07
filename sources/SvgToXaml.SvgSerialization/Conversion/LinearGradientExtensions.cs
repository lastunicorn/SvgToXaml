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

internal static class LinearGradientExtensions
{
    public static SvgLinearGradient ToSvgModel(this XmlLinearGradient xmlLinearGradient)
    {
        if (xmlLinearGradient == null)
            return null;

        SvgLinearGradient svgLinearGradient = new();
        svgLinearGradient.PopulateFrom(xmlLinearGradient);

        if (xmlLinearGradient.X1Specified)
            svgLinearGradient.X1 = xmlLinearGradient.X1;

        if (xmlLinearGradient.Y1Specified)
            svgLinearGradient.Y1 = xmlLinearGradient.Y1;

        if (xmlLinearGradient.X2Specified)
            svgLinearGradient.X2 = xmlLinearGradient.X2;

        if (xmlLinearGradient.Y2Specified)
            svgLinearGradient.Y2 = xmlLinearGradient.Y2;

        if (xmlLinearGradient.GradientUnitsSpecified)
        {
            svgLinearGradient.GradientUnits = xmlLinearGradient.GradientUnits switch
            {
                XmlGradientUnits.ObjectBoundingBox => SvgGradientUnits.ObjectBoundingBox,
                XmlGradientUnits.UserSpaceOnUse => SvgGradientUnits.UserSpaceOnUse,
                _ => throw new Exception("Invalid gradient unit value")
            };
        }

        if (xmlLinearGradient.Stops != null)
        {
            IEnumerable<SvgStop> svgStops = xmlLinearGradient.Stops
                .Select(x => x.ToSvgModel());

            foreach (SvgStop svgStop in svgStops)
                svgLinearGradient.Stops.Add(svgStop);
        }

        if (xmlLinearGradient.GradientTransform != null)
            svgLinearGradient.GradientTransforms.ParseAndAdd(xmlLinearGradient.GradientTransform);

        if (xmlLinearGradient.Href != null)
            svgLinearGradient.Href = xmlLinearGradient.Href;

        return svgLinearGradient;
    }
}