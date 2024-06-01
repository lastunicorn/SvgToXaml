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
    public string Name { get; }

    public string Value { get; }

    public StyleDeclaration(string name, string value)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static implicit operator StyleDeclaration(string text)
    {
        if (text == null)
            return null;

        int pos = text.IndexOf(':');

        if (pos == -1)
            return null;

        string name = text[..pos].Trim();
        string value = text[(pos + 1)..].Trim();

        return new StyleDeclaration(name, value);
    }

    public override string ToString()
    {
        return $"{Name}:{Value}";
    }
}