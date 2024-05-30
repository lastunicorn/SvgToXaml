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
using System.Windows.Markup;
using DustInTheWind.SvgToXaml.Application.SetInputSvg;
using DustInTheWind.SvgToXaml.Application.SetOptimizeFlag;
using DustInTheWind.SvgToXaml.Infrastructure;
using DustInTheWind.SvgToXaml.Utils;

namespace DustInTheWind.SvgToXaml;

public class OutputPanelViewModel : ViewModelBase
{
    private readonly IRequestBus requestBus;
    private string xamlText;
    private UIElement xamlObject;
    private List<ProcessingIssueViewModel> errorItems;
    private bool shouldOptimize;

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

    public List<ProcessingIssueViewModel> ErrorItems
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

            _ = SetShouldOptimizeFlag();
        }
    }

    public CopyToClipboardCommand CopyToClipboardCommand { get; }

    public OutputPanelViewModel(IRequestBus requestBus, EventBus eventBus, CopyToClipboardCommand copyToClipboardCommand)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
        this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

        CopyToClipboardCommand = copyToClipboardCommand;
        shouldOptimize = true;

        eventBus.Subscribe<XamlTextChangedEvent>(XamlTextChangedEventHandler);
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
            : items;

        return Task.CompletedTask;
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

    private async Task SetShouldOptimizeFlag()
    {
        SetOptimizeFlagRequest request = new()
        {
            OptimizeOutputXaml = ShouldOptimize
        };

        await requestBus.Send(request, CancellationToken.None).ConfigureAwait(false);
    }
}