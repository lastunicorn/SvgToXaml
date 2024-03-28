// Country Flags
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

namespace DustInTheWind.SvgToXaml.Svg;

public struct HypertextReference
{
    private readonly string rawValue;

    public string Id { get; }

    public HypertextReference(string value)
    {
        rawValue = value;

        Id = value != null && value.StartsWith("#")
            ? value[1..]
            : null;
    }

    public static implicit operator HypertextReference(string value)
    {
        return new HypertextReference(value);
    }

    public static implicit operator string(HypertextReference hypertextReference)
    {
        return hypertextReference.rawValue;
    }
}