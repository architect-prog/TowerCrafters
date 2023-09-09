using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static TTarget GetNearestFor<TTarget>(this IEnumerable<TTarget> objects, Vector2 position)
            where TTarget : Component
        {
            var result = objects
                .MinBy(x => Vector3.Distance(position, x.transform.position));

            return result;
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var sourceArray = source.ToArray();
            if (!sourceArray.Any())
                return default;

            var min = sourceArray.FirstOrDefault();
            var minKey = keySelector.Invoke(min);
            foreach (var target in sourceArray)
            {
                var targetKey = keySelector.Invoke(target);
                if (targetKey.CompareTo(minKey) < 0)
                {
                    minKey = targetKey;
                    min = target;
                }
            }

            return min;
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var sourceArray = source.ToArray();
            if (!sourceArray.Any())
                return default;

            var max = sourceArray.FirstOrDefault();
            var maxKey = keySelector.Invoke(max);
            foreach (var target in sourceArray)
            {
                var targetKey = keySelector.Invoke(target);
                if (targetKey.CompareTo(maxKey) > 0)
                {
                    maxKey = targetKey;
                    max = target;
                }
            }

            return max;
        }

        public static TSource[] Clear<TSource>(this TSource[] source, TSource clearValue = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            for (var i = 0; i < source.Length; i++)
                source[i] = clearValue;

            return source;
        }
    }
}