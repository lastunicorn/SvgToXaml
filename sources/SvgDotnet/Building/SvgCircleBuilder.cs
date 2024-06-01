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

internal class SvgCircleBuilder
{
    private readonly SvgCircle svgCircle;

    public SvgCircleBuilder()
    {
        svgCircle = new SvgCircle();
    }

    public SvgCircleBuilder(SvgCircle svgCircle)
    {
        this.svgCircle = svgCircle ?? throw new ArgumentNullException(nameof(svgCircle));
    }

    public static SvgCircleBuilder Create()
    {
        return new SvgCircleBuilder();
    }

    public SvgCircleBuilder WithId(string id)
    {
        svgCircle.Id = id;
        return this;
    }

    public SvgCircleBuilder WithRadius(double radius)
    {
        svgCircle.Radius = radius;
        return this;
    }

    public SvgCircleBuilder WithCenter(double centerX, double centerY)
    {
        svgCircle.CenterX = centerX;
        svgCircle.CenterY = centerY;
        return this;
    }

    public SvgCircle Build()
    {
        return svgCircle;
    }
}