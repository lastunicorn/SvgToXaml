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

using System.Windows.Media;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace DustInTheWind.SvgToXaml.Tests.Utils.Assertions;

public class BrushAssertions : ReferenceTypeAssertions<Brush, BrushAssertions>
{
    protected override string Identifier => "Brush";

    public BrushAssertions(Brush instance)
        : base(instance)
    {
    }

    public AndConstraint<BrushAssertions> Be(Color expectedColor, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(x =>
            {
                SolidColorBrush expectedBrush = new(expectedColor);

                if (x is not SolidColorBrush actualBrush)
                    return false;

                SolidColorBrushComparer comparer = new();
                return comparer.Equals(actualBrush, expectedBrush);
            })
            .FailWith("Expected {context} to be {0}{reason}, but found {1}.", expectedColor, Subject);

        return new AndConstraint<BrushAssertions>(this);
    }

    public AndConstraint<BrushAssertions> Be(string expected, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(x =>
            {
                Color expectedColor = (Color)ColorConverter.ConvertFromString(expected);
                SolidColorBrush expectedBrush = new(expectedColor);

                if (x is not SolidColorBrush actualBrush)
                    return false;

                SolidColorBrushComparer comparer = new();
                return comparer.Equals(actualBrush, expectedBrush);
            })
            .FailWith("Expected {context} to be {0}{reason}, but found {1}.", expected, Subject);

        return new AndConstraint<BrushAssertions>(this);
    }
}