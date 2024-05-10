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

public class SvgStop : SvgElement
{
    public double Offset { get; set; }

    public SvgColor StopColor { get; set; }

    public SvgOpacity? StopOpacity { get; set; }

    public SvgColor ComputeStopColor()
    {
        StyleDeclaration styleDeclaration = Style?["stop-color"] ?? GetStyleValueFromClasses("stop-color");

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return StopColor;
    }

    public SvgOpacity? ComputeStopOpacity()
    {
        StyleDeclaration styleDeclaration = Style?["stop-opacity"] ?? GetStyleValueFromClasses("stop-opacity");

        if (styleDeclaration != null)
            return styleDeclaration.Value;

        return StopOpacity;
    }
}