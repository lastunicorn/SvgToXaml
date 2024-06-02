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

using DustInTheWind.SvgToXaml.Application.UseCases.GetOptionsState;
using DustInTheWind.SvgToXaml.Application.UseCases.SetApplyOptimizations;
using DustInTheWind.SvgToXaml.Application.UseCases.SetIgnoredNamespaces;
using DustInTheWind.SvgToXaml.Infrastructure;

namespace DustInTheWind.SvgToXaml.Presentation.OutputArea;

public class OptionsPanelViewModel : ViewModelBase
{
    private bool isEnabled;
    private readonly IRequestBus requestBus;
    private bool applyOptimizations;
    private string ignoredNamespaces;

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

    public string IgnoredNamespaces
    {
        get => ignoredNamespaces;
        set
        {
            if (value == ignoredNamespaces) return;
            ignoredNamespaces = value;
            OnPropertyChanged();

            if (!Initializing)
                _ = SetIgnoredNamespaces();
        }
    }

    public OptionsPanelViewModel(IRequestBus requestBus, EventBus eventBus)
    {
        if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));
        this.requestBus = requestBus ?? throw new ArgumentNullException(nameof(requestBus));

        eventBus.Subscribe<ApplyOptimizationsChangeEvent>(HandleApplyOptimizationsChangeEvent);

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

    private async Task Initialize()
    {
        GetOptionsStateRequest request = new();

        GetOptionsStateResponse response = await requestBus.Send<GetOptionsStateRequest, GetOptionsStateResponse>(request, CancellationToken.None)
            .ConfigureAwait(false);

        ApplyOptimizations = response.ApplyOptimizations;
        IgnoredNamespaces = string.Join(Environment.NewLine, response.IgnoredNamespaces);

        IsEnabled = true;
    }

    private async Task SetApplyOptimizations()
    {
        SetApplyOptimizationsRequest request = new()
        {
            ApplyOptimizations = ApplyOptimizations
        };

        await requestBus.Send(request, CancellationToken.None).ConfigureAwait(false);
    }

    private async Task SetIgnoredNamespaces()
    {
        SetIgnoredNamespacesRequest request = new()
        {
            IgnoredNamespaces = IgnoredNamespaces.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        };

        await requestBus.Send(request, CancellationToken.None).ConfigureAwait(false);
    }
}