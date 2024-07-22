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

using System.IO;
using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

internal class XmlRadialGradientToModelConversion : XmlElementToModelConversion<XmlRadialGradient, SvgRadialGradient>
{
    protected override string ElementName => "radialGradient";

    public XmlRadialGradientToModelConversion(XmlRadialGradient xmlElement, DeserializationContext deserializationContext)
        : base(xmlElement, deserializationContext)
    {
    }

    protected override SvgRadialGradient CreateSvgElement()
    {
        return new SvgRadialGradient();
    }

    protected override void ConvertProperties()
    {
        base.ConvertProperties();

        ConvertGradientUnits();
        ConvertRadius();
        ConvertCenter();
        ConvertGradientOrigin();
        ConvertChildren();
        ConvertGradientTransform();
        ConvertHref();
        ConvertSpreadMethod();
    }

    private void ConvertGradientUnits()
    {
        if (XmlElement.GradientUnitsSpecified)
        {
            SvgElement.GradientUnits = XmlElement.GradientUnits switch
            {
                XmlGradientUnits.ObjectBoundingBox => SvgGradientUnits.ObjectBoundingBox,
                XmlGradientUnits.UserSpaceOnUse => SvgGradientUnits.UserSpaceOnUse,
                _ => throw new Exception("Invalid gradient unit value.")
            };
        }
    }

    private void ConvertRadius()
    {
        Length? radius = XmlElement.R;

        if (radius != null)
        {
            if (radius.Value < 0)
            {
                SvgElement.Radius = 0;

                DeserializationContext.Path.AddAttribute("r");
                string path = DeserializationContext.Path.ToString();
                DeserializationContext.Path.RemoveLast();

                NegativeValueIssue issue = new(DeserializationIssueLevel.Warning, path);
                DeserializationContext.Issues.Add(issue);
            }
            else
            {
                SvgElement.Radius = radius.Value;
            }
        }
    }

    private void ConvertCenter()
    {
        if (XmlElement.Cx != null)
            SvgElement.CenterX = XmlElement.Cx;

        if (XmlElement.Cx != null)
            SvgElement.CenterY = XmlElement.Cy;
    }

    private void ConvertGradientOrigin()
    {
        if (XmlElement.Fx != null)
            SvgElement.Fx = XmlElement.Fx;

        if (XmlElement.Fy != null)
            SvgElement.Fy = XmlElement.Fy;
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

            case XmlStop stop:
                return new XmlStopToModelConversion(stop, DeserializationContext);

            case XmlStyle style:
                return new XmlStyleToModelConversion(style, DeserializationContext);

            default:
                string path = DeserializationContext.Path.ToString();
                DeserializationContext.Issues.AddError(path, $"[{ElementName}] Unknown element type {objectToConvert.GetType().Name}.");
                return null;
        }
    }

    private void ConvertGradientTransform()
    {
        if (XmlElement.GradientTransform != null)
        {
            try
            {
                SvgElement.GradientTransforms.ParseAndAdd(XmlElement.GradientTransform);
            }
            catch (Exception ex)
            {
                DeserializationContext.Path.AddAttribute("gradientTransform");
                string path = DeserializationContext.Path.ToString();
                DeserializationContext.Path.RemoveLast();

                DeserializationIssue issue = new()
                {
                    Level = DeserializationIssueLevel.Error,
                    Path = path,
                    Message = ex.ToString()
                };
                DeserializationContext.Issues.Add(issue);
            }
        }
    }

    private void ConvertHref()
    {
        if (XmlElement.Href != null)
            SvgElement.Href = XmlElement.Href;
    }

    private void ConvertSpreadMethod()
    {
        if (XmlElement.SpreadMethodSpecified)
        {
            SvgElement.SpreadMethod = XmlElement.SpreadMethod switch
            {
                XmlSpreadMethod.Pad => SvgSpreadMethod.Pad,
                XmlSpreadMethod.Reflect => SvgSpreadMethod.Reflect,
                XmlSpreadMethod.Repeat => SvgSpreadMethod.Repeat,
                _ => throw new Exception("Invalid spread method value.")
            };
        }
    }
}