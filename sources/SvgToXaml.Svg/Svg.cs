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

namespace DustInTheWind.SvgToXaml.Svg;

public class Svg : SvgGroup
{
    public Size Width { get; set; }

    public Size Height { get; set; }

    public SvgViewBox ViewBox { get; set; }

    public Svg()
    {
    }

    internal Svg(Serialization.Svg svg)
        : base(svg)
    {
        if (svg == null) throw new ArgumentNullException(nameof(svg));

        if (svg.Width != null)
            Width = svg.Width;

        if (svg.Height != null)
            Height = svg.Height;

        if (svg.ViewBox != null)
            ViewBox = SvgViewBox.Parse(svg.ViewBox);
    }

    public IEnumerable<SvgStyleRuleSet> GetAllCssClasses()
    {
        IEnumerable<SvgGroup> svgGroups = GetAllGroups();

        foreach (SvgGroup svgGroup in svgGroups)
        {
            if (svgGroup.StyleSheet == null)
                continue;

            foreach (SvgStyleRuleSet cssClass in svgGroup.StyleSheet)
                yield return cssClass;
        }
    }

    private IEnumerable<SvgGroup> GetAllGroups()
    {
        yield return this;

        foreach (SvgElement svgElement in Children)
        {
            if (svgElement is SvgGroup svgGroup)
                yield return svgGroup;
        }
    }
}