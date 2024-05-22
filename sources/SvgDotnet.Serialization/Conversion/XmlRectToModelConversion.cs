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

internal class XmlRectToModelConversion : XmlShapeToModelConversion<XmlRect, SvgRectangle>
{
    protected override string ElementName => "rect";

    public XmlRectToModelConversion(XmlRect xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgRectangle CreateSvgElement()
    {
        return new SvgRectangle();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertSize();
        ConvertLocation();
        ConvertCornerRadius();
    }

    private void ConvertSize()
    {
        SvgElement.Width = XmlElement.Width;
        SvgElement.Height = XmlElement.Height;
    }

    private void ConvertLocation()
    {
        SvgElement.X = XmlElement.X;
        SvgElement.Y = XmlElement.Y;
    }

    private void ConvertCornerRadius()
    {
        if (XmlElement.RxSpecified)
            SvgElement.Rx = XmlElement.Rx;

        if (XmlElement.RySpecified)
            SvgElement.Ry = XmlElement.Ry;
    }
}