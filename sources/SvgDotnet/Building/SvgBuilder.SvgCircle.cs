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

internal partial class SvgBuilder
{
    public SvgBuilder AddCircle()
    {
        SvgCircle svgCircle = new();
        Svg.AddChild(svgCircle);

        return this;
    }

    public SvgBuilder AddCircle(Action<SvgCircleBuilder> action)
    {
        SvgCircleBuilder svgCircleBuilder = new();
        action(svgCircleBuilder);

        SvgCircle svgCircle = svgCircleBuilder.Build();
        Svg.Children.Add(svgCircle);

        return this;
    }

    public SvgBuilder AddCircle(Func<SvgCircle> action)
    {
        SvgCircle svgCircle = action();
        Svg.Children.Add(svgCircle);

        return this;
    }

    public SvgBuilder AddCircle(SvgCircle svgCircle)
    {
        Svg.Children.Add(svgCircle);

        return this;
    }
}