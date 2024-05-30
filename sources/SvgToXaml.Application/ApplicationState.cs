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

using DustInTheWind.SvgToXaml.Application.SetInputSvg;

namespace DustInTheWind.SvgToXaml.Application;

public class ApplicationState
{
    public List<string> IgnoredNamespaces { get; } = new()
    {
        "http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd",
        "http://www.inkscape.org/namespaces/inkscape"
    };

    public string InputSvg { get; set; }
    
    public string OutputXaml { get; set; }

    public bool OptimizeOutput { get; set; } = true;
    
    public ProcessingIssueCollection LastConversionIssues { get; set; }
}