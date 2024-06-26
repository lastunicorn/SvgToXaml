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

public class XmlSymbol : XmlContainer
{
    [XmlAttribute("x")]
    public string X { get; set; }

    [XmlAttribute("y")]
    public string Y { get; set; }

    [XmlAttribute("width")]
    public string Width { get; set; }

    [XmlAttribute("height")]
    public string Height { get; set; }

    [XmlAttribute("viewBox")]
    public string ViewBox { get; set; }
}