//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:36:54
// Description: TODO >> This is a script Description.
//============================================================

using JetBrains.Annotations;
using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public static class QuaternionExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="qua4"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string FloatQ4String(this Quaternion qua4)
        {
            var str = $"({qua4.x}, {qua4.y}, {qua4.z}, {qua4.w})";
            return str;
        }
    }
}