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

using System.Text.RegularExpressions;
using DustInTheWind.SvgDotnet.Serialization.XmlModels;

namespace DustInTheWind.SvgDotnet.Serialization.Conversion;

internal class XmlSvgToModelConversion : XmlContainerToModelConversion<XmlSvg, Svg>
{
    protected override string ElementName => "svg";

    public XmlSvgToModelConversion(XmlSvg xmlSvg, DeserializationContext deserializationContext)
        : base(xmlSvg, deserializationContext)
    {
        AllowedChildTypes.AddRange(new[]
        {
            typeof(XmlDesc),
            typeof(XmlTitle),

            typeof(XmlLinearGradient),
            typeof(XmlRadialGradient),

            typeof(XmlCircle),
            typeof(XmlEllipse),
            typeof(XmlLine),
            typeof(XmlPath),
            typeof(XmlPolygon),
            typeof(XmlPolyline),
            typeof(XmlRect),

            typeof(XmlDefs),
            typeof(XmlG),
            typeof(XmlSvg),
            typeof(XmlSymbol),
            typeof(XmlUse),

            typeof(XmlClipPath),
            typeof(XmlStyle),
            typeof(XmlText)
        });
    }

    protected override Svg CreateSvgElement()
    {
        return new Svg();
    }

    protected override void ConvertProperties()
    {
        if (XmlElement == null)
            return;

        base.ConvertProperties();

        ConvertVersion();
        ConvertPosition();
        ConvertSize();
        ConvertViewBox();
        ConvertPreserveAspectRation();
    }

    private void ConvertPreserveAspectRation()
    {
        if (XmlElement.PreserveAspectRatio == null)
            return;

        PreserveAspectRatio? preserveAspectRatio = Parse(XmlElement.PreserveAspectRatio);

        if (preserveAspectRatio == null)
        {
            string path = DeserializationContext.Path.ToString();
            DeserializationContext.Issues.AddError(path, $"[{ElementName}] Invalid value for 'preserveAspectRatio'.");
        }
        else
        {
            SvgElement.PreserveAspectRatio = preserveAspectRatio.Value;
        }
    }

    private static PreserveAspectRatio? Parse(string text)
    {
        Regex regex = new(@"^\s*(none|xMinYMin|xMidYMin|xMaxYMin|xMinYMid|xMidYMid|xMaxYMid|xMinYMax|xMidYMax|xMaxYMax)(?:\s+(meet|slice))?\s*$", RegexOptions.Singleline);

        Match match = regex.Match(text);

        if (!match.Success)
            return null;

        Align align = Enum.Parse<Align>(match.Groups[1].Value, true);
        MeetOrSlice meetOrSlice = match.Groups.Count > 1 && !string.IsNullOrEmpty(match.Groups[2].Value)
            ? Enum.Parse<MeetOrSlice>(match.Groups[2].Value, true)
            : MeetOrSlice.Meet;

        return new PreserveAspectRatio(align, meetOrSlice);
    }

    private void ConvertVersion()
    {
        if (XmlElement.Version != null)
            SvgElement.Version = XmlElement.Version;
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

    private void ConvertViewBox()
    {
        if (XmlElement.ViewBox != null)
            SvgElement.ViewBox = XmlElement.ViewBox;
    }
}