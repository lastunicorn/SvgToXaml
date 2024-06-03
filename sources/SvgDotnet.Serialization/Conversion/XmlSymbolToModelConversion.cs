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

internal class XmlSymbolToModelConversion : XmlContainerToModelConversion<XmlSymbol, SvgSymbol>
{
    protected override string ElementName => "symbol";

    public XmlSymbolToModelConversion(XmlSymbol xmlSymbol, DeserializationContext deserializationContext)
        : base(xmlSymbol, deserializationContext)
    {
        AllowedChildTypes.AddRange(new[]
        {
            typeof(XmlDesc),
            typeof(XmlTitle),

            typeof(XmlLinearGradient),
            typeof(XmlRadialGradient),

            typeof(XmlCircle),
            typeof(XmlEllipse),
            typeof(XmlLine),
            typeof(XmlPath),
            typeof(XmlPolygon),
            typeof(XmlPolyline),
            typeof(XmlRect),

            typeof(XmlDefs),
            typeof(XmlG),
            typeof(XmlSvg),
            typeof(XmlSymbol),
            typeof(XmlUse),

            typeof(XmlClipPath),
            typeof(XmlScript),
            typeof(XmlStyle),
            typeof(XmlText)
        });
    }

    protected override SvgSymbol CreateSvgElement()
    {
        return new SvgSymbol();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertLocation();
        ConvertSize();
        ConvertViewBox();
    }

    private void ConvertLocation()
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

    private void ConvertViewBox()
    {
        if (XmlElement.ViewBox != null)
            SvgElement.ViewBox = XmlElement.ViewBox;
    }
}