using System;
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

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> targets, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            var targetArray = targets?.ToArray() ?? Array.Empty<TSource>();
            if (!targetArray.Any())
            {
                return default;
            }

            var min = targetArray.FirstOrDefault();
            var minKey = keySelector.Invoke(min);
            foreach (var target in targetArray)
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
    }
}