using UnityEngine;

namespace Source.Common.Utils
{
    public static class GizmosUtils
    {
        public static void DrawRect(Vector2 leftBottom, Vector2 size)
        {
            Gizmos.DrawLine(leftBottom, new Vector2(leftBottom.x + size.x, leftBottom.y));
            Gizmos.DrawLine(leftBottom, new Vector2(leftBottom.x, leftBottom.y + size.y));
            Gizmos.DrawLine(leftBottom + size, new Vector2(leftBottom.x + size.x, leftBottom.y));
            Gizmos.DrawLine(leftBottom + size, new Vector2(leftBottom.x, leftBottom.y + size.y));
        }

        public static void DrawRay(Vector2 startPosition, Vector2 direction, float length)
        {
            var weightedDirection = startPosition + direction.normalized * length;
            Gizmos.DrawLine(startPosition, weightedDirection);
        }
    }
}