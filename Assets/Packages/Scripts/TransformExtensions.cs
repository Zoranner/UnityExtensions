//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:40:53
// Description: TODO >> This is a script Description.
//============================================================

using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="standObject"></param>
        /// <returns></returns>
        public static bool IsPrefab(this GameObject standObject)
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                throw new InvalidOperationException("Does not work in edit mode");
            }

            return standObject.scene.buildIndex < 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="standTrans"></param>
        /// <returns></returns>
        [UsedImplicitly]
        public static bool IsPrefab(this Transform standTrans)
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                throw new InvalidOperationException("Does not work in edit mode");
            }

            return standTrans.gameObject.scene.buildIndex < 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="standTrans"></param>
        /// <returns></returns>
        public static PoseObject Pose(this Transform standTrans)
        {
            return new PoseObject(standTrans);
        }

        /// <summary>
        /// </summary>
        /// <param name="standTrans"></param>
        /// <returns></returns>
        public static PoseObject LocalPose(this Transform standTrans)
        {
            return new PoseObject(standTrans, true);
        }

        /// <summary>
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="standTrans"></param>
        /// <param name="isLocal"></param>
        public static void Apply(this Transform transform, Transform standTrans, bool isLocal = false)
        {
            if (!isLocal)
            {
                transform.position = standTrans.position;
                transform.rotation = standTrans.rotation;
                transform.localScale = standTrans.localScale;
            }
            else
            {
                transform.localPosition = standTrans.position;
                transform.localRotation = standTrans.rotation;
                transform.localScale = standTrans.localScale;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="poseObject"></param>
        /// <param name="isLocal"></param>
        public static void Apply(this Transform transform, PoseObject poseObject, bool isLocal = false)
        {
            if (!isLocal)
            {
                transform.position = poseObject.Position;
                transform.rotation = poseObject.Rotation;
                transform.localScale = poseObject.Scale;
            }
            else
            {
                transform.localPosition = poseObject.Position;
                transform.localRotation = poseObject.Rotation;
                transform.localScale = poseObject.Scale;
            }
        }
    }
    public class PoseObject
    {
        /// <summary>
        /// </summary>
        public PoseObject()
        {
            Position = new Vector3(0, 0, 0);
            Rotation = new Quaternion(0, 0, 0, 0);
            Scale = new Vector3(1, 1, 1);
        }

        /// <summary>
        /// </summary>
        /// <param name="t"></param>
        /// <param name="isLocal"></param>
        public PoseObject(Transform t, bool isLocal = false)
        {
            if (!isLocal)
            {
                Position = t.position;
                Rotation = t.rotation;
                Scale = t.localScale;
            }
            else
            {
                Position = t.localPosition;
                Rotation = t.localRotation;
                Scale = t.localScale;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        public PoseObject(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public Vector3 Position { get; }

        public Quaternion Rotation { get; }

        public Vector3 Scale { get; }

        /// <summary>
        /// </summary>
        [UsedImplicitly]
        public string FloatP10String()
        {
            var pos = $"{Position.x}, {Position.y}, {Position.z}";
            var rot = $"{Rotation.x}, {Rotation.y}, {Rotation.z}, {Rotation.w}";
            var scl = $"{Scale.x}, {Scale.y}, {Scale.z}";
            var str = $"({pos}, {rot}, {scl})";
            return str;
        }
    }
}