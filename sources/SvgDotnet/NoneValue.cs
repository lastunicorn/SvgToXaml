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

namespace DustInTheWind.SvgToXaml.SvgModel;

public struct NoneValue
{
    public bool Value { get; }

    public NoneValue(bool value)
    {
        Value = value;
    }

    public static NoneValue Parse(string text)
    {
        bool isNone = text?.Trim().Equals("none") == true;

        return isNone
            ? new NoneValue(true)
            : new NoneValue(false);
    }

    public static implicit operator NoneValue(string text)
    {
        return Parse(text);
    }

    public static implicit operator bool(NoneValue value)
    {
        return value.Value;
    }

    public static implicit operator NoneValue(bool value)
    {
        return new NoneValue(value);
    }
}