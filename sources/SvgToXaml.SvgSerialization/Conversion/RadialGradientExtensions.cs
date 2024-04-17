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

internal static class RadialGradientExtensions
{
    public static SvgRadialGradient ToSvgModel(this XmlRadialGradient xmlRadialGradient, DeserializationContext deserializationContext)
    {
        if (xmlRadialGradient == null)
            return null;

        return deserializationContext.Run("radialGradient", () =>
        {
            SvgRadialGradient svgRadialGradient = new();
            svgRadialGradient.PopulateFromElement(xmlRadialGradient);

            SvgLength? radius = xmlRadialGradient.R;

            if (radius != null)
            {
                if (radius.Value < 0)
                {
                    svgRadialGradient.Radius = 0;

                    deserializationContext.Path.SetAttributeOnLast("r");

                    NegativeValueIssue issue = new(deserializationContext.Path.ToString());
                    deserializationContext.Warnings.Add(issue);
                }
                else
                {
                    svgRadialGradient.Radius = radius.Value;
                }
            }

            svgRadialGradient.CenterX = xmlRadialGradient.Cx;
            svgRadialGradient.CenterY = xmlRadialGradient.Cy;

            if (xmlRadialGradient.GradientUnitsSpecified)
            {
                svgRadialGradient.GradientUnits = xmlRadialGradient.GradientUnits switch
                {
                    XmlGradientUnits.ObjectBoundingBox => SvgGradientUnits.ObjectBoundingBox,
                    XmlGradientUnits.UserSpaceOnUse => SvgGradientUnits.UserSpaceOnUse,
                    _ => throw new Exception("Invalid gradient unit value.")
                };
            }

            if (xmlRadialGradient.Stops != null)
            {
                IEnumerable<SvgStop> svgStops = xmlRadialGradient.Stops
                    .Select(x => x.ToSvgModel());

                foreach (SvgStop svgStop in svgStops)
                    svgRadialGradient.Stops.Add(svgStop);
            }

            if (xmlRadialGradient.GradientTransform != null)
                svgRadialGradient.GradientTransforms.ParseAndAdd(xmlRadialGradient.GradientTransform);

            if (xmlRadialGradient.Href != null)
                svgRadialGradient.Href = xmlRadialGradient.Href;

            return svgRadialGradient;
        });
    }
}