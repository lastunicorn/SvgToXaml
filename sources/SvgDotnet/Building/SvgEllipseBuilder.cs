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

namespace DustInTheWind.SvgDotnet.Building;

internal class SvgEllipseBuilder
{
    private readonly SvgEllipse svgEllipse;

    public SvgEllipseBuilder()
    {
        svgEllipse = new SvgEllipse();
    }

    public SvgEllipseBuilder(SvgEllipse svgEllipse)
    {
        this.svgEllipse = svgEllipse ?? throw new ArgumentNullException(nameof(svgEllipse));
    }

    public SvgEllipse Build()
    {
        return svgEllipse;
    }
}