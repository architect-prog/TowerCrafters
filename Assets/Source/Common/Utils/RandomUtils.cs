using UnityEngine;

namespace Source.Common.Utils
{
    public static class RandomUtils
    {
        public static bool IsDropped(float dropChance)
        {
            var normalizedDropChance = Mathf.Clamp01(dropChance);

            var calculatedChance = Random.Range(0f, 1f);
            var result = calculatedChance < normalizedDropChance;
            return result;
        }

        public static int GetDropCount(int minCount, int maxCount)
        {
            var result = Random.Range(minCount, maxCount + 1);
            return result;
        }
        
        public static Vector2 InsideCircle(Vector2 circleCenter, float radius)
        {
            var randomPosition = Random.insideUnitCircle * radius;
            var result = circleCenter - randomPosition;
            return result;
        }
    }
}