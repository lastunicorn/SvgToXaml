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
using DustInTheWind.SvgToXaml.Svg;

namespace DustInTheWind.SvgToXaml.Conversion;

internal static class SvgTransformExtensions
{
    public static Transform ToXaml(this IList<ISvgTransform> svgTransformList)
    {
        switch (svgTransformList.Count)
        {
            case 1:
                return svgTransformList[0].ToXaml();

            case > 1:
            {
                TransformGroup transformGroup = new();

                for (int i = svgTransformList.Count - 1; i >= 0; i--)
                {
                    ISvgTransform svgTransform = svgTransformList[i];

                    Transform transform = svgTransform.ToXaml();
                    transformGroup.Children.Add(transform);
                }

                return transformGroup;
            }

            default:
                return null;
        }
    }

    public static Transform ToXaml(this ISvgTransform svgTransform)
    {
        switch (svgTransform)
        {
            case SvgTranslateTransform svgTranslateTransform:
                return new TranslateTransform
                {
                    X = svgTranslateTransform.X,
                    Y = svgTranslateTransform.Y
                };

            case SvgScaleTransform svgScaleTransform:
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

            case SvgRotateTransform svgRotateTransform:
                RotateTransform transform = new()
                {
                    Angle = svgRotateTransform.Angle
                };

                if (svgRotateTransform.CenterX != null)
                    transform.CenterX = svgRotateTransform.CenterX.Value;

                if (svgRotateTransform.CenterY != null)
                    transform.CenterY = svgRotateTransform.CenterY.Value;

                return transform;

            case SvgMatrixTransform svgMatrixTransform:
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

            default:
                throw new ArgumentException("Unrecognized transformation object.", nameof(svgTransform));
        }
    }
}