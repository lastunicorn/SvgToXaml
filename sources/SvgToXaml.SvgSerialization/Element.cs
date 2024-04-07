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

namespace DustInTheWind.SvgToXaml.SvgSerialization;

public class Element
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
    public FillRule FillRule { get; set; }

    public bool FillRuleSpecified { get; set; }

    //[XmlAttribute("fill-opacity")]
    //public string FillOpacity { get; set; }

    [XmlAttribute("stroke")]
    public string Stroke { get; set; }

    //[XmlAttribute("stroke-opacity")]
    //public string StrokeOpacity { get; set; }

    [XmlAttribute("stroke-width")]
    public double StrokeWidth { get; set; }

    public bool StrokeWidthSpecified { get; set; }

    [XmlAttribute("stroke-linecap")]
    public StrokeLineCap StrokeLineCap { get; set; }
    
    public bool StrokeLineCapSpecified { get; set; }

    [XmlAttribute("stroke-linejoin")]
    public StrokeLineJoin StrokeLineJoin { get; set; }
    
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