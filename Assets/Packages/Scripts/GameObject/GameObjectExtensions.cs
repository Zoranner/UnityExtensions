//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-14 10:38:47
// Description: TODO >> This is a script Description.
//============================================================

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Zoranner.Engine.Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject standObject) where T : Component
        {
            if (standObject.GetComponent<T>() == null)
            {
                standObject.AddComponent<T>();
            }

            return standObject.GetComponent<T>();
        }

        private static GameObject _ObjectToTextureCamera;

        public static Texture2D TakeSnapshot(this GameObject standObject, Color backgroundColor)
        {
            if (!_ObjectToTextureCamera)
            {
                _ObjectToTextureCamera = new GameObject { name = "TextureCamera" };
            }

            if (!_ObjectToTextureCamera.GetComponent<Camera>())
            {
                var textureCamera = _ObjectToTextureCamera.AddComponent<Camera>();
                textureCamera.cullingMask = LayerMask.GetMask("Plane Object");
                textureCamera.clearFlags = CameraClearFlags.SolidColor;
                textureCamera.backgroundColor = backgroundColor;
            }

            var objectToTexture = _ObjectToTextureCamera.GetComponent<ObjectToTexture>();
            if (objectToTexture == null)
            {
                objectToTexture = _ObjectToTextureCamera.AddComponent<ObjectToTexture>();
            }

            //if (_Texture != null)
            //{
            //    var sprite = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height),
            //        new Vector2(0.5f, 0.5f));
            //    if (TargetImage != null)
            //    {
            //        TargetImage.sprite = sprite;
            //    }
            //}

            return objectToTexture.TakeObjectSnapshot(standObject);
        }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        /// <param name="standObject"></param>
        /// <returns></returns>
        public static Bounds CalculateBounds(this GameObject standObject)
        {
            var transform = standObject.transform;
            var renderers = transform.GetComponentsInChildren<Renderer>();

            var effective = false;
            var bounds = new Bounds();
            foreach (var renderer in renderers)
            {
                var ignored = renderer.GetComponent<IgnoreBounds>();
                var graphic = renderer.GetComponent<Graphic>();
                var particle = renderer.GetComponent<ParticleSystem>();

                if (!renderer.gameObject.activeInHierarchy || !renderer || ignored || graphic || particle)
                {
                    continue;
                }

                bounds = renderer.bounds;
                if (bounds.size == Vector3.zero && bounds.center != renderer.transform.position)
                {
                    bounds = TransformBounds(renderer.transform.localToWorldMatrix, bounds);
                }

                effective = true;
                break;
            }

            if (!effective)
            {
                return new Bounds(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
            }

            CalculateBounds(transform, ref bounds);
            if (bounds.extents == Vector3.zero)
            {
                bounds.extents = new Vector3(0.5f, 0.5f, 0.5f);
            }

            return bounds;
        }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        /// <param name="standTrans"></param>
        /// <param name="totalBounds"></param>
        private static void CalculateBounds(Transform standTrans, ref Bounds totalBounds)
        {
            if (standTrans == null)
            {
                throw new ArgumentNullException(nameof(standTrans));
            }

            foreach (Transform child in standTrans)
            {
                var renderer = child.GetComponent<Renderer>();
                var ignored = child.GetComponent<IgnoreBounds>();
                var graphic = child.GetComponent<Graphic>();
                var particle = child.GetComponent<ParticleSystem>();

                if (child.gameObject.activeInHierarchy && renderer && !ignored && !graphic && !particle)
                {
                    var bounds = renderer.bounds;
                    if (bounds.size == Vector3.zero && bounds.center != renderer.transform.position)
                    {
                        bounds = TransformBounds(renderer.transform.localToWorldMatrix, bounds);
                    }

                    totalBounds.Encapsulate(bounds.min);
                    totalBounds.Encapsulate(bounds.max);
                }

                CalculateBounds(child, ref totalBounds);
            }
        }

        /// <summary>
        /// 获取包围盒
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        private static Bounds TransformBounds(Matrix4x4 matrix, Bounds bounds)
        {
            var center = matrix.MultiplyPoint(bounds.center);

            var extents = bounds.extents;
            var axisX = matrix.MultiplyVector(new Vector3(extents.x, 0, 0));
            var axisY = matrix.MultiplyVector(new Vector3(0, extents.y, 0));
            var axisZ = matrix.MultiplyVector(new Vector3(0, 0, extents.z));

            extents.x = Mathf.Abs(axisX.x) + Mathf.Abs(axisY.x) + Mathf.Abs(axisZ.x);
            extents.y = Mathf.Abs(axisX.y) + Mathf.Abs(axisY.y) + Mathf.Abs(axisZ.y);
            extents.z = Mathf.Abs(axisX.z) + Mathf.Abs(axisY.z) + Mathf.Abs(axisZ.z);

            return new Bounds { center = center, extents = extents };
        }
    }
}