//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-07-02 16:26:36
// Description: TODO >> This is a script Description.
//============================================================

using System;
using JetBrains.Annotations;

namespace Zoranner.Engine.Extensions
{
    public static class TimeSpanExtensions
    {
        [UsedImplicitly]
        public static string ToReadableString(this TimeSpan timeSpan)
        {
            return timeSpan.Days > 0 ? $"{timeSpan:%d}d{timeSpan:%h}hr{timeSpan:%m}min{timeSpan:%s}sec" :
                timeSpan.Hours > 0 ? $"{timeSpan:%h}hr{timeSpan:%m}min{timeSpan:%s}sec" :
                timeSpan.Minutes > 0 ? $"{timeSpan:%m}min{timeSpan:%s}sec" :$"{timeSpan:%s}sec";
        }
    }
}