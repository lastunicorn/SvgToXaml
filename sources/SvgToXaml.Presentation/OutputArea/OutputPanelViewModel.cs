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

using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using DustInTheWind.SvgToXaml.Application.GetOutputInitialization;
using DustInTheWind.SvgToXaml.Application.SetInputSvg;
using DustInTheWind.SvgToXaml.Application.SetOptimizeFlag;
using DustInTheWind.SvgToXaml.Infrastructure;

namespace DustInTheWind.SvgToXaml.Presentation.OutputArea;

public class OutputPanelViewModel : ViewModelBase
{
    private readonly IRequestBus requestBus;
    private string xamlText;
    private UIElement xamlObject;
    private ListCollectionView errorItems;
    private bool shouldOptimize;
    private bool displayErrors = true;
    private bool displayWarnings = true;
    private bool displayInfos;
    private int errorCount;
    private int warningCount;
    private int infoCount;
    private bool isInitialized;

    public bool IsInitialized
    {
        get => isInitialized;
        private set
        {
            if (value == isInitialized) return;
            isInitialized = value;
            OnPropertyChanged();
        }
    }

    public string XamlText
    {
        get => xamlText;
        private set
        {
            if (value == xamlText) return;
            xamlText = value;
            OnPropertyChanged();
        }
    }

    public UIElement XamlObject
    {
        get => xamlObject;
        set
        {
            if (Equals(value, xamlObject)) return;
            xamlObject = value;
            OnPropertyChanged();
        }
    }

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

    public bool ShouldOptimize
    {
        get => shouldOptimize;
        set
        {
            if (value == shouldOptimize) return;
            shouldOptimize = value;
            OnPropertyChanged();

            _ = SetOptimizeFlag();
        }
    }

    public CopyToClipboardCommand CopyToClipboardCommand { get; }

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

    public OutputPanelViewModel(IRequestBus requestBus, EventBus eventBus, CopyToClipboardCommand copyToClipboardCommand)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
        this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

        CopyToClipboardCommand = copyToClipboardCommand;
        shouldOptimize = true;

        eventBus.Subscribe<XamlTextChangedEvent>(XamlTextChangedEventHandler);

        _ = Initialize();
    }

    private Task XamlTextChangedEventHandler(XamlTextChangedEvent ev, CancellationToken cancellationToken)
    {
        XamlText = ev.XamlText;

        XamlObject = ev.XamlText == null
            ? null
            : ExtractUiElement(ev.XamlText);

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

    private UIElement ExtractUiElement(string xamlText)
    {
        using Stream stream = xamlText.ToStream();
        object loadedObject = XamlReader.Load(stream);

        if (loadedObject is ResourceDictionary { Count: > 0 } resourceDictionary)
        {
            IDictionaryEnumerator enumerator = resourceDictionary.GetEnumerator();

            if (enumerator.MoveNext())
            {
                DictionaryEntry entry = (DictionaryEntry)enumerator.Current;
                return entry.Value as UIElement;
            }
        }

        return null;
    }

    private async Task SetOptimizeFlag()
    {
        SetOptimizeFlagRequest request = new()
        {
            OptimizeOutputXaml = ShouldOptimize
        };

        await requestBus.Send(request, CancellationToken.None).ConfigureAwait(false);
    }

    private async Task Initialize()
    {
        GetOutputInitializationRequest request = new();

        GetOutputInitializationResponse response = await requestBus.Send<GetOutputInitializationRequest, GetOutputInitializationResponse>(request, CancellationToken.None)
            .ConfigureAwait(false);

        ShouldOptimize = response.ShouldOptimizeXaml;

        IsInitialized = true;
    }
}