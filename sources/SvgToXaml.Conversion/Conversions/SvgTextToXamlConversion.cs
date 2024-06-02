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

using System.Windows.Controls;
using System.Windows.Media;
using DustInTheWind.SvgDotnet;
using TranslateTransform = System.Windows.Media.TranslateTransform;

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal class SvgTextToXamlConversion : ToXamlConversion<SvgText, TextBlock>
{
    public SvgTextToXamlConversion(SvgText svgText, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgText, conversionContext, referrer)
    {
    }

    protected override TextBlock CreateXamlElement()
    {
        return new TextBlock();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertText();
        ConvertFontSize();
        ConvertPosition();
        ConvertFill();
    }

    private void ConvertText()
    {
        if (SvgElement.Text == null)
            return;

        TextWhiteSpaceProcessing textWhiteSpaceProcessing = new(SvgElement.Text, ShadowTree);
        textWhiteSpaceProcessing.Execute();

        XamlElement.Text = textWhiteSpaceProcessing.Text;
    }

    private void ConvertFill()
    {
        Paint fill = ShadowTree
            .Select(x => x.ComputeFill())
            .FirstOrDefault(x => x != null);

        if (fill == null)
        {
        }
        else if (fill.IsNone)
        {
        }
        else if (fill.Color is { IsEmpty: false })
        {
            AlphaValue? fillOpacity = SvgElement.ComputeFillOpacity();

            Color color = fillOpacity == null
                ? fill.Color.ToColor()
                : fill.Color.ToColor(fillOpacity.Value.NumberValue);

            XamlElement.Foreground = new SolidColorBrush(color);
        }
        else if (fill.Url is { IsEmpty: false })
        {
        }
    }

    private void ConvertFontSize()
    {
        LengthPercentage? fontSize = ShadowTree
            .Select(x => x.ComputeFontSize())
            .FirstOrDefault(x => x != null);

        if (fontSize != null)
            XamlElement.FontSize = fontSize.Value.ComputeValue();
    }

    private void ConvertPosition()
    {
        double left = SvgElement.X;
        double top = SvgElement.Y;

        if (left != 0 || top != 0)
            XamlElement.RenderTransform = new TranslateTransform(left, top);
    }
}