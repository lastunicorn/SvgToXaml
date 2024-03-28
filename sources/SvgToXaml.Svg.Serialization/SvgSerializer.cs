// Country Flags
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

using System.Xml.Serialization;

namespace DustInTheWind.SvgToXaml.Svg.Serialization;

public class SvgSerializer
{
    public void Serialize(Stream stream, Svg svg)
    {
        XmlSerializer xmlSerializer = new(typeof(Svg));
        xmlSerializer.Serialize(stream, svg);
    }

    public Svg Deserialize(Stream stream)
    {
        XmlSerializer xmlSerializer = new(typeof(Svg));
        object deserializedObject = xmlSerializer.Deserialize(stream);

        if (deserializedObject is not Svg svgObject)
            throw new Exception("Invalid svg document.");

        return svgObject;
    }

    public Svg Deserialize(TextReader textReader)
    {
        XmlSerializer xmlSerializer = new(typeof(Svg));
        object deserializedObject = xmlSerializer.Deserialize(textReader);

        if (deserializedObject is not Svg svgObject)
            throw new Exception("Invalid svg document.");

        return svgObject;
    }
}