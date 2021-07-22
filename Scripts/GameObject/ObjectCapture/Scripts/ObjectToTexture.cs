using System;
using JetBrains.Annotations;
using Zoranner.Engine.Extensions;
using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public class ObjectToTexture : MonoBehaviour
    {
        // Use this for initialization
        public Camera Camera;
        public Vector3 DefaultPosition = new Vector3(0, 0, 1);
        public Vector3 DefaultRotation = new Vector3(-20, -50, 20);
        public Vector3 DefaultScale = new Vector3(0.9F, 0.9F, 0.9F);

        public bool DestroyScripts = true;

        [HideInInspector] public int ObjectImageLayer;

        public int SnapshotTextureHeight = 512;
        public int SnapshotTextureWidth = 512;

        [UsedImplicitly]
        private void Awake()
        {
            if (Camera == null)
            {
                Camera = GetComponent<Camera>();
            }

            ObjectImageLayer = LayerMask.NameToLayer("Plane Object");
        }

        private static void SetLayerRecursively(GameObject standObject, int layer)
        {
            foreach (var standTrans in standObject.GetComponentsInChildren<Transform>(true))
            {
                standTrans.gameObject.layer = layer;
            }
        }


        public Texture2D TakeObjectSnapshot(GameObject targetObject)
        {
            return TakeObjectSnapshot(targetObject, DefaultPosition, Quaternion.Euler(DefaultRotation),
                DefaultScale);
        }


        [UsedImplicitly]
        public Texture2D TakeObjectSnapshot(GameObject targetObject, Vector3 position)
        {
            return TakeObjectSnapshot(targetObject, position, Quaternion.Euler(DefaultRotation), DefaultScale);
        }


        public Texture2D TakeObjectSnapshot(GameObject targetObject, Vector3 position,
            Quaternion rotation, Vector3 scale)
        {
            // validate properties
            if (Camera == null)
            {
                throw new InvalidOperationException("Object Image Camera must be set");
            }

            if (ObjectImageLayer < 0 || ObjectImageLayer > 31)
            {
                throw new InvalidOperationException("Object Image Layer must specify a valid layer between 0 and 31");
            }


            // clone the specified game object so we can change its properties at will, and
            // position the object accordingly
            var isActive = targetObject.activeSelf;
            targetObject.SetActive(false);

            var standObject = Instantiate(targetObject, position, rotation * Quaternion.Inverse(targetObject.transform.rotation));
            if (DestroyScripts)
            {
                var scripts = standObject.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var script in scripts)
                {
                    DestroyImmediate(script);
                }
            }

            targetObject.SetActive(isActive);

            var renderers = standObject.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0)
            {
                return null;
            }

            standObject.SetActive(true);
            var bounds = standObject.CalculateBounds();
            var fov = Camera.fieldOfView * Mathf.Deg2Rad;
            var objSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
            var distance = Mathf.Abs(objSize / Mathf.Sin(fov / 1.2f));

            foreach (var r in renderers)
            {
                r.gameObject.SetActive(true);
            }

            position += bounds.center;

            Camera.transform.position = position - Camera.transform.forward * distance;
            Camera.orthographicSize = objSize;
            //gameObject.transform.localScale = scale;

            // set the layer so the render to texture camera will see the object 
            SetLayerRecursively(standObject, ObjectImageLayer);


            // get a temporary render texture and render the camera
            Camera.targetTexture = RenderTexture.GetTemporary(SnapshotTextureWidth, SnapshotTextureHeight, 24);
            Camera.Render();

            // activate the render texture and extract the image into a new texture
            var saveActive = RenderTexture.active;
            RenderTexture.active = Camera.targetTexture;
            var texture = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height);
            texture.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
            texture.Apply();

            RenderTexture.active = saveActive;

            // clean up after ourselves
            Camera.targetTexture = null;
            RenderTexture.ReleaseTemporary(Camera.targetTexture);
            DestroyImmediate(standObject);

            return texture;
        }
    }
}