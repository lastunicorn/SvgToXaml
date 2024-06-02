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

using DustInTheWind.SvgDotnet.Serialization;
using DustInTheWind.SvgToXaml.Conversion;

namespace DustInTheWind.SvgToXaml.Application.Processing;

public class ProcessingIssue
{
    public string Category { get; init; }

    public ProcessingIssueLevel Level { get; init; }

    public string Message { get; init; }

    internal ProcessingIssue()
    {
    }

    internal ProcessingIssue(ConversionIssue conversionIssue)
    {
        Category = "Conversion";

        Level = conversionIssue.Level switch
        {
            ConversionIssueLevel.Info => ProcessingIssueLevel.Info,
            ConversionIssueLevel.Warning => ProcessingIssueLevel.Warning,
            ConversionIssueLevel.Error => ProcessingIssueLevel.Error,
            _ => ProcessingIssueLevel.Info
        };

        Message = conversionIssue.Message;
    }

    internal ProcessingIssue(DeserializationIssue deserializationIssue)
    {
        Category = "SVG Deserialization";

        Level = deserializationIssue.Level switch
        {
            DeserializationIssueLevel.Info => ProcessingIssueLevel.Info,
            DeserializationIssueLevel.Warning => ProcessingIssueLevel.Warning,
            DeserializationIssueLevel.Error => ProcessingIssueLevel.Error,
            _ => ProcessingIssueLevel.Info
        };

        Message = $"{deserializationIssue.Path} : {deserializationIssue.Message}";
    }
}