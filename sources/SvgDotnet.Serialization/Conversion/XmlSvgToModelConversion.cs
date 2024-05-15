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

internal class XmlSvgToModelConversion : XmlContainerToModelConversion<XmlSvg, Svg>
{
    protected override string ElementName => "svg";

    public XmlSvgToModelConversion(XmlSvg xmlSvg, DeserializationContext deserializationContext)
        : base(xmlSvg, deserializationContext)
    {
    }

    protected override Svg CreateSvgElement()
    {
        return new Svg();
    }

    protected override void ConvertProperties()
    {
        if (XmlElement == null)
            return;

        base.ConvertProperties();

        LengthPercentage? x = XmlElement.X;

        if (x != null)
            SvgElement.X = x;

        LengthPercentage? y = XmlElement.Y;

        if (y != null)
            SvgElement.Y = y;

        if (XmlElement.Version != null)
            SvgElement.Version = XmlElement.Version;

        if (XmlElement.Width != null)
            SvgElement.Width = XmlElement.Width;

        if (XmlElement.Height != null)
            SvgElement.Height = XmlElement.Height;

        if (XmlElement.ViewBox != null)
            SvgElement.ViewBox = XmlElement.ViewBox;
    }
}