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

using System.Collections.ObjectModel;

namespace DustInTheWind.SvgToXaml.Application.Processing;

public class ProcessingIssueCollection : Collection<ProcessingIssue>
{
    public int InfoCount { get; private set; }

    public int WarningCount { get; private set; }

    public int ErrorCount { get; private set; }

    protected override void InsertItem(int index, ProcessingIssue item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        IncrementCount(item.Level);

        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, ProcessingIssue item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        IncrementCount(item.Level);

        base.SetItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
        ProcessingIssue processingIssue = Items[index];
        base.RemoveItem(index);
        DecrementCount(processingIssue.Level);
    }

    protected override void ClearItems()
    {
        base.ClearItems();

        InfoCount = 0;
        WarningCount = 0;
        ErrorCount = 0;
    }

    private void IncrementCount(ProcessingIssueLevel processingIssueLevel)
    {
        switch (processingIssueLevel)
        {
            case ProcessingIssueLevel.Info:
                InfoCount++;
                break;

            case ProcessingIssueLevel.Warning:
                WarningCount++;
                break;

            case ProcessingIssueLevel.Error:
                ErrorCount++;
                break;
        }
    }

    private void DecrementCount(ProcessingIssueLevel processingIssueLevel)
    {
        switch (processingIssueLevel)
        {
            case ProcessingIssueLevel.Info:
                InfoCount--;
                break;

            case ProcessingIssueLevel.Warning:
                WarningCount--;
                break;

            case ProcessingIssueLevel.Error:
                ErrorCount--;
                break;
        }
    }

    public void AddRange(IEnumerable<ProcessingIssue> issues)
    {
        IEnumerable<ProcessingIssue> notNullItems = issues.Where(x => x != null);

        foreach (ProcessingIssue processingIssue in notNullItems)
            Add(processingIssue);
    }
}