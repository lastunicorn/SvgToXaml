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

using DustInTheWind.SvgToXaml.Presentation.InputArea;
using DustInTheWind.SvgToXaml.Presentation.OutputArea;

namespace DustInTheWind.SvgToXaml.Presentation.MainArea;

public class MainViewModel : ViewModelBase
{
    public InputPanelViewModel InputPanelViewModel { get; }

    public OutputPanelViewModel OutputPanelViewModel { get; }

    public MainViewModel(InputPanelViewModel inputPanelViewModel, OutputPanelViewModel outputPanelViewModel)
    {
        InputPanelViewModel = inputPanelViewModel ?? throw new ArgumentNullException(nameof(inputPanelViewModel));
        OutputPanelViewModel = outputPanelViewModel ?? throw new ArgumentNullException(nameof(outputPanelViewModel));
    }
}