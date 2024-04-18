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

using System.Xml.Serialization;

namespace DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

public class XmlElement
{
    // Core Attributes

    [XmlAttribute("id")]
    public string Id { get; set; }

    [XmlAttribute("class")]
    public string Class { get; set; }

    [XmlAttribute("style")]
    public string Style { get; set; }

    // Shape Attributes
    // (shapes and text content elements)

    [XmlAttribute("fill")]
    public string Fill { get; set; }

    [XmlAttribute("fill-rule")]
    public XmlFillRule FillRule { get; set; }

    public bool FillRuleSpecified { get; set; }

    [XmlAttribute("fill-opacity")]
    public double FillOpacity { get; set; }

    public bool FillOpacitySpecified { get; set; }

    [XmlAttribute("stroke")]
    public string Stroke { get; set; }

    [XmlAttribute("stroke-opacity")]
    public double StrokeOpacity { get; set; }

    public bool StrokeOpacitySpecified { get; set; }

    [XmlAttribute("stroke-width")]
    public string StrokeWidth { get; set; }

    [XmlAttribute("stroke-linecap")]
    public XmlStrokeLineCap StrokeLineCap { get; set; }

    public bool StrokeLineCapSpecified { get; set; }

    [XmlAttribute("stroke-linejoin")]
    public XmlStrokeLineJoin StrokeLineJoin { get; set; }

    public bool StrokeLineJoinSpecified { get; set; }

    [XmlAttribute("stroke-miterlimit")]
    public double StrokeMiterLimit { get; set; }

    public bool StrokeMiterLimitSpecified { get; set; }

    //[XmlAttribute("stroke-dasharray")]
    //public double StrokeDashArray { get; set; }

    [XmlAttribute("stroke-dashoffset")]
    public double StrokeDashOffset { get; set; }

    public bool StrokeDashOffsetSpecified { get; set; }

    //

    [XmlAttribute("transform")]
    public string Transform { get; set; }

    [XmlAttribute("opacity")]
    public double Opacity { get; set; }

    public bool OpacitySpecified { get; set; }

    [XmlAttribute("clip-path")]
    public string ClipPath { get; set; }
}