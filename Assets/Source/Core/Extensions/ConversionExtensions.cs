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

        public static Vector2Int ToVector2Int(this Vector2 value)
        {
            var result = new Vector2Int(value.x.Floor(), value.y.Floor());
            return result;
        }

        public static int Floor(this float value)
        {
            var result = Mathf.FloorToInt(value);
            return result;
        }
    }
}