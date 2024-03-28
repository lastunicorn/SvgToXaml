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

using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace DustInTheWind.SvgToXaml.Tests.Utils.Assertions;

public class CanvasAssertions : ReferenceTypeAssertions<Canvas, CanvasAssertions>
{
    protected override string Identifier => "Canvas";

    public CanvasAssertions(Canvas instance)
        : base(instance)
    {
    }

    public AndConstraint<CanvasAssertions> Contain(Type childType, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(x => x.Children.OfType<UIElement>().Any())
            .FailWith($"Expected Canvas to contain child of type \"{childType}\".");

        return new AndConstraint<CanvasAssertions>(this);
    }

    public AndConstraint<CanvasAssertions> ContainExact(Type childType, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.Children)
            .ForCondition(children => children.Count == 1)
            .FailWith($"Expected Canvas to contain one child. Actual count: {Subject.Children.Count}")
            .Then
            .ForCondition(children => children[0].GetType() == childType);

        return new AndConstraint<CanvasAssertions>(this);
    }

    public AndConstraint<CanvasAssertions> ContainExact(IList<Type> childTypes, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject.Children)
            .ForCondition(children => children.Count == childTypes.Count)
            .FailWith($"Expected Canvas to contain child count {childTypes.Count}. Actual count: {Subject.Children.Count}")
            .Then
            .ForCondition(children =>
            {
                for (int i = 0; i < children.Count; i++)
                {
                    Type expectedType = childTypes[i];
                    UIElement canvasChild = children[i];

                    if (canvasChild.GetType() != expectedType)
                        return false;
                }

                return true;
            });

        return new AndConstraint<CanvasAssertions>(this);
    }

    public AndConstraint<CanvasAssertions> ContainExact(TypeHierarchy typeHierarchy, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .Given(() => Subject)
            .ForCondition(canvas => VerifyChildren(canvas.Children, typeHierarchy))
            .FailWith("Canvas child hierarchy different than expected.");

        return new AndConstraint<CanvasAssertions>(this);
    }

    private static bool VerifyChildren(UIElementCollection collection, IList<TypeHierarchyItem> expectedTypes)
    {
        if (collection.Count != expectedTypes.Count)
            return false;

        for (int index = 0; index < collection.Count; index++)
        {
            UIElement uiElement = collection[index];
            TypeHierarchyItem expectedType = expectedTypes[index];

            if (uiElement.GetType() != expectedType.ItemType)
                return false;

            if (uiElement is Panel panel)
            {
                bool childVerifySuccess = VerifyChildren(panel.Children, expectedType);

                if (!childVerifySuccess)
                    return false;
            }
        }

        return true;
    }
}