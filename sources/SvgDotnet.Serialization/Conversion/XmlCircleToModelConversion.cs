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

internal class XmlCircleToModelConversion : XmlElementToModelConversion<XmlCircle, SvgCircle>
{
    protected override string ElementName => "circle";

    public XmlCircleToModelConversion(XmlCircle xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgCircle CreateSvgElement()
    {
        return new SvgCircle();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertRadius();
        ConvertPosition();
    }

    private void ConvertRadius()
    {
        if (XmlElement.R < 0)
        {
            SvgElement.Radius = 0;

            DeserializationContext.Path.AddAttribute("r");
            string path = DeserializationContext.Path.ToString();
            DeserializationContext.Path.RemoveLast();

            NegativeValueIssue issue = new(path);
            DeserializationContext.Warnings.Add(issue);
        }
        else
        {
            SvgElement.Radius = XmlElement.R;
        }
    }

    private void ConvertPosition()
    {
        SvgElement.CenterX = XmlElement.Cx;
        SvgElement.CenterY = XmlElement.Cy;
    }
}