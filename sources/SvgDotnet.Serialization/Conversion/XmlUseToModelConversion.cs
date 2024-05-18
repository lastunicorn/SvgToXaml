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

using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

internal class XmlUseToModelConversion : XmlElementToModelConversion<XmlUse, SvgUse>
{
    protected override string ElementName => "use";

    public XmlUseToModelConversion(XmlUse xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgUse CreateSvgElement()
    {
        return new SvgUse();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertPosition();
        ConvertSize();
        ConvertReference();
        ConvertChildren();
    }

    private void ConvertPosition()
    {
        LengthPercentage? x = XmlElement.X;

        if (x != null)
            SvgElement.X = x;

        LengthPercentage? y = XmlElement.Y;

        if (y != null)
            SvgElement.Y = y;
    }

    private void ConvertSize()
    {
        if (XmlElement.Width != null)
            SvgElement.Width = XmlElement.Width;

        if (XmlElement.Height != null)
            SvgElement.Height = XmlElement.Height;
    }

    private void ConvertReference()
    {
        SvgElement.Href = XmlElement.Href ?? XmlElement.HrefLink;
    }

    private void ConvertChildren()
    {
        if (XmlElement.Children != null)
        {
            IEnumerable<SvgElement> elements = XmlElement.Children
                .Select(CreateConversionFor)
                .Where(x => x != null)
                .Select(x => x.Execute());

            foreach (SvgElement svgElement in elements)
                SvgElement.Children.Add(svgElement);
        }
    }

    private IToModelConversion<SvgElement> CreateConversionFor(object objectToConvert)
    {
        switch (objectToConvert)
        {
            case XmlDesc desc:
                return new XmlDescToModelConversion(desc, DeserializationContext);

            case XmlTitle title:
                return new XmlTitleToModelConversion(title, DeserializationContext);

            case XmlClipPath clipPath:
                return new XmlClipPathToModelConversion(clipPath, DeserializationContext);

            case XmlStyle style:
                return new XmlStyleToModelConversion(style, DeserializationContext);

            default:
                DeserializationIssue deserializationIssue = new("Xml deserialization", $"Unknown element type {objectToConvert.GetType().Name} in {ElementName}.");
                DeserializationContext.Errors.Add(deserializationIssue);
                return null;
        }
    }
}