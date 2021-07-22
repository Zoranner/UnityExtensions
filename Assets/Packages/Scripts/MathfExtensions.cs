//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 14:47:50
// Description: TODO >> This is a script Description.
//============================================================

using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public enum MotionMode
    {
        Constant,
        Accelerate,
        Decelerate,
        Fade
    }

    public static class MathfExtensions
    {
        /// <summary>
        /// 贝塞尔插值法
        /// </summary>
        /// <param name="ctt"></param>
        /// <param name="tlt"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "RedundantAssignment")]
        [UsedImplicitly]
        public static float BezierLerp(float ctt, float tlt, MotionMode m)
        {
            //float distance = Vector3.Distance(StartPoint, PausePoint);

            float bzf = 0;
            var tef = 0.5F;
            float gst = 0;
            const float DEG = 0.000001F;

            var tr = new Vector3(0, 0);
            var p0 = new Vector3(0, 0);
            var t0 = new Vector3(0, 0);
            var t1 = new Vector3(0, 0);
            var p1 = new Vector3(tlt, 1);

            switch (m)
            {
                case MotionMode.Constant:
                    t0 = new Vector3(0, 0);
                    t1 = new Vector3(tlt, 1);
                    break;
                case MotionMode.Accelerate:
                    t0 = new Vector3(tlt * 0.7F, 0);
                    t1 = new Vector3(tlt, 1);
                    break;
                case MotionMode.Decelerate:
                    t0 = new Vector3(0, 0);
                    t1 = new Vector3(tlt * 0.3F, 1);
                    break;
                case MotionMode.Fade:
                    t0 = new Vector3(tlt * 0.7F, 0);
                    t1 = new Vector3(tlt * 0.3F, 1);
                    break;
                default:
                    t0 = new Vector3(tlt * 0.7F, 0);
                    t1 = new Vector3(tlt * 0.3F, 1);
                    break;
            }

            do
            {
                tr = Bezier(p0, t0, t1, p1, bzf);
                gst = tr.x;

                if (gst - ctt > DEG)
                {
                    bzf = bzf - tef;
                }
                else if (gst - ctt < -DEG)
                {
                    bzf = bzf + tef;
                }

                tef = tef / 2;
            } while (Math.Abs(gst - ctt) > DEG && tef > DEG);

            return tr.y;
        }

        /// <summary>
        /// 贝塞尔曲线上任意点坐标算法
        /// </summary>
        /// <param name="t0"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="t1"></param>
        /// <param name="f"></param>
        /// <returns>Bezier Position</returns>
        private static Vector3 Bezier(Vector3 p0, Vector3 t0, Vector3 t1, Vector3 p1, double f)
        {
            const double FT2 = 3;
            const double FT3 = -3;
            const double FU1 = 3;
            const double FU2 = -6;
            const double FU3 = 3;
            const double FV1 = -3;
            const double FV2 = 3;

            var fax = -p0.x + FT2 * t0.x + FT3 * t1.x + p1.x;
            var fbx = FU1 * p0.x + FU2 * t0.x + FU3 * t1.x;
            var fcx = FV1 * p0.x + FV2 * t0.x;
            double fdx = p0.x;

            var fay = -p0.y + FT2 * t0.y + FT3 * t1.y + p1.y;
            var fby = FU1 * p0.y + FU2 * t0.y + FU3 * t1.y;
            var fcy = FV1 * p0.y + FV2 * t0.y;
            double fdy = p0.y;

            var faz = -p0.z + FT2 * t0.z + FT3 * t1.z + p1.z;
            var fbz = FU1 * p0.z + FU2 * t0.z + FU3 * t1.z;
            var fcz = FV1 * p0.z + FV2 * t0.z;
            double fdz = p0.z;

            var fx = (float) (((fax * f + fbx) * f + fcx) * f + fdx);
            var fy = (float) (((fay * f + fby) * f + fcy) * f + fdy);
            var fz = (float) (((faz * f + fbz) * f + fcz) * f + fdz);

            return new Vector3(fx, fy, fz);
        }
    }
}