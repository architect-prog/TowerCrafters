using UnityEngine;

namespace Source.Core.Extensions
{
    public static class ConversionExtensions
    {
        public static Vector2 ToVector2(this Vector2Int value)
        {
            var result = new Vector2(value.x, value.y);
            return result;
        }
    }
}