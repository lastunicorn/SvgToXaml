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

using System.Windows.Data;
using DustInTheWind.SvgToXaml.Application.UseCases.SetInputSvg;
using DustInTheWind.SvgToXaml.Infrastructure;

namespace DustInTheWind.SvgToXaml.Presentation.OutputArea;

public class OutputIssuesPanelViewModel : ViewModelBase
{
    private ListCollectionView errorItems;
    private bool displayErrors = true;
    private bool displayWarnings = true;
    private bool displayInfos;
    private int errorCount;
    private int warningCount;
    private int infoCount;

    public ListCollectionView ErrorItems
    {
        get => errorItems;
        private set
        {
            if (Equals(value, errorItems)) return;
            errorItems = value;
            OnPropertyChanged();
        }
    }

    public bool DisplayErrors
    {
        get => displayErrors;
        set
        {
            if (value == displayErrors) return;
            displayErrors = value;
            OnPropertyChanged();

            ErrorItems?.Refresh();
        }
    }

    public bool DisplayWarnings
    {
        get => displayWarnings;
        set
        {
            if (value == displayWarnings) return;
            displayWarnings = value;
            OnPropertyChanged();

            ErrorItems?.Refresh();
        }
    }

    public bool DisplayInfos
    {
        get => displayInfos;
        set
        {
            if (value == displayInfos) return;
            displayInfos = value;
            OnPropertyChanged();

            ErrorItems?.Refresh();
        }
    }

    public int ErrorCount
    {
        get => errorCount;
        set
        {
            if (value == errorCount) return;
            errorCount = value;
            OnPropertyChanged();
        }
    }

    public int WarningCount
    {
        get => warningCount;
        set
        {
            if (value == warningCount) return;
            warningCount = value;
            OnPropertyChanged();
        }
    }

    public int InfoCount
    {
        get => infoCount;
        set
        {
            if (value == infoCount) return;
            infoCount = value;
            OnPropertyChanged();
        }
    }

    public OutputIssuesPanelViewModel(EventBus eventBus)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

        eventBus.Subscribe<XamlTextChangedEvent>(XamlTextChangedEventHandler);
    }

    private Task XamlTextChangedEventHandler(XamlTextChangedEvent ev, CancellationToken cancellationToken)
    {
        List<ProcessingIssueViewModel> items = ev.Issues
            .Select(x => new ProcessingIssueViewModel(x))
            .ToList();

        ErrorItems = items.Count == 0
            ? null
            : new ListCollectionView(items)
            {
                Filter = ErrorItemsFilter
            };

        InfoCount = ev.InfoCount;
        WarningCount = ev.WarningCount;
        ErrorCount = ev.ErrorCount;

        return Task.CompletedTask;
    }

    private bool ErrorItemsFilter(object item)
    {
        if (item is ProcessingIssueViewModel processingIssueViewModel)
        {
            return processingIssueViewModel.IssueType switch
            {
                IssueType.Error => DisplayErrors,
                IssueType.Waring => DisplayWarnings,
                IssueType.Info => DisplayInfos,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return false;
    }
}