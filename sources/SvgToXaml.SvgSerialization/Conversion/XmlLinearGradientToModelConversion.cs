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

internal class XmlLinearGradientToModelConversion : XmlElementToModelConversion<XmlLinearGradient, SvgLinearGradient>
{
    protected override string ElementName => "linearGradient";

    public XmlLinearGradientToModelConversion(XmlLinearGradient xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgLinearGradient CreateSvgElement()
    {
        return new SvgLinearGradient();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertPoints();
        ConvertGradientUnits();
        ConvertStops();
        ConvertGradientTransform();
        ConvertHref();
    }

    private void ConvertPoints()
    {
        if (XmlElement.X1 != null)
            SvgElement.X1 = XmlElement.X1;

        if (XmlElement.Y1 != null)
            SvgElement.Y1 = XmlElement.Y1;

        if (XmlElement.X2 != null)
            SvgElement.X2 = XmlElement.X2;

        if (XmlElement.Y2 != null)
            SvgElement.Y2 = XmlElement.Y2;
    }

    private void ConvertGradientUnits()
    {
        if (XmlElement.GradientUnitsSpecified)
        {
            SvgElement.GradientUnits = XmlElement.GradientUnits switch
            {
                XmlGradientUnits.ObjectBoundingBox => SvgGradientUnits.ObjectBoundingBox,
                XmlGradientUnits.UserSpaceOnUse => SvgGradientUnits.UserSpaceOnUse,
                _ => throw new Exception("Invalid gradient unit value")
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