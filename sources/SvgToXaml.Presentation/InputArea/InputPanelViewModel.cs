﻿// SvgToXaml
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

using System.Windows.Threading;
using DustInTheWind.SvgToXaml.Application.UseCases.OpenFile;
using DustInTheWind.SvgToXaml.Application.UseCases.SetInputSvg;
using DustInTheWind.SvgToXaml.Infrastructure;

namespace DustInTheWind.SvgToXaml.Presentation.InputArea;

public class InputPanelViewModel : ViewModelBase
{
    private readonly IRequestBus requestBus;
    private string svgText;
    private string svgFilePath;
    private readonly Dispatcher dispatcher;

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

    public OpenFileCommand OpenFileCommand { get; }

    public InputPanelViewModel(IRequestBus requestBus, EventBus eventBus, OpenFileCommand openFileCommand)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
        this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

        OpenFileCommand = openFileCommand;

        eventBus.Subscribe<SvgTextChangingEvent>(SvgTextChangingEventHandler);
        eventBus.Subscribe<SvgTextChangedEvent>(SvgTextChangedEventHandler);

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

    private async Task TransformSvgToXaml()
    {
        SetInputSvgRequest request = new()
        {
            SvgText = svgText
        };

        await requestBus.Send(request, CancellationToken.None).ConfigureAwait(false);
    }
}