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

namespace DustInTheWind.SvgToXaml.SvgModel;

public readonly struct SvgOpacity
{
    private readonly byte value;

    public SvgOpacity()
    {
        value = byte.MaxValue;
    }

    public SvgOpacity(float value)
    {
        this.value = value switch
        {
            <= 0 => byte.MinValue,
            >= 1 => byte.MaxValue,
            _ => (byte)Math.Round(value * byte.MaxValue)
        };
    }

    public SvgOpacity(double value)
    {
        this.value = value switch
        {
            <= 0 => byte.MinValue,
            >= 1 => byte.MaxValue,
            _ => (byte)Math.Round(value * byte.MaxValue)
        };
    }

    public SvgOpacity(byte value)
    {
        this.value = value;
    }

    public override string ToString()
    {
        return (value / 255).ToString();
    }

    public static implicit operator SvgOpacity(string text)
    {
        float value = float.Parse(text);
        return new SvgOpacity(value);
    }

    public static implicit operator SvgOpacity(double value)
    {
        return new SvgOpacity(value);
    }

    public static implicit operator SvgOpacity(float value)
    {
        return new SvgOpacity(value);
    }

    public static implicit operator byte(SvgOpacity opacity)
    {
        return opacity.value;
    }

    public static implicit operator SvgOpacity(byte value)
    {
        return new SvgOpacity(value);
    }
}