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

namespace DustInTheWind.SvgToXaml.Svg.Serialization;

public class LinearGradient : Element
{
    [XmlAttribute("x1")]
    public string X1 { get; set; }

    public bool X1Specified { get; set; }

    [XmlAttribute("y1")]
    public string Y1 { get; set; }

    public bool Y1Specified { get; set; }

    [XmlAttribute("x2")]
    public string X2 { get; set; }

    public bool X2Specified { get; set; }

    [XmlAttribute("y2")]
    public string Y2 { get; set; }

    public bool Y2Specified { get; set; }

    [XmlAttribute("gradientUnits")]
    public GradientUnits GradientUnits { get; set; }
    
    public bool GradientUnitsSpecified { get; set; }

    [XmlElement("stop")]
    public List<Stop> Stops { get; set; }

    [XmlAttribute("gradientTransform")]
    public string GradientTransform { get; set; }

    [XmlAttribute("href", Namespace = "http://www.w3.org/1999/xlink")]
    public string Href { get; set; }
}