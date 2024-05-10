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

using System.Windows.Media;
using System.Windows.Shapes;
using DustInTheWind.SvgDotnet;
using FillRule = System.Windows.Media.FillRule;
using SvgFillRule = DustInTheWind.SvgDotnet.FillRule;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgPathToXamlConversion : SvgShapeToXamlConversion<SvgPath, Path>
{
    public SvgPathToXamlConversion(SvgPath svgPath, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgPath, conversionContext, referrer)
    {
    }

    protected override Path CreateXamlElement()
    {
        return new Path();
    }

    protected override void ConvertProperties(List<SvgElement> inheritedSvgElements)
    {
        base.ConvertProperties(inheritedSvgElements);

        SetData();
        SetFillRule(inheritedSvgElements);
    }

    private void SetData()
    {
        XamlElement.Data = SvgElement.Data is null or "none"
            ? Geometry.Empty
            : Geometry.Parse(SvgElement.Data);
    }

    private void SetFillRule(IEnumerable<SvgElement> svgElements)
    {
        if (XamlElement.Data.IsEmpty())
            return;

        SvgFillRule? svgFillRule = svgElements
            .Select(x => x.ComputeFillRule())
            .FirstOrDefault(x => x != null);

        FillRule? fillRule = ComputeFillRule(svgFillRule);

        if (fillRule == null)
            return;

        switch (XamlElement.Data)
        {
            case GeometryGroup geometryGroup:
            {
                geometryGroup = geometryGroup.Clone();
                geometryGroup.FillRule = fillRule.Value;
                geometryGroup.Freeze();

                XamlElement.Data = geometryGroup;
                break;
            }

            case PathGeometry pathGeometry:
            {
                pathGeometry = pathGeometry.Clone();
                pathGeometry.FillRule = fillRule.Value;
                pathGeometry.Freeze();
                XamlElement.Data = pathGeometry;
                break;
            }

            case StreamGeometry streamGeometry:
            {
                streamGeometry = streamGeometry.Clone();
                streamGeometry.FillRule = fillRule.Value;
                streamGeometry.Freeze();
                XamlElement.Data = streamGeometry;
                break;
            }
        }
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