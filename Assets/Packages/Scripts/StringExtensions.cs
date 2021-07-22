//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:41:49
// Description: TODO >> This is a script Description.
//============================================================

using JetBrains.Annotations;
using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool BoolParse(this string text)
        {
            try
            {
                return bool.Parse(text);
            }
            catch
            {
                return true;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int IntParse(this string text)
        {
            try
            {
                return int.Parse(text);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static int? IntNullParse(this string text)
        {
            try
            {
                return int.Parse(text);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static float FloatParse(this string text)
        {
            try
            {
                return float.Parse(text);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 请规范string格式为"(x, y, z)"。
        /// </summary>
        /// <param name="vec3"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static Vector3 Vector3Parse(this string vec3)
        {
            vec3 = vec3.Replace("(", "").Replace(")", "");
            var s = vec3.Split(',');
            return new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        }

        /// <summary>
        /// 请规范string格式为"(x, y, z, w)"。
        /// </summary>
        /// <param name="qua4"></param>
        /// <returns></returns>
        public static Quaternion QuaternionParse(this string qua4)
        {
            qua4 = qua4.Replace("(", "").Replace(")", "");
            var s = qua4.Split(',');
            return new Quaternion(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
        }

        /// <summary>
        /// 请规范string格式为"(r, g, b)"。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static Color Color3Parse(this string color)
        {
            color = color.Replace("(", "").Replace(")", "");
            var s = color.Split(',');
            return new Color(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        }

        /// <summary>
        /// </summary>
        /// <param name="pose"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static PoseObject Pose10Parse(this string pose)
        {
            pose = pose.Replace("(", "").Replace(")", "");
            var s = pose.Split(',');
            var pos = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
            var rot = new Quaternion(float.Parse(s[3]), float.Parse(s[4]), float.Parse(s[5]), float.Parse(s[6]));
            var scl = new Vector3(float.Parse(s[7]), float.Parse(s[8]), float.Parse(s[9]));
            return new PoseObject(pos, rot, scl);
        }
    }
}