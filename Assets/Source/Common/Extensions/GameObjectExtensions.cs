using System;
using UnityEngine;

namespace Source.Common.Extensions
{
    public static class GameObjectExtensions
    {
        public static void HandleAction<T>(this GameObject gameObject, Action<T> action)
        {
            var component = gameObject.GetComponent<T>();
            if (component != null)
            {
                action?.Invoke(component);
            }
        }
    }
}