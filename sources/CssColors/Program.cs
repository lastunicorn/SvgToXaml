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

using System.Reflection;

namespace DustInTheWind.CssColors;

internal class Program
{
    private static void Main(string[] args)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream stream = assembly.GetManifestResourceStream("DustInTheWind.CssColors.colors.csv");
        using StreamReader streamReader = new(stream);

        string line = streamReader.ReadLine();

        List<CssColor> colors = new();

        while (line != null)
        {
            CssColor cssColor = ParseLine(line);
            colors.Add(cssColor);

            line = streamReader.ReadLine();
        }

        foreach (CssColor cssColor in colors)
        {
            string propertyName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(cssColor.Name);
            Console.WriteLine("public static SvgColor {0} {{ get; }} = new({1}, {2}, {3});", propertyName, cssColor.Red, cssColor.Green, cssColor.Blue);
        }

        foreach (CssColor cssColor in colors)
        {
            string propertyName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(cssColor.Name);
            Console.WriteLine("\"{0}\" => {1},", cssColor.Name, propertyName);
        }
    }

    private static CssColor ParseLine(string line)
    {
        string[] parts = line.Split(';');

        if (parts.Length != 3)
            throw new Exception();

        string name = parts[0];

        string hexText = parts[1];

        if (!hexText.StartsWith("#"))
            throw new Exception();

        byte hexRed = Convert.ToByte(hexText.Substring(1, 2), 16);
        byte hexGreen = Convert.ToByte(hexText.Substring(3, 2), 16);
        byte hexBlue = Convert.ToByte(hexText.Substring(5, 2), 16);

        string decText = parts[2];

        string[] decParts = decText.Split(' ');

        byte decRed = byte.Parse(decParts[0]);
        byte decGreen = byte.Parse(decParts[1]);
        byte decBlue = byte.Parse(decParts[2]);

        if (decRed != hexRed)
            throw new Exception();

        if (decGreen != hexGreen)
            throw new Exception();

        if (decBlue != hexBlue)
            throw new Exception();

        CssColor cssColor = new()
        {
            Name = name,
            Red = decRed,
            Green = decGreen,
            Blue = decBlue
        };

        return cssColor;
    }
}