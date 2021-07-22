using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Zoranner.Engine.Extensions
{
    public class TakeSnapshot : MonoBehaviour
    {
        private Texture2D _Texture;
        public GameObject CameraPrefab;
        public Vector3 Scale = new Vector3(0.9f, 0.9f, 0.9f);
        public RawImage TargetImage;
        public GameObject TargetPrefab;

        [UsedImplicitly]
        private void Start()
        {
            if (TargetPrefab == null)
            {
                return;
            }

            TargetImage.texture = TargetPrefab.TakeSnapshot(Color.blue);
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            if (_Texture != null)
            {
                Destroy(_Texture);
            }
        }

        public Texture2D Texture()
        {
            var objectToTextureCamera = CameraPrefab != null ? Instantiate(CameraPrefab) : new GameObject();

            if (!objectToTextureCamera.GetComponent<Camera>())
            {
                var cam = objectToTextureCamera.AddComponent<Camera>();
                cam.orthographic = true;
                cam.orthographicSize = 1;
            }

            var objectToTexture = objectToTextureCamera.GetComponent<ObjectToTexture>();
            if (objectToTexture == null)
            {
                objectToTexture = objectToTextureCamera.AddComponent<ObjectToTexture>();
            }

            objectToTexture.DefaultScale = Scale;

            if (_Texture != null)
            {
                Destroy(_Texture);
            }

            _Texture = objectToTexture.TakeObjectSnapshot(TargetPrefab);
            //if (_Texture != null)
            //{
            //    var sprite = Sprite.Create(_Texture, new Rect(0, 0, _Texture.width, _Texture.height),
            //        new Vector2(0.5f, 0.5f));
            //    if (TargetImage != null)
            //    {
            //        TargetImage.sprite = sprite;
            //    }
            //}

            Destroy(objectToTextureCamera);
            return _Texture;
        }
    }
}