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

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DustInTheWind.SvgToXaml.Presentation.OutputArea;

public class InfoItemTypeToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IssueType conversionInfoItemType)
        {
            return conversionInfoItemType switch
            {
                IssueType.Error => Brushes.OrangeRed,
                IssueType.Waring => new SolidColorBrush(Color.FromRgb(0xfc, 0xdd, 0x5f)),
                IssueType.Info => Brushes.LightSkyBlue
            };
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}