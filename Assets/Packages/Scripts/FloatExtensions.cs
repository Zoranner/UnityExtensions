//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:42:31
// Description: TODO >> This is a script Description.
//============================================================

using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public static class FloatExtensions
    {
        public static float Wave(this float value, float offset)
        {
            return value.Wave(-offset, offset);
        }

        public static float Wave(this float value, float floor, float ceiling)
        {
            return value + Random.Range(floor, ceiling);
        }
    }
}