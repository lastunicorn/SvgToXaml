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
using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.Tests.Utils;

namespace DustInTheWind.SvgToXaml.Tests.Conversion;

public class SvgFileTestsBase
{
    protected void TestConvertSvgFile(string resourceFileName, Action<Canvas> callBack = null)
    {
        Type callerType = GetCallerType();

        string svg = TestResources.ReadTextFile(resourceFileName, callerType);
        SvgDocument svgDocument = SvgDocument.Parse(svg);

        StaEnvironment.Run(ExecutionErrorBehavior.ThrowException, () =>
        {
            SvgConversion svgConversion = new(svgDocument.Content);
            Canvas canvas = svgConversion.Execute();

            callBack?.Invoke(canvas);
        });
    }

    private static Type GetCallerType()
    {
        StackFrame stackFrame = new(2, false);
        MethodBase caller = stackFrame.GetMethod();

        return caller.DeclaringType;
    }
}