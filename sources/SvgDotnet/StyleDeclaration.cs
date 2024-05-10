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

public class StyleDeclaration
{
    public string Name { get; init; }

    public string Value { get; init; }

    public static implicit operator StyleDeclaration(string text)
    {
        int pos = text.IndexOf(':');

        if (pos == -1)
            return null;

        return new StyleDeclaration
        {
            Name = text[..pos].Trim(),
            Value = text[(pos + 1)..].Trim()
        };
    }

    public override string ToString()
    {
        return $"{Name}:{Value}";
    }
}