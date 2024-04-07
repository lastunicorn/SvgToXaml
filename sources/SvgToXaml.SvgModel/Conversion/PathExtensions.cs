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

namespace DustInTheWind.SvgToXaml.SvgModel.Conversion;

internal static class PathExtensions
{
    public static SvgPath ToSvgModel(this Path path)
    {
        if (path == null)
            return null;

        SvgPath model = new();
        model.PopulateFrom(path);

        model.Data = path.D;

        return model;
    }
}