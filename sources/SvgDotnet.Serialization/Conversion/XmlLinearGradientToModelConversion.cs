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

using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

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
        ConvertChildren();
        ConvertGradientTransform();
        ConvertHref();
        ConvertSpreadMethod();
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

    private void ConvertChildren()
    {
        if (XmlElement.Children != null)
        {
            IEnumerable<SvgElement> elements = XmlElement.Children
                .Select(CreateConversionFor)
                .Where(x => x != null)
                .Select(x => x.Execute());

            foreach (SvgElement svgElement in elements)
                SvgElement.Children.Add(svgElement);
        }
    }

    private IToModelConversion<SvgElement> CreateConversionFor(object objectToConvert)
    {
        switch (objectToConvert)
        {
            case XmlDesc desc:
                return new XmlDescToModelConversion(desc, DeserializationContext);

            case XmlTitle title:
                return new XmlTitleToModelConversion(title, DeserializationContext);

            case XmlStop stop:
                return new XmlStopToModelConversion(stop, DeserializationContext);

            case XmlStyle style:
                return new XmlStyleToModelConversion(style, DeserializationContext);

            default:
                DeserializationIssue deserializationIssue = new("Xml deserialization", $"Unknown element type {objectToConvert.GetType().Name} in {ElementName}.");
                DeserializationContext.Errors.Add(deserializationIssue);
                return null;
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

    private void ConvertSpreadMethod()
    {
        if (XmlElement.SpreadMethodSpecified)
        {
            SvgElement.SpreadMethod = XmlElement.SpreadMethod switch
            {
                XmlSpreadMethod.Pad => SvgSpreadMethod.Pad,
                XmlSpreadMethod.Reflect => SvgSpreadMethod.Reflect,
                XmlSpreadMethod.Repeat => SvgSpreadMethod.Repeat,
                _ => throw new Exception("Invalid spread method value.")
            };
        }
    }
}