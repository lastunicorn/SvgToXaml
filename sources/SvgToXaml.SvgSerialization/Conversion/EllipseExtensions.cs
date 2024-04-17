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

internal static class EllipseExtensions
{
    public static SvgEllipse ToSvgModel(this XmlEllipse xmlEllipse, DeserializationContext deserializationContext)
    {
        if (xmlEllipse == null)
            return null;

        return deserializationContext.Run("ellipse", () =>
        {
            SvgEllipse svgEllipse = new();
            svgEllipse.PopulateFromElement(xmlEllipse);

            if (xmlEllipse.Rx < 0)
            {
                svgEllipse.RadiusX = 0;

                deserializationContext.Path.SetAttributeOnLast("rx");

                NegativeValueIssue issue = new(deserializationContext.Path.ToString());
                deserializationContext.Warnings.Add(issue);
            }
            else
            {
                svgEllipse.RadiusX = xmlEllipse.Rx;
            }

            if (xmlEllipse.Ry < 0)
            {
                svgEllipse.RadiusY = 0;

                deserializationContext.Path.SetAttributeOnLast("ry");

                NegativeValueIssue issue = new(deserializationContext.Path.ToString());
                deserializationContext.Warnings.Add(issue);
            }
            else
            {
                svgEllipse.RadiusY = xmlEllipse.Ry;
            }

            svgEllipse.CenterX = xmlEllipse.Cx;
            svgEllipse.CenterY = xmlEllipse.Cy;

            return svgEllipse;
        });
    }
}