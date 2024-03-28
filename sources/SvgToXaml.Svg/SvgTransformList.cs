// Country Flags
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

using System.Collections.ObjectModel;

namespace DustInTheWind.SvgToXaml.Svg;

public class SvgTransformList : Collection<ISvgTransform>
{
    public void ParseAndAdd(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        IEnumerable<KeyValuePair<string, string>> items = Parse(text);

        foreach (KeyValuePair<string, string> item in items)
        {
            switch (item.Key)
            {
                case "translate":
                    SvgTranslateTransform svgTranslateTransform = new(item.Value);
                    Items.Add(svgTranslateTransform);
                    break;

                case "scale":
                    SvgScaleTransform svgScaleTransform = new(item.Value);
                    Items.Add(svgScaleTransform);
                    break;

                case "rotate":
                    SvgRotateTransform svgRotateTransform = new(item.Value);
                    Items.Add(svgRotateTransform);
                    break;

                case "matrix":
                    SvgMatrixTransform svgMatrixTransform = new(item.Value);
                    Items.Add(svgMatrixTransform);
                    break;
            }
        }
    }

    private static IEnumerable<KeyValuePair<string, string>> Parse(string text)
    {
        ParseState parseState = ParseState.ExpectName;

        string name = null;

        int startIndex = -1;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            switch (parseState)
            {
                case ParseState.ExpectName:
                    if (char.IsWhiteSpace(c))
                    {
                    }
                    else if (c == '(')
                    {
                        // advance until close parenthesis.

                        for (; i < text.Length; i++)
                        {
                            if (text[i] == ')')
                                break;
                        }
                    }
                    else if (c == ')')
                    {
                    }
                    else
                    {
                        startIndex = i;
                        parseState = ParseState.Name;
                    }

                    break;

                case ParseState.Name:
                    if (char.IsWhiteSpace(c))
                    {
                        name = text.Substring(startIndex, i - startIndex);
                        parseState = ParseState.ExpectValueStart;
                    }
                    else if (c == '(')
                    {
                        name = text.Substring(startIndex, i - startIndex);
                        parseState = ParseState.ExpectValue;
                    }
                    else if (c == ')')
                    {
                        name = null;
                        parseState = ParseState.ExpectName;
                    }

                    break;

                case ParseState.ExpectValueStart:
                    if (char.IsWhiteSpace(c))
                    {
                    }
                    else if (c == '(')
                    {
                        parseState = ParseState.ExpectValue;
                    }
                    else if (c == ')')
                    {
                        name = null;
                        parseState = ParseState.ExpectName;
                    }
                    else
                    {
                        name = null;
                        startIndex = i;
                    }

                    break;

                case ParseState.ExpectValue:
                    if (char.IsWhiteSpace(c))
                    {
                    }
                    else if (c == '(')
                    {
                        // advance until close parenthesis.

                        for (; i < text.Length; i++)
                        {
                            if (text[i] == ')')
                                break;
                        }

                        name = null;
                        parseState = ParseState.ExpectName;
                    }
                    else if (c == ')')
                    {
                        yield return new KeyValuePair<string, string>(name!, string.Empty);

                        name = null;
                        parseState = ParseState.ExpectName;
                    }
                    else
                    {
                        startIndex = i;
                        parseState = ParseState.Value;
                    }

                    break;

                case ParseState.Value:
                    if (char.IsWhiteSpace(c))
                    {
                    }
                    else if (c == '(')
                    {
                        // advance until close parenthesis.

                        for (; i < text.Length; i++)
                        {
                            if (text[i] == ')')
                                break;
                        }

                        name = null;
                        parseState = ParseState.ExpectName;
                    }
                    else if (c == ')')
                    {
                        string value = text.Substring(startIndex, i - startIndex);
                        yield return new KeyValuePair<string, string>(name!, value);

                        name = null;
                        parseState = ParseState.ExpectName;
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private enum ParseState
    {
        ExpectName,
        Name,
        ExpectValueStart,
        ExpectValue,
        Value
    }
}