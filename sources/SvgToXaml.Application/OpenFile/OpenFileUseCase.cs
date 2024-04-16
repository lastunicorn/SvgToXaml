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

using DustInTheWind.SvgToXaml.Infrastructure;
using DustInTheWind.SvgToXaml.Ports.FileAccess;
using DustInTheWind.SvgToXaml.Ports.UserAccess;
using MediatR;

namespace DustInTheWind.SvgToXaml.Application.OpenFile;

internal class OpenFileUseCase : IRequestHandler<OpenFileRequest>
{
    private readonly IUserInteractions userInteractions;
    private readonly IFileSystem fileSystem;
    private readonly EventBus eventBus;

    public OpenFileUseCase(IUserInteractions userInteractions, IFileSystem fileSystem, EventBus eventBus)
    {
        this.userInteractions = userInteractions ?? throw new ArgumentNullException(nameof(userInteractions));
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    public async Task Handle(OpenFileRequest request, CancellationToken cancellationToken)
    {
        string svgFilePath = userInteractions.AskSvgFileToOpen();

        if (svgFilePath == null)
            return;

        await RaiseSvgTextChangingEvent(svgFilePath, cancellationToken);

        string svgText = OpenTextFile(svgFilePath);

        await RaiseSvgTextChangedEvent(svgFilePath, svgText, cancellationToken);
    }

    private async Task RaiseSvgTextChangingEvent(string svgFilePath, CancellationToken cancellationToken)
    {
        SvgTextChangingEvent svgTextChangedEvent = new()
        {
            FilePath = svgFilePath
        };
        await eventBus.Publish(svgTextChangedEvent, cancellationToken);
    }

    private string OpenTextFile(string svgFilePath)
    {
        try
        {
            return fileSystem.GetTextContent(svgFilePath);
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    private async Task RaiseSvgTextChangedEvent(string svgFilePath, string svgText, CancellationToken cancellationToken)
    {
        SvgTextChangedEvent svgTextChangedEvent = new()
        {
            FilePath = svgFilePath,
            SvgText = svgText
        };
        await eventBus.Publish(svgTextChangedEvent, cancellationToken);
    }
}