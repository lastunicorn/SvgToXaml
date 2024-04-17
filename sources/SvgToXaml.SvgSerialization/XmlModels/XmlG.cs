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

namespace DustInTheWind.SvgToXaml.SvgSerialization.XmlModels;

/// <remarks>
/// May contain any number of the following elements, in any order:
///    - animation elements — ‘animate’, ‘animateMotion’, ‘animateTransform’, ‘discard’, ‘set’
///    - descriptive elements — ‘desc’, ‘title’, ‘metadata’
///    - paint server elements — ‘linearGradient’, ‘radialGradient’, ‘pattern’
///    - shape elements — ‘circle’, ‘ellipse’, ‘line’, ‘path’, ‘polygon’, ‘polyline’, ‘rect’
///    - structural elements — ‘defs’, ‘g’, ‘svg’, ‘symbol’, ‘use’
///    - a, audio, canvas, clipPath, filter, foreignObject, iframe, image, marker, mask, script, style, switch, text, video, view.
/// </remarks>
public class XmlG : XmlContainer
{
}