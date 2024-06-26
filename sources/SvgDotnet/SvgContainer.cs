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

namespace DustInTheWind.SvgDotnet;

public abstract class SvgContainer : SvgElement
{
    public SvgElementCollection<SvgElement> Children { get; }

    protected SvgContainer()
    {
        Children = new SvgElementCollection<SvgElement>(this);
    }

    public T AddChild<T>()
        where T : SvgElement, new()
    {
        T svgElement = new();
        Children.Add(svgElement);

        return svgElement;
    }

    internal T AddChild<T>(Action<T> action)
        where T : SvgElement, new()
    {
        if (action == null) throw new ArgumentNullException(nameof(action));

        T svgElement = new();
        action(svgElement);

        Children.Add(svgElement);

        return svgElement;
    }

    public T AddChild<T>(T svgElement)
        where T : SvgElement
    {
        if (svgElement == null) throw new ArgumentNullException(nameof(svgElement));

        Children.Add(svgElement);
        return svgElement;
    }

    public T AddChild<T>(Func<T> func)
        where T : SvgElement
    {
        if (func == null) throw new ArgumentNullException(nameof(func));

        T svgElement = func();
        Children.Add(svgElement);

        return svgElement;
    }

    public virtual SvgElement FindChild(string id)
    {
        return Children.FindChild(id);
    }

    protected IEnumerable<T> GetChildrenRecursively<T>()
    {
        foreach (SvgElement svgElement in Children)
        {
            switch (svgElement)
            {
                case SvgContainer svgContainer:
                {
                    IEnumerable<T> children = svgContainer.GetChildrenRecursively<T>();

                    foreach (T child in children)
                        yield return child;
                    break;
                }

                case T tSvgElement:
                    yield return tSvgElement;
                    break;
            }
        }
    }

    public IEnumerable<StyleRuleSet> GetAllStyleRuleSets(string mimeType = null)
    {
        IEnumerable<SvgStyle> svgStyles = GetStyleSheetsRecursively(mimeType);

        foreach (SvgStyle svgStyle in svgStyles)
        foreach (StyleRuleSet styleRuleSet in svgStyle.RuleSets)
            yield return styleRuleSet;
    }

    private IEnumerable<SvgStyle> GetStyleSheetsRecursively(string mimeType)
    {
        foreach (SvgElement svgElement in Children)
        {
            if (svgElement is SvgStyle svgStyleSheet)
            {
                switch (mimeType)
                {
                    case null:
                        yield return svgStyleSheet;
                        break;

                    case MimeTypes.TextCss:
                        if (svgStyleSheet.Type is null or MimeTypes.TextCss)
                            yield return svgStyleSheet;
                        break;
                }
            }
            else if (svgElement is SvgContainer svgContainer)
            {
                IEnumerable<SvgStyle> styleSheets = svgContainer.GetStyleSheetsRecursively(mimeType);

                foreach (SvgStyle styleSheet in styleSheets)
                    yield return styleSheet;
            }
        }
    }
}