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

internal class SvgGroupBuilder
{
    private readonly SvgGroup svgGroup;

    public SvgGroupBuilder()
    {
        svgGroup = new SvgGroup();
    }

    public SvgGroupBuilder(SvgGroup svgGroup)
    {
        this.svgGroup = svgGroup ?? throw new ArgumentNullException(nameof(svgGroup));
    }

    public SvgGroupBuilder AddCircle(Action<SvgCircleBuilder> action)
    {
        SvgCircleBuilder svgCircleBuilder = new();
        action(svgCircleBuilder);

        SvgCircle svgCircle = svgCircleBuilder.Build();
        svgGroup.Children.Add(svgCircle);

        return this;
    }

    public SvgGroupBuilder AddUse(Action<SvgUseBuilder> action)
    {
        SvgUseBuilder svgUseBuilder = new();
        action(svgUseBuilder);

        SvgUse svgUse = svgUseBuilder.Build();
        svgGroup.Children.Add(svgUse);

        return this;
    }

    public SvgGroup Build()
    {
        return svgGroup;
    }
}