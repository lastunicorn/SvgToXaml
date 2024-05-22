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

internal class XmlEllipseToModelConversion : XmlShapeToModelConversion<XmlEllipse, SvgEllipse>
{
    protected override string ElementName => "ellipse";

    public XmlEllipseToModelConversion(XmlEllipse xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgEllipse CreateSvgElement()
    {
        return new SvgEllipse();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertRadiusX();
        ConvertRadiusY();
        ConvertCenter();
    }

    private void ConvertRadiusX()
    {
        if (XmlElement.Rx < 0)
        {
            SvgElement.RadiusX = 0;

            DeserializationContext.Path.AddAttribute("rx");
            string path = DeserializationContext.Path.ToString();
            DeserializationContext.Path.RemoveLast();

            NegativeValueIssue issue = new(path);
            DeserializationContext.Warnings.Add(issue);
        }
        else
        {
            SvgElement.RadiusX = XmlElement.Rx;
        }
    }

    private void ConvertRadiusY()
    {
        if (XmlElement.Ry < 0)
        {
            SvgElement.RadiusY = 0;

            DeserializationContext.Path.AddAttribute("ry");
            string path = DeserializationContext.Path.ToString();
            DeserializationContext.Path.RemoveLast();

            NegativeValueIssue issue = new(path);
            DeserializationContext.Warnings.Add(issue);
        }
        else
        {
            SvgElement.RadiusY = XmlElement.Ry;
        }
    }

    private void ConvertCenter()
    {
        SvgElement.CenterX = XmlElement.Cx;
        SvgElement.CenterY = XmlElement.Cy;
    }
}