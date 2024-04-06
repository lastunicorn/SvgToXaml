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

// a
// clipPath
// defs
// g
// marker
// mask
// pattern
// svg
// switch
// symbol
// unknown

public class SvgContainer : SvgElement
{
    public SvgElementCollection<SvgElement> Children { get; }

    public SvgContainer()
    {
        Children = new SvgElementCollection<SvgElement>(this);
    }

    public SvgContainer(Element element)
        : base(element)
    {
        Children = new SvgElementCollection<SvgElement>(this);
    }

    public virtual SvgElement FindChild(string id)
    {
        return Children.FindChild(id);
    }
}