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
using DustInTheWind.SvgToXaml.SvgModel;
using FillRule = System.Windows.Media.FillRule;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgPathToXamlConversion : SvgShapeToXamlConversion<SvgPath, Path>
{
    public SvgPathToXamlConversion(SvgPath svgPath, SvgElement referrer = null)
        : base(svgPath, referrer)
    {
    }

    protected override Path CreateXamlElement()
    {
        return new Path
        {
            Data = SvgElement.Data is null or "none" 
                ? Geometry.Empty
                : ParseGeometry(SvgElement.Data)
        };
    }

    private Geometry ParseGeometry(string text)
    {
        Geometry geometry = Geometry.Parse(text);

        // Svg Default = nonzero
        // Xaml Default = evenodd

        if (geometry is GeometryGroup geometryGroup)
        {
            FillRule? fillRule = ComputeFillRule();

            if (fillRule != null)
            {
                geometryGroup = geometryGroup.Clone();
                geometryGroup.FillRule = fillRule.Value;
                geometryGroup.Freeze();
                
                return geometryGroup;
            }
        }
        else if (geometry is PathGeometry pathGeometry)
        {
            FillRule? fillRule = ComputeFillRule();

            if (fillRule != null)
            {
                pathGeometry = pathGeometry.Clone();
                pathGeometry.FillRule = fillRule.Value;
                pathGeometry.Freeze();
                return pathGeometry;
            }
        }
        else if (geometry is StreamGeometry streamGeometry)
        {
            FillRule? fillRule = ComputeFillRule();

            if (fillRule != null)
            {
                streamGeometry = streamGeometry.Clone();
                streamGeometry.FillRule = fillRule.Value;
                streamGeometry.Freeze();
                return streamGeometry;
            }
        }

        return geometry;
    }

    private FillRule? ComputeFillRule()
    {
        switch (SvgElement.FillRule)
        {
            case null:
            case SvgModel.FillRule.Nonzero:
                return FillRule.Nonzero;

            case SvgModel.FillRule.EvenOdd:
                return null;

            default:
                throw new Exception("Invalid value for FillRule.");
        }
    }
}