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

internal class SvgPolylineToXamlConversion : SvgShapeToXamlConversion<SvgPolyline, Polyline>
{
    public SvgPolylineToXamlConversion(SvgPolyline svgPolyline, SvgElement referrer = null)
        : base(svgPolyline, referrer)
    {
    }

    protected override Polyline CreateXamlElement()
    {
        Polyline polyline = new()
        {
            Points = SvgElement.Points.ToXaml()
        };

        SetFillRule(polyline, SvgElement);

        return polyline;
    }

    private static void SetFillRule(Polyline polyline, SvgPolyline svgPolyline)
    {
        FillRule? fillRule = svgPolyline.CalculateFillRule();

        if (fillRule != null)
        {
            polyline.FillRule = fillRule switch
            {
                FillRule.EvenOdd => System.Windows.Media.FillRule.EvenOdd,
                FillRule.Nonzero => System.Windows.Media.FillRule.Nonzero,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}