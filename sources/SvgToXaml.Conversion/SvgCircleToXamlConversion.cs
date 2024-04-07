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

namespace DustInTheWind.SvgToXaml.Conversion;

internal class SvgCircleToXamlConversion : SvgShapeToXamlConversion<SvgCircle, Ellipse>
{
    public SvgCircleToXamlConversion(SvgCircle svgCircle, SvgElement referrer = null)
        : base(svgCircle, referrer)
    {
    }

    protected override Ellipse CreateXamlElement()
    {
        Ellipse ellipse = new()
        {
            Width = SvgElement.Radius * 2,
            Height = SvgElement.Radius * 2
        };

        double left = SvgElement.CenterX - SvgElement.Radius;
        double top = SvgElement.CenterY - SvgElement.Radius;

        if (left != 0 || top != 0)
            ellipse.RenderTransform = new TranslateTransform(left, top);

        return ellipse;
    }
}