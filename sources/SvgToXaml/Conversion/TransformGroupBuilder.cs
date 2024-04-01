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

using System.Windows.Media;

namespace DustInTheWind.SvgToXaml.Conversion;

internal class TransformGroupBuilder
{
    public Transform RootTransform { get; private set; }

    public TransformGroupBuilder(Transform initialTransform)
    {
        switch (initialTransform)
        {
            case null:
                break;

            case TransformGroup initialTransformGroup:
                RootTransform = initialTransformGroup;
                break;

            default:
                RootTransform = initialTransform;
                break;
        }
    }

    public void Add(Transform transform)
    {
        if (transform == null) throw new ArgumentNullException(nameof(transform));

        if (RootTransform == null)
        {
            RootTransform = transform;
        }
        else if (RootTransform is TransformGroup rootTransformGroup)
        {
            rootTransformGroup.Children.Add(transform);
        }
        else
        {
            TransformGroup newRootTransformGroup = new();
            
            newRootTransformGroup.Children.Add(RootTransform);
            newRootTransformGroup.Children.Add(transform);

            RootTransform = newRootTransformGroup;
        }
    }
}