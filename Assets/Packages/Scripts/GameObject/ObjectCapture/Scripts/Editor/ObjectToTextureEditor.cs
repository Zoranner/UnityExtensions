using UnityEngine;

using System.Collections;
using UnityEditor;

namespace Zoranner.Engine.Extensions
{
    [CustomEditor(typeof(ObjectToTexture))]
    public class ObjectImageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ObjectToTexture t = (ObjectToTexture)target;
            t.ObjectImageLayer = EditorGUILayout.LayerField("Object Image Layer", t.ObjectImageLayer);

            if (GUI.changed)
                EditorUtility.SetDirty(target);

            DrawDefaultInspector();
        }

    }
}
 