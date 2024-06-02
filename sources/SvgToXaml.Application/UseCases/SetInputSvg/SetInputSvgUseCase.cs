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

using DustInTheWind.SvgToXaml.Application.Processing;
using DustInTheWind.SvgToXaml.Infrastructure;
using MediatR;

namespace DustInTheWind.SvgToXaml.Application.UseCases.SetInputSvg;

internal class SetInputSvgUseCase : IRequestHandler<SetInputSvgRequest>
{
    private readonly EventBus eventBus;
    private readonly ApplicationState applicationState;

    public SetInputSvgUseCase(EventBus eventBus, ApplicationState applicationState)
    {
        this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        this.applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
    }

    public async Task Handle(SetInputSvgRequest request, CancellationToken cancellationToken)
    {
        applicationState.InputSvg = request.SvgText;

        PerformTransformation();
        await RaiseXamlEvent(cancellationToken);
    }

    private void PerformTransformation()
    {
        SvgToXamlTransformation svgToXamlTransformation = new()
        {
            Svg = applicationState.InputSvg,
            PerformOptimizations = applicationState.ProcessingOptions.OptimizeOutput,
            IgnoredNamespaces = applicationState.ProcessingOptions.IgnoredNamespaces.ToList()
        };

        svgToXamlTransformation.Execute();

        applicationState.OutputXaml = svgToXamlTransformation.Xaml;
        applicationState.ProcessingResults.LastProcessingIssues = svgToXamlTransformation.Issues;

        applicationState.ProcessingResults.DeserializationTime = svgToXamlTransformation.DeserializationTime;
        applicationState.ProcessingResults.ConversionTime = svgToXamlTransformation.ConversionTime;
        applicationState.ProcessingResults.OptimizationTime = svgToXamlTransformation.OptimizationTime;
        applicationState.ProcessingResults.SerializationTime = svgToXamlTransformation.SerializationTime;
        applicationState.ProcessingResults.AlterationTime = svgToXamlTransformation.AlterationTime;
    }

    private async Task RaiseXamlEvent(CancellationToken cancellationToken)
    {
        XamlTextChangedEvent xamlTextChangedEvent = new()
        {
            XamlText = applicationState.OutputXaml,

            Issues = applicationState.ProcessingResults.LastProcessingIssues.ToList(),
            InfoCount = applicationState.ProcessingResults.LastProcessingIssues.InfoCount,
            WarningCount = applicationState.ProcessingResults.LastProcessingIssues.WarningCount,
            ErrorCount = applicationState.ProcessingResults.LastProcessingIssues.ErrorCount,

            DeserializationTime = applicationState.ProcessingResults.DeserializationTime,
            ConversionTime = applicationState.ProcessingResults.ConversionTime,
            OptimizationTime = applicationState.ProcessingResults.OptimizationTime,
            SerializationTime = applicationState.ProcessingResults.SerializationTime,
            AlterationTime = applicationState.ProcessingResults.AlterationTime
        };

        await eventBus.Publish(xamlTextChangedEvent, cancellationToken);
    }
}