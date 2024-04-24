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
using DustInTheWind.SvgToXaml.SvgModel;
using MatrixTransform = DustInTheWind.SvgToXaml.SvgModel.MatrixTransform;
using RotateTransform = DustInTheWind.SvgToXaml.SvgModel.RotateTransform;
using ScaleTransform = DustInTheWind.SvgToXaml.SvgModel.ScaleTransform;
using TranslateTransform = DustInTheWind.SvgToXaml.SvgModel.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion;

internal static class SvgTransformExtensions
{
    public static Transform ToXaml(this IList<ITransform> svgTransformList, Transform existingTransform)
    {
        if (svgTransformList == null) throw new ArgumentNullException(nameof(svgTransformList));

        IEnumerable<Transform> transforms = svgTransformList
            .Reverse()
            .Select(x => x.ToXaml());

        TransformGroupBuilder transformGroupBuilder = new(existingTransform);
        transformGroupBuilder.AddRange(transforms);

        return transformGroupBuilder.RootTransform;
    }

    public static Transform ToXaml(this ITransform transform)
    {
        return transform switch
        {
            TranslateTransform svgTranslateTransform => svgTranslateTransform.ToXaml(),
            ScaleTransform svgScaleTransform => svgScaleTransform.ToXaml(),
            RotateTransform svgRotateTransform => svgRotateTransform.ToXaml(),
            MatrixTransform svgMatrixTransform => svgMatrixTransform.ToXaml(),
            _ => throw new ArgumentException("Unrecognized transformation object.", nameof(transform))
        };
    }

    private static Transform ToXaml(this TranslateTransform translateTransform)
    {
        return new System.Windows.Media.TranslateTransform
        {
            X = translateTransform.X,
            Y = translateTransform.Y
        };
    }

    private static Transform ToXaml(this ScaleTransform scaleTransform)
    {
        System.Windows.Media.ScaleTransform xaml = new();

        if (scaleTransform.CenterX != null)
            xaml.CenterX = scaleTransform.CenterX.Value;

        if (scaleTransform.CenterY != null)
            xaml.CenterY = scaleTransform.CenterY.Value;

        if (scaleTransform.ScaleX != null)
            xaml.ScaleX = scaleTransform.ScaleX.Value;

        if (scaleTransform.ScaleY != null)
            xaml.ScaleY = scaleTransform.ScaleY.Value;

        return xaml;
    }

    private static Transform ToXaml(this RotateTransform rotateTransform)
    {
        System.Windows.Media.RotateTransform transform = new()
        {
            Angle = rotateTransform.Angle
        };

        if (rotateTransform.CenterX != null)
            transform.CenterX = rotateTransform.CenterX.Value;

        if (rotateTransform.CenterY != null)
            transform.CenterY = rotateTransform.CenterY.Value;

        return transform;
    }

    private static Transform ToXaml(this MatrixTransform matrixTransform)
    {
        return new System.Windows.Media.MatrixTransform
        {
            Matrix = new Matrix
            {
                M11 = matrixTransform.M11,
                M12 = matrixTransform.M12,
                M21 = matrixTransform.M21,
                M22 = matrixTransform.M22,
                OffsetX = matrixTransform.OffsetX,
                OffsetY = matrixTransform.OffsetY
            }
        };
    }

    public static void TranslateOriginRecursively(this Transform transform, double x, double y)
    {
        if (transform is System.Windows.Media.RotateTransform rotateTransform)
        {
            rotateTransform.TranslateOrigin(x, y);
        }
        if (transform is System.Windows.Media.ScaleTransform scaleTransform)
        {
            scaleTransform.TranslateOrigin(x, y);
        }
        else if (transform is TransformGroup transformGroup)
        {
            foreach (Transform childTransforms in transformGroup.Children)
                TranslateOriginRecursively(childTransforms, x, y);
        }
    }

    public static void TranslateOrigin(this System.Windows.Media.RotateTransform rotateTransform, double x, double y)
    {
        bool xIsAbsent = double.IsNaN(x) || x == 0;
        if (!xIsAbsent)
            rotateTransform.CenterX -= x;

        bool yIsAbsent = double.IsNaN(y) || y == 0;
        if (!yIsAbsent)
            rotateTransform.CenterY -= y;
    }

    public static void TranslateOrigin(this System.Windows.Media.ScaleTransform scaleTransform, double x, double y)
    {
        bool xIsAbsent = double.IsNaN(x) || x == 0;
        if (!xIsAbsent)
            scaleTransform.CenterX -= x;

        bool yIsAbsent = double.IsNaN(y) || y == 0;
        if (!yIsAbsent)
            scaleTransform.CenterY -= y;
    }
}