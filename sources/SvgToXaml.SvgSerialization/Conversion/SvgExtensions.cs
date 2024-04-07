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

using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal static class SvgExtensions
{
    public static Svg ToSvgModel(this XmlSvg xmlSvg)
    {
        if (xmlSvg == null)
            return null;

        Svg modelSvg = new();
        modelSvg.PopulateFromGroup(xmlSvg);

        if (xmlSvg.Width != null)
            modelSvg.Width = xmlSvg.Width;

        if (xmlSvg.Height != null)
            modelSvg.Height = xmlSvg.Height;

        if (xmlSvg.ViewBox != null)
            modelSvg.ViewBox = SvgViewBox.Parse(xmlSvg.ViewBox);

        return modelSvg;
    }
}