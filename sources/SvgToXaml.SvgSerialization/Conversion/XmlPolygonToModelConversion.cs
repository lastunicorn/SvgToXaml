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

using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal class XmlPolygonToModelConversion : XmlElementToModelConversion<XmlPolygon, SvgPolygon>
{
    protected override string ElementName => "polygon";

    public XmlPolygonToModelConversion(XmlPolygon xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgPolygon CreateSvgElement()
    {
        return new SvgPolygon();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        if (XmlElement.Points != null)
        {
            IEnumerable<SvgPoint> points = SvgPoint.ParseMany(XmlElement.Points);

            foreach (SvgPoint point in points)
                SvgElement.Points.Add(point);
        }
    }
}