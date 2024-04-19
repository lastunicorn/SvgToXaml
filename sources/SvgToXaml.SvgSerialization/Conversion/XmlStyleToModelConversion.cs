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
using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal class XmlStyleToModelConversion : ToModelConversion<XmlStyle, SvgStyleSheet>
{
    private static readonly Regex Regex = new(@"\.(\w+)\s*{\s*(.*?)\s*}", RegexOptions.Multiline);

    protected override string ElementName => "style";

    public XmlStyleToModelConversion(XmlStyle xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgStyleSheet CreateSvgElement()
    {
        return new SvgStyleSheet();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        SvgElement.Title = XmlElement.Title;
        SvgElement.Type = XmlElement.Type;

        IEnumerable<SvgStyleRuleSet> svgStyleRuleSets = ParseStyles(XmlElement.Value);

        foreach (SvgStyleRuleSet svgStyleRuleSet in svgStyleRuleSets)
            SvgElement.Add(svgStyleRuleSet);
    }

    private static IEnumerable<SvgStyleRuleSet> ParseStyles(string text)
    {
        if (text == null)
            return Enumerable.Empty<SvgStyleRuleSet>();

        MatchCollection matches = Regex.Matches(text);

        return matches
            .Select(x => new SvgStyleRuleSet
            {
                Selector = x.Groups[1].Value,
                Declarations = x.Groups[2].Value
            });
    }
}