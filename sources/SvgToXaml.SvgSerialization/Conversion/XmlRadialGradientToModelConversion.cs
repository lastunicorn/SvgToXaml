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

internal class XmlRadialGradientToModelConversion : XmlElementToModelConversion<XmlRadialGradient, SvgRadialGradient>
{
    protected override string ElementName => "radialGradient";

    public XmlRadialGradientToModelConversion(XmlRadialGradient xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgRadialGradient CreateSvgElement()
    {
        return new SvgRadialGradient();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertRadius();
        ConvertCenter();
        ConvertGradientUnits();
        ConvertStops();
        ConvertGradientTransform();
        ConvertHref();
    }

    private void ConvertRadius()
    {
        SvgLength? radius = XmlElement.R;

        if (radius != null)
        {
            if (radius.Value < 0)
            {
                SvgElement.Radius = 0;

                DeserializationContext.Path.SetAttributeOnLast("r");

                NegativeValueIssue issue = new(DeserializationContext.Path.ToString());
                DeserializationContext.Warnings.Add(issue);
            }
            else
            {
                SvgElement.Radius = radius.Value;
            }
        }
    }

    private void ConvertCenter()
    {
        SvgElement.CenterX = XmlElement.Cx;
        SvgElement.CenterY = XmlElement.Cy;
    }

    private void ConvertGradientUnits()
    {
        if (XmlElement.GradientUnitsSpecified)
        {
            SvgElement.GradientUnits = XmlElement.GradientUnits switch
            {
                XmlGradientUnits.ObjectBoundingBox => SvgGradientUnits.ObjectBoundingBox,
                XmlGradientUnits.UserSpaceOnUse => SvgGradientUnits.UserSpaceOnUse,
                _ => throw new Exception("Invalid gradient unit value.")
            };
        }
    }

    private void ConvertStops()
    {
        if (XmlElement.Stops != null)
        {
            IEnumerable<SvgStop> svgStops = XmlElement.Stops
                .Select(x =>
                {
                    XmlStopToModelConversion conversion = new(x, DeserializationContext);
                    return conversion.Execute();
                });

            foreach (SvgStop svgStop in svgStops)
                SvgElement.Stops.Add(svgStop);
        }
    }

    private void ConvertGradientTransform()
    {
        if (XmlElement.GradientTransform != null)
            SvgElement.GradientTransforms.ParseAndAdd(XmlElement.GradientTransform);
    }

    private void ConvertHref()
    {
        if (XmlElement.Href != null)
            SvgElement.Href = XmlElement.Href;
    }
}