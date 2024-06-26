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

using DustInTheWind.SvgToXaml.Application.UseCases.GetOutputInitialization;
using DustInTheWind.SvgToXaml.Application.UseCases.SetApplyOptimizations;
using DustInTheWind.SvgToXaml.Application.UseCases.SetInputSvg;
using DustInTheWind.SvgToXaml.Infrastructure;

namespace DustInTheWind.SvgToXaml.Presentation.OutputArea;

public class OutputPanelViewModel : ViewModelBase
{
    private readonly IRequestBus requestBus;
    private bool applyOptimizations;
    private bool isEnabled;
    private bool isErrorPanelVisible;

    public bool IsEnabled
    {
        get => isEnabled;
        private set
        {
            if (value == isEnabled) return;
            isEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool ApplyOptimizations
    {
        get => applyOptimizations;
        set
        {
            if (value == applyOptimizations) return;
            applyOptimizations = value;
            OnPropertyChanged();

            if (!Initializing)
                _ = SetApplyOptimizations();
        }
    }

    public bool IsErrorPanelVisible
    {
        get => isErrorPanelVisible;
        set
        {
            if (value == isErrorPanelVisible) return;
            isErrorPanelVisible = value;
            OnPropertyChanged();
        }
    }

    public OutputXamlPanelViewModel OutputXamlPanelViewModel { get; }

    public OutputImagePanelViewModel OutputImagePanelViewModel { get; }

    public OptionsPanelViewModel OptionsPanelViewModel { get; }

    public OutputIssuesPanelViewModel OutputIssuesPanelViewModel { get; }

    public OutputPanelViewModel(IRequestBus requestBus, EventBus eventBus, OutputXamlPanelViewModel outputXamlPanelViewModel,
        OutputImagePanelViewModel outputImagePanelViewModel, OptionsPanelViewModel optionsPanelViewModel,
        OutputIssuesPanelViewModel outputIssuesPanelViewModel)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
        this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

        OutputXamlPanelViewModel = outputXamlPanelViewModel ?? throw new ArgumentNullException(nameof(outputXamlPanelViewModel));
        OutputImagePanelViewModel = outputImagePanelViewModel ?? throw new ArgumentNullException(nameof(outputImagePanelViewModel));
        OptionsPanelViewModel = optionsPanelViewModel ?? throw new ArgumentNullException(nameof(optionsPanelViewModel));
        OutputIssuesPanelViewModel = outputIssuesPanelViewModel ?? throw new ArgumentNullException(nameof(outputIssuesPanelViewModel));

        eventBus.Subscribe<ApplyOptimizationsChangeEvent>(HandleApplyOptimizationsChangeEvent);
        eventBus.Subscribe<XamlTextChangedEvent>(XamlTextChangedEventHandler);

        _ = Initialize();
    }

    private Task HandleApplyOptimizationsChangeEvent(ApplyOptimizationsChangeEvent ev, CancellationToken cancellationToken)
    {
        Initialize(() =>
        {
            ApplyOptimizations = ev.ApplyOptimizations;
        });

        return Task.CompletedTask;
    }

    private Task XamlTextChangedEventHandler(XamlTextChangedEvent ev, CancellationToken cancellationToken)
    {
        IsErrorPanelVisible = ev.Issues.Count > 0;

        return Task.CompletedTask;
    }

    private async Task SetApplyOptimizations()
    {
        SetApplyOptimizationsRequest request = new()
        {
            ApplyOptimizations = ApplyOptimizations
        };

        await requestBus.Send(request, CancellationToken.None).ConfigureAwait(false);
    }

    private async Task Initialize()
    {
        GetOutputInitializationRequest request = new();

        GetOutputInitializationResponse response = await requestBus.Send<GetOutputInitializationRequest, GetOutputInitializationResponse>(request, CancellationToken.None)
            .ConfigureAwait(false);

        ApplyOptimizations = response.ShouldOptimizeXaml;

        IsEnabled = true;
    }
}