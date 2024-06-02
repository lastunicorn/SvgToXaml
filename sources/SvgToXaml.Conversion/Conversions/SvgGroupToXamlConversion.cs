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
using DustInTheWind.SvgDotnet;

namespace DustInTheWind.SvgToXaml.Conversion.Conversions;

internal class SvgGroupToXamlConversion : SvgContainerToXamlConversion<SvgGroup, Canvas>
{
    public SvgGroupToXamlConversion(SvgGroup svgGroup, ConversionContext conversionContext, SvgElement referrer = null)
        : base(svgGroup, conversionContext, referrer)
    {
    }

    protected override Canvas CreateXamlElement()
    {
        return new Canvas();
    }

    protected override void OnExecuted()
    {
        base.OnExecuted();

        if (XamlElement == null)
            return;

        bool isZeroSize = XamlElement.Width == 0 || XamlElement.Height == 0;
        if (isZeroSize)
            ConversionContext.Issues.AddWarning($"[{XamlElement.GetType().Name}] Zero-size canvas detected.");

        bool isEmpty = XamlElement.Children.Count == 0;
        if (isEmpty)
            ConversionContext.Issues.AddWarning($"[{XamlElement.GetType().Name}] Empty canvas detected.");
    }
}