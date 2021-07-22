//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:37:28
// Description: TODO >> This is a script Description.
//============================================================

using JetBrains.Annotations;
using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ToFloatC3String(this Color color)
        {
            var str = $"({color.r}, {color.g}, {color.b})";
            return str;
        }
    }
}