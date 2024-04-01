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

using System.Windows;
using DustInTheWind.SvgToXaml.Svg;

namespace DustInTheWind.SvgToXaml.Conversion;

internal abstract class SvgElementToXamlConversion<TSvg, TXaml> : IConversion<TXaml>
    where TSvg : SvgElement
    where TXaml : UIElement
{
    private readonly SvgElement referrer;

    protected TSvg SvgElement { get; }

    protected TXaml XamlElement { get; private set; }

    protected SvgElementToXamlConversion(TSvg svgElement, SvgElement referrer = null)
    {
        SvgElement = svgElement ?? throw new ArgumentNullException(nameof(svgElement));
        this.referrer = referrer;
    }

    public TXaml Execute()
    {
        try
        {
            XamlElement = CreateXamlElement();

            if (SvgElement.Transforms.Count > 0)
                XamlElement.RenderTransform = SvgElement.Transforms.ToXaml(XamlElement.RenderTransform);

            List<SvgElement> inheritedSvgElements = EnumerateInheritedElements().ToList();

            InheritPropertiesFrom(inheritedSvgElements);

            return XamlElement;
        }
        catch (SvgConversionException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new SvgConversionException(ex);
        }
    }

    protected abstract TXaml CreateXamlElement();

    protected virtual IEnumerable<SvgElement> EnumerateInheritedElements()
    {
        if (referrer == null)
        {
            yield return SvgElement;

            IEnumerable<SvgElement> ancestors = SvgElement.EnumerateAncestors();

            foreach (SvgElement ancestor in ancestors)
                yield return ancestor;
        }
        else
        {
            yield return SvgElement;

            IEnumerable<SvgElement> ancestors = SvgElement.EnumerateAncestors()
                .TakeWhile(x => x.GetType() != typeof(SvgDefinitions));

            foreach (SvgElement ancestor in ancestors)
                yield return ancestor;

            yield return referrer;

            IEnumerable<SvgElement> referrerAncestors = referrer.EnumerateAncestors();

            foreach (SvgElement ancestor in referrerAncestors)
                yield return ancestor;
        }
    }

    protected virtual void InheritPropertiesFrom(IEnumerable<SvgElement> svgElements)
    {
        SetOpacity(svgElements);
    }

    private void SetOpacity(IEnumerable<SvgElement> svgElements)
    {
        double? opacity = svgElements
            .Select(x => x.CalculateOpacity())
            .FirstOrDefault(x => x != null);

        if (opacity != null)
            XamlElement.Opacity = opacity.Value;
    }
}