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
using DustInTheWind.SvgToXaml.Svg;

namespace DustInTheWind.SvgToXaml.Conversion;

internal static class SvgTransformExtensions
{
    public static Transform ToXaml(this IList<ISvgTransform> svgTransformList, Transform existingTransform)
    {
        if (svgTransformList == null) throw new ArgumentNullException(nameof(svgTransformList));

        IEnumerable<Transform> transforms = svgTransformList
            .Reverse()
            .Select(x => x.ToXaml());

        TransformGroupBuilder transformGroupBuilder = new(existingTransform);
        transformGroupBuilder.AddRange(transforms);

        return transformGroupBuilder.RootTransform;
    }

    public static Transform ToXaml(this ISvgTransform svgTransform)
    {
        return svgTransform switch
        {
            SvgTranslateTransform svgTranslateTransform => svgTranslateTransform.ToXaml(),
            SvgScaleTransform svgScaleTransform => svgScaleTransform.ToXaml(),
            SvgRotateTransform svgRotateTransform => svgRotateTransform.ToXaml(),
            SvgMatrixTransform svgMatrixTransform => svgMatrixTransform.ToXaml(),
            _ => throw new ArgumentException("Unrecognized transformation object.", nameof(svgTransform))
        };
    }

    private static Transform ToXaml(this SvgTranslateTransform svgTranslateTransform)
    {
        return new TranslateTransform
        {
            X = svgTranslateTransform.X,
            Y = svgTranslateTransform.Y
        };
    }

    private static Transform ToXaml(this SvgScaleTransform svgScaleTransform)
    {
        ScaleTransform xaml = new();

        if (svgScaleTransform.CenterX != null)
            xaml.CenterX = svgScaleTransform.CenterX.Value;

        if (svgScaleTransform.CenterY != null)
            xaml.CenterY = svgScaleTransform.CenterY.Value;

        if (svgScaleTransform.ScaleX != null)
            xaml.ScaleX = svgScaleTransform.ScaleX.Value;

        if (svgScaleTransform.ScaleY != null)
            xaml.ScaleY = svgScaleTransform.ScaleY.Value;

        return xaml;
    }

    private static Transform ToXaml(this SvgRotateTransform svgRotateTransform)
    {
        RotateTransform transform = new()
        {
            Angle = svgRotateTransform.Angle
        };

        if (svgRotateTransform.CenterX != null)
            transform.CenterX = svgRotateTransform.CenterX.Value;

        if (svgRotateTransform.CenterY != null)
            transform.CenterY = svgRotateTransform.CenterY.Value;

        return transform;
    }

    private static Transform ToXaml(this SvgMatrixTransform svgMatrixTransform)
    {
        return new MatrixTransform
        {
            Matrix = new Matrix
            {
                M11 = svgMatrixTransform.M11,
                M12 = svgMatrixTransform.M12,
                M21 = svgMatrixTransform.M21,
                M22 = svgMatrixTransform.M22,
                OffsetX = svgMatrixTransform.OffsetX,
                OffsetY = svgMatrixTransform.OffsetY
            }
        };
    }

    public static void TranslateOriginRecursively(this Transform transform, double x, double y)
    {
        if (transform is RotateTransform rotateTransform)
        {
            rotateTransform.TranslateOrigin(x, y);
        }
        else if (transform is TransformGroup transformGroup)
        {
            foreach (Transform childTransforms in transformGroup.Children)
                TranslateOriginRecursively(childTransforms, x, y);
        }
    }

    public static void TranslateOrigin(this RotateTransform rotateTransform, double x, double y)
    {
        bool xIsAbsent = double.IsNaN(x) || x == 0;
        if (!xIsAbsent)
            rotateTransform.CenterX -= x;

        bool yIsAbsent = double.IsNaN(y) || y == 0;
        if (!yIsAbsent)
            rotateTransform.CenterY -= y;
    }
}