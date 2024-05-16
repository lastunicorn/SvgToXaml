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
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgDotnet.Tests;

public class SvgFileTestsBase
{
    protected void ParseSvgFile(string resourceFileName, Action<Svg> callBack = null)
    {
        Type callerType = GetType();
        DeserializationResult deserializationResult = ParseSvgFileInternal(resourceFileName, callerType);
        callBack?.Invoke(deserializationResult.Svg);
    }

    protected void ParseSvgFile(string resourceFileName, Action<DeserializationResult> callBack = null)
    {
        Type callerType = GetType();
        DeserializationResult deserializationResult = ParseSvgFileInternal(resourceFileName, callerType);
        callBack?.Invoke(deserializationResult);
    }

    private static DeserializationResult ParseSvgFileInternal(string resourceFileName, Type callerType)
    {
        string fullResourceFileName = ComputeFullResourceFileName(resourceFileName, callerType);

        string svgText = TestResources.ReadTextFile(fullResourceFileName, callerType.Assembly);
        SvgSerializer svgSerializer = new();
        return svgSerializer.Deserialize(svgText);
    }

    private static string ComputeFullResourceFileName(string resourceFileName, Type callerType)
    {
        string callerNamespace = callerType.Namespace;
        return $"{callerNamespace}.{callerType.Name}.Resources.{resourceFileName}";
    }
}