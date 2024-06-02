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
using System.Reflection;
using System.Xml;

namespace DustInTheWind.SvgToXaml.Application.Processing;

internal class XmlAlteration
{
    private readonly List<Type> alterationStepsTypes = new();
    private readonly XmlDocument xmlDocument;

    public XmlAlteration(string xml)
    {
        xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xml);
    }

    public void AddStep(Type stepType)
    {
        bool isAlterationStep = typeof(IXmlAlterationStep).IsAssignableFrom(stepType);

        if (!isAlterationStep)
            throw new ArgumentException("The provided type is not an alteration step.", nameof(stepType));

        bool hasConstructor = HasConstructor(stepType);

        if (!hasConstructor)
            throw new ArgumentException("The provided step must have a constructor accepting one XmlDocument object.", nameof(stepType));

        alterationStepsTypes.Add(stepType);
    }

    private static bool HasConstructor(Type type)
    {
        return type.GetConstructors()
            .Any(x =>
            {
                ParameterInfo[] parameterInfos = x.GetParameters();

                if (parameterInfos.Length != 1)
                    return false;

                return parameterInfos[0].ParameterType == typeof(XmlDocument);
            });
    }

    public void Execute()
    {
        IEnumerable<IXmlAlterationStep> alterations = alterationStepsTypes
            .Select(x =>
            {
                ConstructorInfo constructor = x.GetConstructor(new[] { typeof(XmlDocument) });
                return constructor.Invoke(new[] { xmlDocument }) as IXmlAlterationStep;
            })
            .Where(x => x != null);

        foreach (IXmlAlterationStep step in alterations)
            step.Execute();
    }

    public string SerializeResult()
    {
        using MemoryStream ms = new();

        XmlWriterSettings settings = new()
        {
            Indent = true,
            NewLineOnAttributes = true
        };

        using XmlWriter xmlWriter = XmlWriter.Create(ms, settings);

        xmlDocument.WriteTo(xmlWriter);
        xmlWriter.Flush();

        ms.Position = 0;

        using StreamReader sr = new(ms);
        return sr.ReadToEnd();
    }
}