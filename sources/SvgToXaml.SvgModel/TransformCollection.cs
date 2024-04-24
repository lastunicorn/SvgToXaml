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

using System.Collections.ObjectModel;

namespace DustInTheWind.SvgToXaml.SvgModel;

public class TransformCollection : Collection<ITransform>
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
                    TranslateTransform translateTransform = new(item.Value);
                    Items.Add(translateTransform);
                    break;

                case "translateX":
                case "translateY":
                    throw new NotImplementedException();

                case "scale":
                    ScaleTransform scaleTransform = new(item.Value);
                    Items.Add(scaleTransform);
                    break;

                case "scaleX":
                case "scaleY":
                    throw new NotImplementedException();

                case "rotate":
                    RotateTransform rotateTransform = new(item.Value);
                    Items.Add(rotateTransform);
                    break;

                case "matrix":
                    MatrixTransform matrixTransform = new(item.Value);
                    Items.Add(matrixTransform);
                    break;

                case "skew":
                case "skewX":
                case "skewY":
                    throw new NotImplementedException();

            }
        }
    }

    private static IEnumerable<KeyValuePair<string, string>> Parse(string text)
    {
        ParsingState parsingState = ParsingState.ExpectName;

        string name = null;

        int startIndex = -1;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            switch (parsingState)
            {
                case ParsingState.ExpectName:
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
                        parsingState = ParsingState.Name;
                    }

                    break;

                case ParsingState.Name:
                    if (char.IsWhiteSpace(c))
                    {
                        name = text.Substring(startIndex, i - startIndex);
                        parsingState = ParsingState.ExpectValueStart;
                    }
                    else if (c == '(')
                    {
                        name = text.Substring(startIndex, i - startIndex);
                        parsingState = ParsingState.ExpectValue;
                    }
                    else if (c == ')')
                    {
                        name = null;
                        parsingState = ParsingState.ExpectName;
                    }

                    break;

                case ParsingState.ExpectValueStart:
                    if (char.IsWhiteSpace(c))
                    {
                    }
                    else if (c == '(')
                    {
                        parsingState = ParsingState.ExpectValue;
                    }
                    else if (c == ')')
                    {
                        name = null;
                        parsingState = ParsingState.ExpectName;
                    }
                    else
                    {
                        name = null;
                        startIndex = i;
                    }

                    break;

                case ParsingState.ExpectValue:
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
                        parsingState = ParsingState.ExpectName;
                    }
                    else if (c == ')')
                    {
                        yield return new KeyValuePair<string, string>(name!, string.Empty);

                        name = null;
                        parsingState = ParsingState.ExpectName;
                    }
                    else
                    {
                        startIndex = i;
                        parsingState = ParsingState.Value;
                    }

                    break;

                case ParsingState.Value:
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
                        parsingState = ParsingState.ExpectName;
                    }
                    else if (c == ')')
                    {
                        string value = text.Substring(startIndex, i - startIndex);
                        yield return new KeyValuePair<string, string>(name!, value);

                        name = null;
                        parsingState = ParsingState.ExpectName;
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private enum ParsingState
    {
        ExpectName,
        Name,
        ExpectValueStart,
        ExpectValue,
        Value
    }
}