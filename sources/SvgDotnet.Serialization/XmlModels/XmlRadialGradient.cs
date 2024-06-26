﻿// SvgToXaml
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

namespace DustInTheWind.SvgDotnet.Serialization.XmlModels;

public class XmlRadialGradient : XmlElement
{
    [XmlAttribute("cx")]
    public string Cx { get; set; }

    [XmlAttribute("cy")]
    public string Cy { get; set; }

    [XmlAttribute("r")]
    public string R { get; set; }

    [XmlAttribute("fx")]
    public string Fx { get; set; }

    [XmlAttribute("fy")]
    public string Fy { get; set; }

    [XmlAttribute("gradientUnits")]
    public XmlGradientUnits GradientUnits { get; set; }

    public bool GradientUnitsSpecified { get; set; }

    [XmlAttribute("gradientTransform")]
    public string GradientTransform { get; set; }

    [XmlAttribute("href", Namespace = Namespaces.XLink)]
    public string Href { get; set; }

    [XmlAttribute("spreadMethod")]
    public XmlSpreadMethod SpreadMethod { get; set; }

    public bool SpreadMethodSpecified { get; set; }

    // Descriptive elements
    [XmlElement("desc", typeof(XmlDesc))]
    [XmlElement("title", typeof(XmlTitle))]
    //[XmlElement("metadata", typeof())]

    // Other elements
    //[XmlElement("animate", typeof())]
    //[XmlElement("animateTransform", typeof())]
    //[XmlElement("script", typeof())]
    //[XmlElement("set", typeof())]
    [XmlElement("stop", typeof(XmlStop))]
    [XmlElement("style", typeof(XmlStyle))]
    public object[] Children { get; set; }
}