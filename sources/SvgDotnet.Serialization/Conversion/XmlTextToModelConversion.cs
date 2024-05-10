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

internal class XmlTextToModelConversion : XmlElementToModelConversion<XmlText, SvgText>
{
    protected override string ElementName => "text";

    public XmlTextToModelConversion(XmlText xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgText CreateSvgElement()
    {
        return new SvgText();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        SvgElement.Text = XmlElement.Text;

        if (XmlElement.XSpecified)
            SvgElement.X = XmlElement.X;

        if (XmlElement.YSpecified)
            SvgElement.Y = XmlElement.Y;
    }
}