// Country Flags
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

namespace DustInTheWind.SvgToXaml.Tests.Utils;

public static class StaEnvironment
{
    public static void Run(Action action)
    {
        Run(ExecutionErrorBehavior.ThrowException, action);
    }

    public static void Run(ExecutionErrorBehavior executionErrorBehavior, Action action)
    {
        Exception exception = null;

        Thread thread = new(() =>
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;

        thread.Start();
        thread.Join();

        if (exception != null)
        {
            switch (executionErrorBehavior)
            {
                default:
                case ExecutionErrorBehavior.ThrowException:
                    throw new StaEnvironmentException(exception);

                case ExecutionErrorBehavior.RethrowOriginalException:
                    throw exception;

                case ExecutionErrorBehavior.SwallowException:
                    break;
            }
        }
    }
}