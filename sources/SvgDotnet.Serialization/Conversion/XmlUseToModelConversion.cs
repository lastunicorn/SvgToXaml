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

internal class XmlUseToModelConversion : XmlContainerToModelConversion<XmlUse, SvgUse>
{
    protected override string ElementName => "use";

    public XmlUseToModelConversion(XmlUse xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
        AllowedChildTypes.AddRange(new[]
        {
            // Descriptive elements
            typeof(XmlDesc),
            typeof(XmlTitle),
            //typeof(XmlMetadata),
            
            // Other elements
            typeof(XmlClipPath),
            //typeof(XmlMask),
            typeof(XmlScript),
            typeof(XmlStyle)
        });
    }

    protected override SvgUse CreateSvgElement()
    {
        return new SvgUse();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertPosition();
        ConvertSize();
        ConvertReference();
    }

    private void ConvertPosition()
    {
        LengthPercentage? x = XmlElement.X;

        if (x != null)
            SvgElement.X = x;

        LengthPercentage? y = XmlElement.Y;

        if (y != null)
            SvgElement.Y = y;
    }

    private void ConvertSize()
    {
        if (XmlElement.Width != null)
            SvgElement.Width = XmlElement.Width;

        if (XmlElement.Height != null)
            SvgElement.Height = XmlElement.Height;
    }

    private void ConvertReference()
    {
        SvgElement.Href = XmlElement.Href ?? XmlElement.HrefLink;
    }
}