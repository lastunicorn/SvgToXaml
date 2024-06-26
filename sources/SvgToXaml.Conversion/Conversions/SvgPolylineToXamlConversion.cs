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

using System.Windows.Shapes;
using DustInTheWind.SvgDotnet;
using FillRule = System.Windows.Media.FillRule;
using SvgFillRule = DustInTheWind.SvgDotnet.FillRule;

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal class SvgPolylineToXamlConversion : SvgShapeToXamlConversion<SvgPolyline, Polyline>
{
    public SvgPolylineToXamlConversion(SvgPolyline svgPolyline, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgPolyline, conversionContext, referrer)
    {
    }

    protected override Polyline CreateXamlElement()
    {
        return new Polyline();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        SetPoints();
        SetFillRule();
    }

    private void SetPoints()
    {
        XamlElement.Points = SvgElement.Points.ToXaml();
    }

    private void SetFillRule()
    {
        SvgFillRule? svgFillRule = ShadowTree
            .Select(x => x.ComputeFillRule())
            .FirstOrDefault(x => x != null);

        FillRule? fillRule = ComputeFillRule(svgFillRule);

        if (fillRule == null)
            return;

        XamlElement.FillRule = fillRule.Value;
    }

    private static FillRule? ComputeFillRule(SvgFillRule? fillRule)
    {
        // Svg Default = nonzero
        // Xaml Default = evenodd

        switch (fillRule)
        {
            case null:
            case SvgFillRule.Nonzero:
                return FillRule.Nonzero;

            case SvgFillRule.EvenOdd:
                return null;

            default:
                throw new Exception("Invalid value for FillRule.");
        }
    }
}