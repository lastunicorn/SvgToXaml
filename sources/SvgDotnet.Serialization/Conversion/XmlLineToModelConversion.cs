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

using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

internal class XmlLineToModelConversion : XmlShapeToModelConversion<XmlLine, SvgLine>
{
    protected override string ElementName => "line";

    public XmlLineToModelConversion(XmlLine xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgLine CreateSvgElement()
    {
        return new SvgLine();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertPoints();
    }

    private void ConvertPoints()
    {
        SvgElement.X1 = XmlElement.X1;
        SvgElement.Y1 = XmlElement.Y1;
        SvgElement.X2 = XmlElement.X2;
        SvgElement.Y2 = XmlElement.Y2;
    }
}