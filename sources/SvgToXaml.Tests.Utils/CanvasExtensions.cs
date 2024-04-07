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

using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.SvgToXaml.Tests.Utils;

public static class CanvasExtensions
{
    public static T GetFirstElement<T>(this Canvas canvas)
        where T : UIElement
    {
        return canvas.Children[0] as T;
    }

    public static T GetElementByIndex<T>(this Canvas canvas, int index)
        where T : UIElement
    {
        if (index < 0 || index >= canvas.Children.Count)
            throw new Exception($"Element was expected at index {index}, but it does not exist.");

        return canvas.Children[index] as T;
    }

    public static T GetElement<T>(this Canvas canvas)
        where T : UIElement
    {
        return canvas.Children
            .OfType<T>()
            .FirstOrDefault();
    }
}