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

namespace DustInTheWind.SvgDotnet;

public class DashArray : IEnumerable<LengthPercentage>
{
    private readonly List<LengthPercentage> values = new();

    public DashArray()
    {
    }

    public DashArray(IEnumerable<LengthPercentage> values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));

        this.values.AddRange(values);
    }

    public static DashArray Parse(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        IEnumerable<LengthPercentage> values = text.Split(new []{ ' ', ',' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => (LengthPercentage)x);

        return new DashArray(values);
    }

    public IEnumerator<LengthPercentage> GetEnumerator()
    {
        return values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}