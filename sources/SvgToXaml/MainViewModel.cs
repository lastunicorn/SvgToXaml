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
using System.Windows.Threading;
using DustInTheWind.SvgToXaml.Application.OpenFile;
using DustInTheWind.SvgToXaml.Application.Transform;
using DustInTheWind.SvgToXaml.Infrastructure;

namespace DustInTheWind.SvgToXaml;

public class MainViewModel : ViewModelBase
{
    private readonly IRequestBus requestBus;
    private string svgText;
    private string xamlText;
    private UIElement xamlObject;
    private string svgFilePath;
    private readonly Dispatcher dispatcher;
    private string errors;
    private bool shouldOptimize;

    public string SvgFilePath
    {
        get => svgFilePath;
        private set
        {
            if (value == svgFilePath) return;
            svgFilePath = value;
            OnPropertyChanged();
        }
    }

    public string SvgText
    {
        get => svgText;
        set
        {
            if (value == svgText) return;
            svgText = value;
            OnPropertyChanged();

            _ = TransformSvgToXaml();
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

    public string Errors
    {
        get => errors;
        set
        {
            if (value == errors) return;
            errors = value;
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

            _ = TransformSvgToXaml();
        }
    }

    public OpenFileCommand OpenFileCommand { get; }

    public CopyToClipboardCommand CopyToClipboardCommand { get; }

    public MainViewModel(IRequestBus requestBus, EventBus eventBus, OpenFileCommand openFileCommand, CopyToClipboardCommand copyToClipboardCommand)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
        this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

        OpenFileCommand = openFileCommand;
        CopyToClipboardCommand = copyToClipboardCommand;
        shouldOptimize = true;

        eventBus.Subscribe<SvgTextChangingEvent>(SvgTextChangingEventHandler);
        eventBus.Subscribe<SvgTextChangedEvent>(SvgTextChangedEventHandler);
        eventBus.Subscribe<XamlTextChangedEvent>(XamlTextChangedEventHandler);

        dispatcher = Dispatcher.CurrentDispatcher;
    }

    private Task SvgTextChangingEventHandler(SvgTextChangingEvent ev, CancellationToken cancellationToken)
    {
        dispatcher.Invoke(() =>
        {
            SvgFilePath = ev.FilePath;
            SvgText = string.Empty;
        });

        return Task.CompletedTask;
    }

    private Task SvgTextChangedEventHandler(SvgTextChangedEvent ev, CancellationToken cancellationToken)
    {
        dispatcher.Invoke(() =>
        {
            SvgFilePath = ev.FilePath;
            SvgText = ev.SvgText;
        });

        return Task.CompletedTask;
    }

    private Task XamlTextChangedEventHandler(XamlTextChangedEvent ev, CancellationToken cancellationToken)
    {
        XamlText = ev.XamlText;

        XamlObject = ev.XamlText == null
            ? null
            : ExtractUiElement(ev.XamlText);

        IEnumerable<ErrorInfo> logItems = ev.Errors
            .Concat(ev.Warning)
            .Concat(ev.Info);

        Errors = logItems.Any()
            ? string.Join(Environment.NewLine, logItems.Select(x => x.Message))
            : null;

        return Task.CompletedTask;
    }

    private UIElement ExtractUiElement(string xamlText)
    {
        using Stream stream = ToStream(xamlText);
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

    private async Task TransformSvgToXaml()
    {
        TransformRequest request = new()
        {
            SvgText = svgText,
            ShouldOptimize = ShouldOptimize
        };

        await requestBus.Send(request, CancellationToken.None).ConfigureAwait(false);
    }

    public static Stream ToStream(string s)
    {
        MemoryStream stream = new();
        StreamWriter writer = new(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}