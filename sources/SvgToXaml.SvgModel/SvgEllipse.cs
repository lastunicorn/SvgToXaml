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

using DustInTheWind.SvgToXaml.SvgSerialization;

namespace DustInTheWind.SvgToXaml.SvgModel;

public class SvgEllipse : SvgShape
{
    public double RadiusX { get; set; }

    public double RadiusY { get; set; }

    public double CenterX { get; set; }

    public double CenterY { get; set; }

    public SvgEllipse()
    {
    }

    internal SvgEllipse(Ellipse ellipse)
        : base(ellipse)
    {
        if (ellipse == null) throw new ArgumentNullException(nameof(ellipse));

        RadiusX = ellipse.Rx;
        RadiusY = ellipse.Ry;
        CenterX = ellipse.Cx;
        CenterY = ellipse.Cy;
    }
}