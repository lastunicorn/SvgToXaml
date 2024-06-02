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
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal class TextWhiteSpaceProcessing
{
    private readonly List<SvgElement> shadowTree;

    public string Text { get; private set; }

    public TextWhiteSpaceProcessing(string text, List<SvgElement> shadowTree)
    {
        Text = text;
        this.shadowTree = shadowTree;
    }

    public void Execute()
    {
        WhiteSpacePreservation? whiteSpacePreservation = shadowTree
            .Select(x => x.ComputeWhiteSpacePreservation())
            .FirstOrDefault(x => x != null);

        if (whiteSpacePreservation != null)
        {
            Process(whiteSpacePreservation.Value);
        }
        else
        {
            SpacePreservation? spacePreservation = shadowTree
                .Select(x => x.ComputeSpacePreservation())
                .FirstOrDefault(x => x != null);

            if (spacePreservation != null)
                Process(spacePreservation.Value);
        }
    }

    private void Process(WhiteSpacePreservation whiteSpacePreservation)
    {
        bool collapseNewLines = whiteSpacePreservation is
            WhiteSpacePreservation.Normal or
            WhiteSpacePreservation.NoWrap;

        bool collapseSpacesAndTabs = whiteSpacePreservation is
            WhiteSpacePreservation.Normal or
            WhiteSpacePreservation.NoWrap or
            WhiteSpacePreservation.PreLine;

        if (collapseNewLines && collapseSpacesAndTabs)
            Text = Regex.Replace(Text, @"[\r\n|\r|\n| |\t]+", " ");
        else if (collapseNewLines)
            Text = Regex.Replace(Text, @"[\r\n|\r|\n]+", " ");
        else if (collapseSpacesAndTabs) 
            Text = Regex.Replace(Text, @"[ |\t]+", " ");

        bool trimSpaces = whiteSpacePreservation is
            WhiteSpacePreservation.Normal or
            WhiteSpacePreservation.NoWrap or
            WhiteSpacePreservation.PreLine;

        if (trimSpaces)
        {
            Regex regexTrim = new(@"^[^\S\r\n]*(.+?)[^\S\r\n]*$", RegexOptions.Multiline);
            Text = regexTrim.Replace(Text, "$1");
        }
    }

    private void Process(SpacePreservation spacePreservation)
    {
        if (spacePreservation == SpacePreservation.Default)
        {
            Text = Regex.Replace(Text, @"\s+", " ");
            Text = Text.Trim();
        }
    }
}