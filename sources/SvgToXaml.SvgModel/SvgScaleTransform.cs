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

using System.Globalization;

namespace DustInTheWind.SvgToXaml.SvgModel;

public class SvgScaleTransform : ISvgTransform
{
    public double? CenterX { get; set; }

    public double? CenterY { get; set; }

    public double? ScaleX { get; set; }

    public double? ScaleY { get; set; }

    public SvgScaleTransform(string text)
    {
        if (text == null)
            return;

        string[] parts = text.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (parts.Length >= 1)
        {
            double scaleValue = double.Parse(parts[0], CultureInfo.InvariantCulture);
            ScaleX = scaleValue;
        }

        if (parts.Length >= 2)
        {
            double scaleValue = double.Parse(parts[1], CultureInfo.InvariantCulture);
            ScaleY = scaleValue;
        }
        else
        {
            ScaleY = ScaleX;
        }
    }
}