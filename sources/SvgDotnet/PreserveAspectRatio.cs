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

using System.ComponentModel;

namespace DustInTheWind.SvgDotnet;

public struct PreserveAspectRatio
{
    public Align Align { get; }

    public MeetOrSlice MeetOrSlice { get; }

    public static PreserveAspectRatio Default = new();

    public PreserveAspectRatio()
    {
        Align = Align.XMidYMid;
        MeetOrSlice = MeetOrSlice.Meet;
    }

    public PreserveAspectRatio(Align align)
    {
        if (!Enum.IsDefined(typeof(Align), align)) throw new InvalidEnumArgumentException(nameof(align), (int)align, typeof(Align));

        Align = align;
        MeetOrSlice = MeetOrSlice.Meet;
    }

    public PreserveAspectRatio(Align align, MeetOrSlice meetOrSlice)
    {
        if (!Enum.IsDefined(typeof(Align), align)) throw new InvalidEnumArgumentException(nameof(align), (int)align, typeof(Align));
        if (!Enum.IsDefined(typeof(MeetOrSlice), meetOrSlice)) throw new InvalidEnumArgumentException(nameof(meetOrSlice), (int)meetOrSlice, typeof(MeetOrSlice));

        Align = align;
        MeetOrSlice = meetOrSlice;
    }
}