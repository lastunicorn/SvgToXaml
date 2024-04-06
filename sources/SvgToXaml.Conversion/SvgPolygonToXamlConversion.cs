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

using System.Windows.Shapes;
using DustInTheWind.SvgToXaml.SvgModel;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgPolygonToXamlConversion : SvgShapeToXamlConversion<SvgPolygon, Polygon>
{
    public SvgPolygonToXamlConversion(SvgPolygon svgPolygon, SvgElement referrer = null)
        : base(svgPolygon, referrer)
    {
    }

    protected override Polygon CreateXamlElement()
    {
        Polygon polygon = new()
        {
            Points = SvgElement.Points.ToXaml()
        };

        SetFillRule(polygon, SvgElement);

        return polygon;
    }

    private static void SetFillRule(Polygon polygon, SvgPolygon svgPolygon)
    {
        FillRule? fillRule = svgPolygon.ComputeFillRule();

        if (fillRule != null)
        {
            polygon.FillRule = fillRule switch
            {
                FillRule.EvenOdd => System.Windows.Media.FillRule.EvenOdd,
                FillRule.Nonzero => System.Windows.Media.FillRule.Nonzero,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}