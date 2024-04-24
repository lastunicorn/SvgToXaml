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
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace DustInTheWind.SvgToXaml.Application.Transform;

internal static class UiElementExtensions
{
    public static void AddToParent(this UIElement child, DependencyObject parent, int? index = null)
    {
        if (parent == null)
            return;

        if (parent is ItemsControl itemsControl)
        {
            if (index == null)
                itemsControl.Items.Add(child);
            else
                itemsControl.Items.Insert(index.Value, child);
        }
        else if (parent is Panel panel)
        {
            if (index == null)
                panel.Children.Add(child);
            else
                panel.Children.Insert(index.Value, child);
        }
        else if (parent is Decorator decorator)
        {
            decorator.Child = child;
        }
        else if (parent is ContentPresenter contentPresenter)
        {
            contentPresenter.Content = child;
        }
        else if (parent is ContentControl contentControl)
        {
            contentControl.Content = child;
        }
    }

    public static bool RemoveFromParent(this UIElement child, out DependencyObject parent, out int? index)
    {
        parent = child.GetParent(true) ?? child.GetParent(false);

        if (parent == null)
        {
            index = null;
            return false;
        }

        if (parent is ItemsControl itemsControl)
        {
            if (itemsControl.Items.Contains(child))
            {
                index = itemsControl.Items.IndexOf(child);
                itemsControl.Items.Remove(child);
                return true;
            }
        }
        else if (parent is Panel panel)
        {
            if (panel.Children.Contains(child))
            {
                index = panel.Children.IndexOf(child);
                panel.Children.Remove(child);
                return true;
            }
        }
        else if (parent is Decorator decorator)
        {
            if (decorator.Child == child)
            {
                decorator.Child = null;
                index = null;
                return true;
            }
        }
        else if (parent is ContentPresenter contentPresenter)
        {
            if (contentPresenter.Content == child)
            {
                contentPresenter.Content = null;
                index = null;
                return true;
            }
        }
        else if (parent is ContentControl contentControl)
        {
            if (contentControl.Content == child)
            {
                contentControl.Content = null;
                index = null;
                return true;
            }
        }

        index = null;
        return false;
    }

    public static DependencyObject GetParent(this DependencyObject dependencyObject, bool isVisualTree)
    {
        if (isVisualTree)
        {
            return dependencyObject is Visual or Visual3D
                ? VisualTreeHelper.GetParent(dependencyObject)
                : null;
        }

        return LogicalTreeHelper.GetParent(dependencyObject);
    }
}