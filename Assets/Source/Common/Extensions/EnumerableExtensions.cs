﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static TTarget GetNearestFor<TTarget, TSource>(this IEnumerable<TTarget> targets, TSource source)
            where TTarget : MonoBehaviour
            where TSource : MonoBehaviour
        {
            var result = targets
                .MinBy(x => Vector3.Distance(source.transform.position, x.transform.position));

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
    }
}