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

namespace DustInTheWind.SvgDotnet.Serialization;

public class DeserializationIssueCollection : Collection<DeserializationIssue>
{
    protected override void InsertItem(int index, DeserializationIssue item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, DeserializationIssue item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        base.SetItem(index, item);
    }

    public void AddInfo(string path, string message)
    {
        DeserializationIssue conversionIssue = new()
        {
            Level = DeserializationIssueLevel.Info,
            Path = path,
            Message = message
        };
        Items.Add(conversionIssue);
    }

    public void AddWarning(string path, string message)
    {
        DeserializationIssue conversionIssue = new()
        {
            Level = DeserializationIssueLevel.Warning,
            Path = path,
            Message = message
        };
        Items.Add(conversionIssue);
    }

    public void AddError(string path, string message)
    {
        DeserializationIssue conversionIssue = new()
        {
            Level = DeserializationIssueLevel.Error,
            Path = path,
            Message = message
        };
        Items.Add(conversionIssue);
    }
}