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

using DustInTheWind.SvgToXaml.SvgModel;
using DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

namespace DustInTheWind.SvgToXaml.SvgSerialization.Conversion;

internal class XmlSvgToModelConversion : ToModelConversionBase<XmlSvg, Svg>
{
    protected override string ElementName => "svg";

    public XmlSvgToModelConversion(XmlSvg xmlSvg, DeserializationContext deserializationContext)
        : base(xmlSvg, deserializationContext)
    {
    }

    protected override Svg DoExecute()
    {
        if (XmlElement == null)
            return null;

        Svg modelSvg = new();
        modelSvg.PopulateFromContainer(XmlElement, DeserializationContext);

        if (XmlElement.Version != null)
            modelSvg.Version = XmlElement.Version;

        if (XmlElement.Width != null)
            modelSvg.Width = XmlElement.Width;

        if (XmlElement.Height != null)
            modelSvg.Height = XmlElement.Height;

        if (XmlElement.ViewBox != null)
            modelSvg.ViewBox = XmlElement.ViewBox;

        return modelSvg;
    }
}