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

namespace DustInTheWind.SvgToXaml.SvgModel;

internal class TransformParser
{
    private readonly string text;
    private ParsingState parsingState;
    private string functionName;
    private int currentSectionStartIndex;
    private int currentIndex;
    private char currentCharacter;

    public KeyValuePair<string, string> Current { get; private set; }

    public TransformParser(string text)
    {
        this.text = text;

        Reset();
    }

    public void Reset()
    {
        Current = new KeyValuePair<string, string>();
        parsingState = ParsingState.BeforeFunctionName;
        functionName = null;
        currentSectionStartIndex = -1;
        currentIndex = -1;
        currentCharacter = '\0';
    }

    public bool MoveNext()
    {
        if (text == null)
            return false;

        while (!IsEndOfText())
        {
            switch (parsingState)
            {
                case ParsingState.BeforeFunctionName:
                    MoveToNextFunctionName();
                    break;

                case ParsingState.FunctionName:
                    ReadFunctionName();
                    break;

                case ParsingState.BeforeParenthesis:
                    MoveToParenthesisStart();
                    break;

                case ParsingState.BeforeParameters:
                    {
                        bool completeFunctionParsed = MoveToParameterStart();
                        if (completeFunctionParsed)
                            return true;

                        break;
                    }

                case ParsingState.Parameters:
                    {
                        bool completeFunctionParsed = ReadParameters();
                        if (completeFunctionParsed)
                            return true;

                        break;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return false;
    }

    private bool ReadParameters()
    {
        while (MoveToNextChar())
        {
            if (char.IsWhiteSpace(currentCharacter))
            {
                continue;
            }

            if (currentCharacter == '(')
            {
                // Advance until close parenthesis. (Skip the entire parenthesis content.)

                while (MoveToNextChar())
                {
                    if (currentCharacter == ')')
                        break;
                }

                if (IsEndOfText())
                    return false;

                functionName = null;
                parsingState = ParsingState.BeforeFunctionName;
                return false;
            }

            if (currentCharacter == ')')
            {
                int parametersLength = currentIndex - currentSectionStartIndex;
                string parameters = text.Substring(currentSectionStartIndex, parametersLength);

                Current = new KeyValuePair<string, string>(functionName, parameters);

                functionName = null;
                parsingState = ParsingState.BeforeFunctionName;

                return true;
            }
        }

        return false;
    }

    private void MoveToNextFunctionName()
    {
        while (MoveToNextChar())
        {
            if (char.IsWhiteSpace(currentCharacter))
            {
                // Skip white spaces
            }
            else if (currentCharacter == '(')
            {
                // Advance until close parenthesis. (Skip the entire parenthesis content.)

                while (MoveToNextChar())
                {
                    if (currentCharacter == ')')
                        break;
                }

                if (IsEndOfText())
                    break;
            }
            else if (currentCharacter == ')')
            {
                // Ignore closing parenthesis.
            }
            else
            {
                currentSectionStartIndex = currentIndex;
                parsingState = ParsingState.FunctionName;
                return;
            }
        }
    }

    private void ReadFunctionName()
    {
        while (MoveToNextChar())
        {
            if (char.IsWhiteSpace(currentCharacter))
            {
                int functionNameLength = currentIndex - currentSectionStartIndex;
                functionName = text.Substring(currentSectionStartIndex, functionNameLength);
                parsingState = ParsingState.BeforeParenthesis;
                return;
            }

            if (currentCharacter == '(')
            {
                int functionNameLength = currentIndex - currentSectionStartIndex;
                functionName = text.Substring(currentSectionStartIndex, functionNameLength);
                parsingState = ParsingState.BeforeParameters;
                return;
            }

            if (currentCharacter == ')')
            {
                // Invalid closing parenthesis. Search for a new function name.

                functionName = null;
                parsingState = ParsingState.BeforeFunctionName;
                return;
            }
        }
    }

    private void MoveToParenthesisStart()
    {
        while (MoveToNextChar())
        {
            if (char.IsWhiteSpace(currentCharacter))
            {
                // Skip white spaces.

                continue;
            }

            if (currentCharacter == '(')
            {
                // Found the starting of the value section.

                parsingState = ParsingState.BeforeParameters;
                return;
            }

            if (currentCharacter == ')')
            {
                // Invalid closing parenthesis => search for another function name.

                functionName = null;
                currentSectionStartIndex = -1;
                parsingState = ParsingState.BeforeFunctionName;
                return;
            }

            // Found a character. This is the start of another function name.

            functionName = null;
            currentSectionStartIndex = currentIndex;
            parsingState = ParsingState.FunctionName;
            return;
        }
    }

    private bool MoveToParameterStart()
    {
        while (MoveToNextChar())
        {
            if (char.IsWhiteSpace(currentCharacter))
            {
                // Skip white spaces

                continue;
            }

            if (currentCharacter == '(')
            {
                // Advance until close parenthesis. (Skip the entire parenthesis content.)

                while (MoveToNextChar())
                {
                    if (currentCharacter == ')')
                        break;
                }

                if (IsEndOfText())
                    break;

                functionName = null;
                currentSectionStartIndex = -1;
                parsingState = ParsingState.BeforeFunctionName;
                break;
            }

            if (currentCharacter == ')')
            {
                // Function has no parameters

                Current = new KeyValuePair<string, string>(functionName!, string.Empty);

                functionName = null;
                currentSectionStartIndex = -1;
                parsingState = ParsingState.BeforeFunctionName;

                return true;
            }

            currentSectionStartIndex = currentIndex;
            parsingState = ParsingState.Parameters;
            break;
        }

        return false;
    }

    private bool MoveToNextChar()
    {
        if (IsEndOfText())
            return false;

        currentIndex++;

        if (IsEndOfText())
        {
            currentCharacter = '\0';
            return false;
        }

        currentCharacter = text[currentIndex];
        return true;
    }

    private bool IsEndOfText()
    {
        return currentIndex == text.Length;
    }

    private enum ParsingState
    {
        BeforeFunctionName,
        FunctionName,
        BeforeParenthesis,
        BeforeParameters,
        Parameters
    }
}