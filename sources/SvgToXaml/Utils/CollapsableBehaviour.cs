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

namespace DustInTheWind.SvgToXaml.Utils;

public static class CollapsableBehaviour
{
    private static readonly Dictionary<DependencyObject, GridLength> OldValues = new();

    public static readonly DependencyProperty EnableProperty = DependencyProperty.RegisterAttached(
        "Enable",
        typeof(GridSplitter),
        typeof(CollapsableBehaviour),
        new UIPropertyMetadata(null, OnEnableChanged));

    public static GridSplitter GetEnable(DependencyObject obj)
    {
        return (GridSplitter)obj.GetValue(EnableProperty);
    }

    public static void SetEnable(DependencyObject obj, GridSplitter value)
    {
        obj.SetValue(EnableProperty, value);
    }

    private static void OnEnableChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        if (obj is Grid grid)
        {
            if (e.NewValue is GridSplitter gridSplitter)
            {
                gridSplitter.IsVisibleChanged += (sender, e2) =>
                {
                    UpdateGrid(grid, (GridSplitter)sender, (bool)e2.NewValue, (bool)e2.OldValue);
                };
            }
        }
    }

    private static void UpdateGrid(Grid grid, GridSplitter gridSplitter, bool newValue, bool oldValue)
    {
        if (newValue)
        {
            // We're visible again
            switch (gridSplitter.ResizeDirection)
            {
                case GridResizeDirection.Auto:
                    bool isRowCollapsing = double.IsNaN(gridSplitter.Width) || gridSplitter.Width >= gridSplitter.Height;

                    if (isRowCollapsing)
                        ShowRows(grid, gridSplitter);
                    else
                        ShowColumns(grid, gridSplitter);
                    break;

                case GridResizeDirection.Columns:
                    ShowColumns(grid, gridSplitter);
                    break;

                case GridResizeDirection.Rows:
                    ShowRows(grid, gridSplitter);

                    break;
            }
        }
        else
        {
            // We're being hidden
            switch (gridSplitter.ResizeDirection)
            {
                case GridResizeDirection.Auto:
                    bool isRowCollapsing = double.IsNaN(gridSplitter.Width) || gridSplitter.Width >= gridSplitter.Height;

                    if (isRowCollapsing)
                        HideRows(grid, gridSplitter);
                    else
                        HideColumns(grid, gridSplitter);
                    break;

                case GridResizeDirection.Columns:
                    HideColumns(grid, gridSplitter);
                    break;

                case GridResizeDirection.Rows:
                    HideRows(grid, gridSplitter);
                    break;
            }
        }
    }

    private static void ShowColumns(Grid grid, GridSplitter gridSplitter)
    {
    }

    private static void ShowRows(Grid grid, GridSplitter gridSplitter)
    {
        int rowIndex = (int)gridSplitter.GetValue(Grid.RowProperty);
        int previousRowIndex = rowIndex - 1;
        RowDefinition previousRow = grid.RowDefinitions.ElementAt(previousRowIndex);

        int nextRowIndex = rowIndex + 1;
        RowDefinition currentRow = grid.RowDefinitions.ElementAt(nextRowIndex);

        if (OldValues.TryGetValue(previousRow, out GridLength previousRowHeight))
        {
            if (OldValues.TryGetValue(currentRow, out GridLength currentRowHeight))
            {
                previousRow.Height = previousRowHeight;
                currentRow.Height = currentRowHeight;
            }
        }
    }

    private static void HideColumns(Grid grid, GridSplitter gridSplitter)
    {
    }

    private static void HideRows(Grid grid, GridSplitter gridSplitter)
    {
        int rowIndex = (int)gridSplitter.GetValue(Grid.RowProperty);

        int previousRowIndex = rowIndex - 1;
        RowDefinition previousRow = grid.RowDefinitions.ElementAt(previousRowIndex);

        OldValues[previousRow] = previousRow.Height;
        previousRow.Height = new GridLength(1, GridUnitType.Star);

        int nextRowIndex = rowIndex + 1;
        RowDefinition currentRow = grid.RowDefinitions.ElementAt(nextRowIndex);

        OldValues[currentRow] = currentRow.Height;
        currentRow.Height = new GridLength(0);
    }
}