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

public class ColorAssertions : ReferenceTypeAssertions<Color, ColorAssertions>
{
    protected override string Identifier => "Color";

    public ColorAssertions(Color instance)
        : base(instance)
    {
    }

    public AndConstraint<ColorAssertions> Be(string expectedColor, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(x =>
            {
                Color expectedColorObject = (Color)ColorConverter.ConvertFromString(expectedColor);
                return x == expectedColorObject;
            })
            .FailWith("Expected {context} to be {0}{reason}, but found {1}.", expectedColor, Subject);

        return new AndConstraint<ColorAssertions>(this);
    }
}