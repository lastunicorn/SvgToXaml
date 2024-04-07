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

/// <remarks>
/// May contain any number of the following elements, in any order:
///    - animation elements — ‘animate’, ‘animateMotion’, ‘animateTransform’, ‘discard’, ‘set’
///    - descriptive elements — ‘desc’, ‘title’, ‘metadata’
///    - paint server elements — ‘linearGradient’, ‘radialGradient’, ‘pattern’
///    - shape elements — ‘circle’, ‘ellipse’, ‘line’, ‘path’, ‘polygon’, ‘polyline’, ‘rect’
///    - structural elements — ‘defs’, ‘g’, ‘svg’, ‘symbol’, ‘use’
///    - a, audio, canvas, clipPath, filter, foreignObject, iframe, image, marker, mask, script, style, switch, text, video, view.
/// </remarks>
public class XmlG : XmlElement
{
    [XmlElement("circle", typeof(XmlCircle))]
    [XmlElement("ellipse", typeof(XmlEllipse))]
    [XmlElement("line", typeof(XmlLine))]
    [XmlElement("path", typeof(XmlPath))]
    [XmlElement("polygon", typeof(XmlPolygon))]
    [XmlElement("polyline", typeof(XmlPolyline))]
    [XmlElement("rect", typeof(XmlRect))]
    [XmlElement("defs", typeof(XmlDefs))]
    [XmlElement("g", typeof(XmlG))]
    [XmlElement("use", typeof(XmlUse))]
    [XmlElement("style", typeof(XmlStyle))]
    [XmlElement("text", typeof(XmlText))]
    [XmlElement("linearGradient", typeof(XmlLinearGradient))]
    [XmlElement("clipPath", typeof(XmlClipPath))]
    public object[] Children { get; set; }
}