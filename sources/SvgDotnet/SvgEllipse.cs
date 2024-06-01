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

namespace DustInTheWind.SvgDotnet;

public class SvgEllipse : SvgShape
{
    private double radiusX;
    private double radiusY;

    protected override string ElementName => "ellipse";

    public double RadiusX
    {
        get => radiusX;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Radius X cannot be a negative value. It must be a positive, finite number.");

            if (double.IsNaN(value))
                throw new ArgumentOutOfRangeException(nameof(value), "Radius X must be a positive, finite number.");

            radiusX = value;
        }
    }

    public double RadiusY
    {
        get => radiusY;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Radius Y cannot be a negative value. It must be a positive, finite number.");

            if (double.IsNaN(value))
                throw new ArgumentOutOfRangeException(nameof(value), "Radius Y must be a positive, finite number.");

            radiusY = value;
        }
    }

    public double CenterX { get; set; }

    public double CenterY { get; set; }
}