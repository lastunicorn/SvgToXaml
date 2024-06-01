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
    public Svg Svg { get; }

    public SvgBuilder()
    {
        Svg = new Svg();
    }

    public SvgBuilder(Svg svg)
    {
        Svg = svg ?? throw new ArgumentNullException(nameof(svg));
    }

    public static SvgBuilder Create()
    {
        return new SvgBuilder();
    }

    public SvgBuilder AddGroup(Action<SvgGroupBuilder> action)
    {
        SvgGroupBuilder svgGroupBuilder = new();
        action(svgGroupBuilder);

        SvgGroup svgGroup = svgGroupBuilder.Build();
        Svg.AddChild(svgGroup);

        return this;
    }

    public SvgBuilder AddUse(Action<SvgUseBuilder> action)
    {
        SvgUseBuilder svgUseBuilder = new();
        action(svgUseBuilder);

        SvgUse svgUse = svgUseBuilder.Build();
        Svg.Children.Add(svgUse);

        return this;
    }

    public Svg Build()
    {
        return Svg;
    }
}