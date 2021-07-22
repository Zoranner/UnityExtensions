//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:36:28
// Description: TODO >> This is a script Description.
//============================================================

using JetBrains.Annotations;
using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// </summary>
        /// <param name="vec3"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string FloatV3String(this Vector3 vec3)
        {
            var str = $"({vec3.x}, {vec3.y}, {vec3.z})";
            return str;
        }
    }
}