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
using DustInTheWind.SvgToXaml.Application.UseCases.SetInputSvg;
using DustInTheWind.SvgToXaml.Infrastructure;

namespace DustInTheWind.SvgToXaml.Presentation.OutputArea;

public class OutputImagePanelViewModel : ViewModelBase
{
    private UIElement xamlObject;

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

    public OutputImagePanelViewModel(EventBus eventBus)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

        eventBus.Subscribe<XamlTextChangedEvent>(XamlTextChangedEventHandler);
    }

    private Task XamlTextChangedEventHandler(XamlTextChangedEvent ev, CancellationToken cancellationToken)
    {
        XamlObject = ev.XamlText == null
            ? null
            : ExtractUiElement(ev.XamlText);

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
}