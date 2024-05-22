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
using System.Windows.Markup;
using System.Windows.Shapes;

namespace DustInTheWind.SvgToXaml.Application.Transform;

internal class CanvasOptimization
{
    public Canvas Canvas { get; }

    public List<ErrorInfo> Errors { get; } = new();

    public List<ErrorInfo> Infos { get; } = new();

    public CanvasOptimization(Canvas canvas)
    {
        Canvas = canvas;
    }

    public void Execute()
    {
        if (Canvas == null)
            return;

        Optimize(Canvas);
    }

    private void Optimize(Canvas canvas)
    {
        int i = 0;

        while (i < canvas.Children.Count)
        {
            UIElement child = canvas.Children[i];

            bool isCompletelyTransparent = child.Opacity == 0;
            if (isCompletelyTransparent)
            {
                bool success = RemoveElement(child, "Completely transparent.");
                if (success)
                    continue;
            }

            if (child is Canvas childCanvas)
            {
                Optimize(childCanvas);

                bool canvasHasChildren = childCanvas.Children.Count > 0;
                if (!canvasHasChildren)
                {
                    bool success = RemoveElement(childCanvas, "Empty canvas.");
                    if (success)
                        continue;
                }

                bool isCanvasUseful = IsCanvasUseful(childCanvas);
                if (!isCanvasUseful)
                {
                    bool success = ExtractChildrenFrom(childCanvas, out int childCount);
                    if (success)
                    {
                        i += childCount;
                        continue;
                    }
                }
            }
            else if (child is Rectangle rectangle)
            {
                bool rectangleIsZeroSize = rectangle.Width == 0 || rectangle.Height == 0;

                if (rectangleIsZeroSize)
                {
                    bool success = RemoveElement(rectangle, "Zero-size rectangle.");
                    if (success)
                        continue;
                }
            }
            else if (child is Ellipse ellipse)
            {
                bool rectangleIsZeroSize = ellipse.Width == 0 || ellipse.Height == 0;

                if (rectangleIsZeroSize)
                {
                    bool success = RemoveElement(ellipse, "Zero-size ellipse.");
                    if (success)
                        continue;
                }
            }
            else if (child is TextBlock textBlock)
            {
                bool textHasContent = !string.IsNullOrEmpty(textBlock.Text);

                if (!textHasContent)
                {
                    bool success = RemoveElement(textBlock, "Empty text box.");
                    if (success)
                        continue;
                }
            }

            i++;
        }
    }

    private bool ExtractChildrenFrom(Panel childCanvas, out int childCount)
    {
        List<UIElement> grandChildren = RemoveAllChildren(childCanvas);
        bool success = childCanvas.RemoveFromParent(out DependencyObject parent, out int? index);

        if (!success)
        {
            childCount = 0;

            ErrorInfo errorInfo = new()
            {
                Message = $"Optimization: Failed moving {grandChildren.Count} children outside of container. Container could not be removed - {childCanvas.GetType().Name}."
            };
            Errors.Add(errorInfo);

            return false;
        }
        else
        {
            grandChildren.AddToParent(parent, index);

            childCount = grandChildren.Count;

            ErrorInfo errorInfo = new()
            {
                Message = $"Optimization: {grandChildren.Count} children moved outside of container. Container removed - {childCanvas.GetType().Name}."
            };
            Infos.Add(errorInfo);

            return true;
        }
    }

    private static List<UIElement> RemoveAllChildren(Panel panel)
    {
        List<UIElement> children = new();

        while (panel.Children.Count > 0)
        {
            UIElement grandChild = panel.Children[0];

            grandChild.RemoveFromParent(out DependencyObject _, out int? _);
            children.Add(grandChild);
        }

        return children;
    }

    private bool RemoveElement(UIElement element, string reason)
    {
        bool success = element.RemoveFromParent(out DependencyObject _, out int? _);

        ErrorInfo errorInfo = new()
        {
            Message = $"Optimization: UIElement removed - {element.GetType().Name}. Reason: {reason}"
        };
        Infos.Add(errorInfo);

        return success;
    }

    private static bool IsCanvasUseful(Canvas canvas)
    {
        bool containsTransformations = canvas.RenderTransform != null && canvas.RenderTransform != System.Windows.Media.Transform.Identity;
        if (containsTransformations)
            return true;

        bool containsLanguage = canvas.Language != null && canvas.Language != XmlLanguage.Empty && canvas.Language != XmlLanguage.GetLanguage("en-US");
        if (containsLanguage)
            return true;

        bool containsClip = canvas.Clip != null;
        if (containsClip)
            return true;
        
        bool isTransparent = Math.Abs(canvas.Opacity - 1) > double.Epsilon;
        if (isTransparent)
            return true;

        return false;
    }
}