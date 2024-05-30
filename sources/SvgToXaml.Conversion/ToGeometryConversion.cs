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

using System.Windows;
using System.Windows.Media;
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class ToGeometryConversion
{
    private readonly SvgElement rootSvgElement;
    private readonly ConversionContext conversionContext;

    public ToGeometryConversion(SvgElement svgElement, ConversionContext conversionContext)
    {
        this.rootSvgElement = svgElement ?? throw new ArgumentNullException(nameof(svgElement));
        this.conversionContext = conversionContext ?? throw new ArgumentNullException(nameof(conversionContext));
    }

    public Geometry Execute()
    {
        return ConvertToGeometry(rootSvgElement);
    }

    private Geometry ConvertToGeometry(SvgElement svgElement)
    {
        switch (svgElement)
        {
            case SvgCircle svgCircle:
            {
                Point centerPoint = new(svgCircle.CenterX, svgCircle.CenterY);
                return new EllipseGeometry(centerPoint, svgCircle.Radius, svgCircle.Radius);
            }

            case SvgEllipse svgEllipse:
            {
                Point centerPoint = new(svgEllipse.CenterX, svgEllipse.CenterY);
                return new EllipseGeometry(centerPoint, svgEllipse.RadiusX, svgEllipse.RadiusY);
            }

            case SvgPath svgPath:
            {
                return Geometry.Parse(svgPath.Data);
            }

            case SvgLine svgLine:
            {
                Point startPoint = new(svgLine.X1, svgLine.Y1);
                Point endPoint = new(svgLine.X2, svgLine.Y2);
                return new LineGeometry(startPoint, endPoint);
            }

            case SvgRectangle svgRectangle:
            {
                Rect rect = new(svgRectangle.X, svgRectangle.Y, svgRectangle.Width, svgRectangle.Height);
                return new RectangleGeometry(rect);
            }

            case SvgPolygon svgPolygon:
                conversionContext.Issues.AddError("Failing to transform SvgPolygon into a Geometry. Reason: not implemented.");
                return null;

            case SvgPolyline svgPolyline:
                conversionContext.Issues.AddError("Failing to transform SvgPolyline into a Geometry. Reason: not implemented.");
                return null;

            case SvgUse svgUse:
            {
                string referencedId = svgUse.Href.Id;

                if (referencedId == null)
                    return Geometry.Empty;

                SvgElement referencedElement = svgElement.GetParentSvg().FindChild(referencedId);

                if (referencedElement == null)
                    return null;

                return ConvertToGeometry(referencedElement);
            }

            default:
                conversionContext.Issues.AddError($"Failed to transform '{svgElement?.GetType()}' into a Geometry. Reason: unknown type.");
                return null;
        }
    }
}