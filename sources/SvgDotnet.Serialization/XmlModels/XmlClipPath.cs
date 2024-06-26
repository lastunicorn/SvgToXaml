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

public class XmlClipPath : XmlElement
{
    // Descriptive elements
    [XmlElement("desc", typeof(XmlDesc))]
    [XmlElement("title", typeof(XmlTitle))]
    //[XmlElement("metadata", typeof())]

    // Shape elements
    [XmlElement("circle", typeof(XmlCircle))]
    [XmlElement("ellipse", typeof(XmlEllipse))]
    [XmlElement("line", typeof(XmlLine))]
    [XmlElement("path", typeof(XmlPath))]
    [XmlElement("polygon", typeof(XmlPolygon))]
    [XmlElement("polyline", typeof(XmlPolyline))]
    [XmlElement("rect", typeof(XmlRect))]

    // Other elements
    [XmlElement("text", typeof(XmlText))]
    [XmlElement("use", typeof(XmlUse))]
    //[XmlElement("script", typeof(XmlScript))]
    public object[] Children { get; set; }
}