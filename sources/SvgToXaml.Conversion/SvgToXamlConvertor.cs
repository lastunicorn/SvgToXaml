﻿// SvgToXaml
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

using DustInTheWind.SvgDotnet;
using DustInTheWind.SvgToXaml.Conversion.Conversions;

namespace DustInTheWind.SvgToXaml.Conversion;

public class SvgToXamlConvertor
{
    public ConversionResult ConvertToXaml(Svg svg)
    {
        ConversionContext conversionContext = new();

        SvgToXamlConversion svgToXamlConversion = new(svg, conversionContext);
        conversionContext.Canvas = svgToXamlConversion.Execute();

        return new ConversionResult
        {
            Canvas = conversionContext.Canvas,
            Issues = conversionContext.Issues
        };
    }
}