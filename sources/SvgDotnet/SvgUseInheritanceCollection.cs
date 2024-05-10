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

using System.Collections;

namespace DustInTheWind.SvgDotnet;

public class SvgUseInheritanceCollection : IEnumerable<SvgElement>
{
    private readonly SvgUse svgUse;

    public SvgElement InheritedElement { get; }

    public SvgUseInheritanceCollection(SvgUse svgUse)
    {
        this.svgUse = svgUse;

        InheritedElement = svgUse?.GetReferencedElement();
    }

    public IEnumerator<SvgElement> GetEnumerator()
    {
        if (svgUse == null)
            return new DummyEnumerator<SvgElement>();

        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class Enumerator : IEnumerator<SvgElement>
    {
        private readonly SvgUseInheritanceCollection svgUseInheritanceCollection;
        private EnumeratorState state;
        private IEnumerator<SvgElement> ancestorsEnumerator;

        object IEnumerator.Current => Current;

        public SvgElement Current { get; private set; }

        public Enumerator(SvgUseInheritanceCollection svgUseInheritanceCollection)
        {
            this.svgUseInheritanceCollection = svgUseInheritanceCollection ?? throw new ArgumentNullException(nameof(svgUseInheritanceCollection));
        }

        public bool MoveNext()
        {
            switch (state)
            {
                case EnumeratorState.New:
                {
                    state = EnumeratorState.Myself;
                    Current = svgUseInheritanceCollection.svgUse;
                    return true;
                }

                case EnumeratorState.Myself:
                {
                    state = EnumeratorState.Inherited;
                    Current = svgUseInheritanceCollection.InheritedElement;
                    return true;
                }

                case EnumeratorState.Inherited:
                {
                    state = EnumeratorState.Ancestors;
                    ancestorsEnumerator = svgUseInheritanceCollection.svgUse!.EnumerateAncestors().GetEnumerator();

                    bool success = ancestorsEnumerator.MoveNext();

                    if (!success)
                    {
                        state = EnumeratorState.Finished;
                        return false;
                    }

                    Current = ancestorsEnumerator.Current;
                    return true;
                }

                case EnumeratorState.Ancestors:
                {
                    bool success = ancestorsEnumerator!.MoveNext();

                    if (!success)
                    {
                        state = EnumeratorState.Finished;
                        return false;
                    }

                    Current = ancestorsEnumerator.Current;
                    return true;
                }

                case EnumeratorState.Finished:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Reset()
        {
            state = EnumeratorState.New;
            ancestorsEnumerator = null;
        }

        public void Dispose()
        {
        }
    }

    private enum EnumeratorState
    {
        New = 0,
        Myself,
        Inherited,
        Ancestors,
        Finished
    }
}