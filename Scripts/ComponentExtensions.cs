using UnityEngine;

namespace Zoranner.Engine.Extensions
{
    public static class ComponentExtensions
    {
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            if (component.GetComponent<T>() == null)
            {
                component.gameObject.AddComponent<T>();
            }

            return component.GetComponent<T>();
        }
    }
}