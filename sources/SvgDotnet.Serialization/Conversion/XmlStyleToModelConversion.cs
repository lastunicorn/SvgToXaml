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

using System.Text.RegularExpressions;
using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

internal class XmlStyleToModelConversion : XmlElementToModelConversion<XmlStyle, SvgStyle>
{
    private static readonly Regex Regex = new(@"\.(\w+)\s*{\s*(.*?)\s*}", RegexOptions.Multiline);

    protected override string ElementName => "style";

    public XmlStyleToModelConversion(XmlStyle xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgStyle CreateSvgElement()
    {
        return new SvgStyle();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        SvgElement.Title = XmlElement.Title;
        SvgElement.Type = XmlElement.Type;

        IEnumerable<StyleRuleSet> svgStyleRuleSets = ParseStyles(XmlElement.Value);
        SvgElement.RuleSets.AddRange(svgStyleRuleSets);
    }

    private static IEnumerable<StyleRuleSet> ParseStyles(string text)
    {
        if (text == null)
            return Enumerable.Empty<StyleRuleSet>();

        MatchCollection matches = Regex.Matches(text);

        return matches
            .Select(x => new StyleRuleSet
            {
                Selector = x.Groups[1].Value,
                Declarations = x.Groups[2].Value
            });
    }
}