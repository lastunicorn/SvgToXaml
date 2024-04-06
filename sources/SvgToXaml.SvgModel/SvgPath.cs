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

using Path = DustInTheWind.SvgToXaml.SvgSerialization.Path;

namespace DustInTheWind.SvgToXaml.SvgModel;

public class SvgPath : SvgShape
{
    /// <remarks>
    /// Possible values: none | &lt;string&gt;
    /// Initial value: none
    /// Inherited: no
    /// </remarks>>
    // todo: allow Data to accept "none".
    public string Data { get; set; }

    public SvgPath()
    {
    }

    internal SvgPath(Path path)
        : base(path)
    {
        if (path == null) throw new ArgumentNullException(nameof(path));

        Data = path.D;
    }
}