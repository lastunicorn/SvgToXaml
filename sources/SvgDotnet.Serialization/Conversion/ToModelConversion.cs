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

using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

internal abstract class ToModelConversion<TXml, TSvg> : IToModelConversion<TSvg>
    where TXml : XmlElement
    where TSvg : SvgElement
{
    protected DeserializationContext DeserializationContext { get; }

    protected abstract string ElementName { get; }

    protected TXml XmlElement { get; }

    protected TSvg SvgElement { get; private set; }

    protected ToModelConversion(TXml xmlElement, DeserializationContext deserializationContext)
    {
        XmlElement = xmlElement ?? throw new ArgumentNullException(nameof(xmlElement));
        DeserializationContext = deserializationContext ?? throw new ArgumentNullException(nameof(deserializationContext));
    }

    public TSvg Execute()
    {
        DeserializationContext.Path.AddElement(ElementName);

        try
        {
            SvgElement = CreateSvgElement();
            ConvertProperties();

            return SvgElement;
        }
        finally
        {
            DeserializationContext.Path.RemoveLast();
        }
    }

    protected abstract TSvg CreateSvgElement();

    protected virtual void ConvertProperties()
    {
    }
}