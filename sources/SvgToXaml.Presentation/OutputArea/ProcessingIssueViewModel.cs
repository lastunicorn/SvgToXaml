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

using DustInTheWind.SvgToXaml.Application.UseCases.SetInputSvg;

namespace DustInTheWind.SvgToXaml.Presentation.OutputArea;

public class ProcessingIssueViewModel
{
    public IssueType IssueType { get; }

    public string Message { get; }

    public ProcessingIssueViewModel(Issue processingIssue)
    {
        IssueType = processingIssue.Level switch
        {
            IssueLevel.Info => IssueType.Info,
            IssueLevel.Warning => IssueType.Waring,
            IssueLevel.Error => IssueType.Error,
            _ => IssueType.Info
        };

        Message = $"{processingIssue.Category}: {processingIssue.Message}";
    }
}