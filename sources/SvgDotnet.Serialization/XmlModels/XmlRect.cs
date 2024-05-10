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

namespace DustInTheWind.SvgDotnet.Serialization.XmlModels;

public class XmlRect : XmlShape
{
    [XmlAttribute("width")]
    public double Width { get; set; }

    [XmlAttribute("height")]
    public double Height { get; set; }

    [XmlAttribute("x")]
    public double X { get; set; }

    [XmlAttribute("y")]
    public double Y { get; set; }

    [XmlAttribute("rx")]
    public double Rx { get; set; }
    
    public bool RxSpecified { get; set; }

    [XmlAttribute("ry")]
    public double Ry { get; set; }

    public bool RySpecified { get; set; }
}