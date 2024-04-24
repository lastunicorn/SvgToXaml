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

using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;
using DustInTheWind.SvgToXaml.Conversion;
using DustInTheWind.SvgToXaml.SvgSerialization;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests;

public class SvgFileTestsBase
{
    protected void ConvertSvgFile(string resourceFileName, Action<Canvas> callBack = null)
    {
        Type callerType = GetCallerType();
        DeserializationResult deserializationResult = ParseSvgFileInternal(resourceFileName, callerType);

        StaEnvironment.Run(ExecutionErrorBehavior.ThrowException, () =>
        {
            ConversionContext conversionContext = new();
            SvgConversion svgConversion = new(deserializationResult.Svg, conversionContext);
            Canvas canvas = svgConversion.Execute();

            callBack?.Invoke(canvas);
        });
    }

    protected void ParseSvgFile(string resourceFileName, Action<DustInTheWind.SvgToXaml.SvgModel.Svg> callBack = null)
    {
        Type callerType = GetCallerType();
        DeserializationResult deserializationResult = ParseSvgFileInternal(resourceFileName, callerType);
        callBack?.Invoke(deserializationResult.Svg);
    }

    protected void ParseSvgFile(string resourceFileName, Action<DeserializationResult> callBack = null)
    {
        Type callerType = GetCallerType();
        DeserializationResult deserializationResult = ParseSvgFileInternal(resourceFileName, callerType);
        callBack?.Invoke(deserializationResult);
    }

    private static Type GetCallerType()
    {
        StackFrame stackFrame = new(2, false);
        MethodBase caller = stackFrame.GetMethod();

        return caller.DeclaringType;
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