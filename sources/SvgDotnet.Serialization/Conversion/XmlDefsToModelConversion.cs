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

internal class XmlDefsToModelConversion : XmlContainerToModelConversion<XmlDefs, SvgDefinitions>
{
    protected override string ElementName => "defs";

    public XmlDefsToModelConversion(XmlDefs xmlContainer, DeserializationContext deserializationContext)
        : base(xmlContainer, deserializationContext)
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

    protected override SvgDefinitions CreateSvgElement()
    {
        return new SvgDefinitions();
    }
}