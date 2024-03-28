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

using System.Collections;
using System.Globalization;

namespace DustInTheWind.SvgToXaml.Svg;

public class SvgPointCollection : IEnumerable<SvgPoint>
{
    private readonly List<SvgPoint> points = new();

    public SvgPointCollection()
    {
    }

    public SvgPointCollection(string value)
    {
        if (value == null)
            return;

        string[] parts = value.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (parts.Length % 2 != 0)
            throw new ArgumentException("Invalid number of points.", nameof(value));

        for (int i = 0; i < parts.Length; i += 2)
        {
            SvgPoint svgPoint = new()
            {
                X = double.Parse(parts[i], CultureInfo.InvariantCulture),
                Y = double.Parse(parts[i + 1], CultureInfo.InvariantCulture)
            };

            points.Add(svgPoint);
        }
    }

    public IEnumerator<SvgPoint> GetEnumerator()
    {
        return points.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}