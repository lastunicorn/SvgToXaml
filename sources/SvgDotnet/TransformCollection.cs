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
using System.Text.RegularExpressions;

namespace DustInTheWind.SvgDotnet;

public class TransformCollection : Collection<ITransform>
{
    public void ParseAndAdd(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        IEnumerable<ITransform> transforms = Parse2(text)
            .Select(CreateTransform)
            .Where(x => x != null);

        foreach (ITransform transform in transforms)
            Items.Add(transform);
    }

    private static ITransform CreateTransform(KeyValuePair<string, string> item)
    {
        switch (item.Key)
        {
            case "translate":
                return new TranslateTransform(item.Value);

            case "translateX":
            case "translateY":
                throw new NotImplementedException();

            case "scale":
                return new ScaleTransform(item.Value);

            case "scaleX":
            case "scaleY":
                throw new NotImplementedException();

            case "rotate":
                return new RotateTransform(item.Value);

            case "matrix":
                return new MatrixTransform(item.Value);

            case "skew":
            case "skewX":
            case "skewY":
                throw new NotImplementedException();

            default:
                throw new NotImplementedException();
        }
    }

    private static IEnumerable<KeyValuePair<string, string>> Parse1(string text)
    {
        Regex regex = new(@"\s*(\w*)\s*\(\s*(.*?)\s*\)\s*");

        MatchCollection matches = regex.Matches(text);

        foreach (Match match in matches)
        {
            if (match.Success)
            {
                string key = match.Groups[1].Value;
                string value = match.Groups[2].Value;

                KeyValuePair<string, string> item = new(key, value);
                yield return item;
            }
        }
    }

    private static IEnumerable<KeyValuePair<string, string>> Parse2(string text)
    {
        TransformParser parser = new(text);

        while (parser.MoveNext())
            yield return parser.Current;
    }
}