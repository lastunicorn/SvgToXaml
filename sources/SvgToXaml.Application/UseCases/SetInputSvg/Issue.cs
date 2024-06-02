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

using DustInTheWind.SvgToXaml.Domain;

namespace DustInTheWind.SvgToXaml.Application.UseCases.SetInputSvg;

public class Issue
{
    public string Category { get; init; }

    public IssueLevel Level { get; init; }

    public string Message { get; init; }

    internal Issue(ProcessingIssue processingIssue)
    {
        if (processingIssue == null)
            return;

        Category = processingIssue.Category;
        Level = processingIssue.Level switch
        {
            ProcessingIssueLevel.Info => IssueLevel.Info,
            ProcessingIssueLevel.Warning => IssueLevel.Warning,
            ProcessingIssueLevel.Error => IssueLevel.Error,
            _ => throw new ArgumentOutOfRangeException()
        };
        Message = processingIssue.Message;
    }
}