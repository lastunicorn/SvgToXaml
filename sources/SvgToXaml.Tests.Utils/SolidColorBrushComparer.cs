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

using System.Windows.Media;

namespace DustInTheWind.SvgToXaml.Tests.Utils;

public class SolidColorBrushComparer : IEqualityComparer<SolidColorBrush>
{
    public bool Equals(SolidColorBrush x, SolidColorBrush y)
    {
        return x.Color == y.Color && x.Opacity == y.Opacity;
    }

    public int GetHashCode(SolidColorBrush obj)
    {
        var o = new
        {
            C = obj.Color,
            O = obj.Opacity
        };

        return o.GetHashCode();
    }
}