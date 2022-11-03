using System.Collections.Generic;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Core.Components.Towers.Abstractions
{
    public abstract class BaseTargetProvider<TTarget> : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayers;
        private readonly List<TTarget> targets = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!targetLayers.Includes(other.gameObject.layer))
                return;

            if (!other.TryGetComponent<TTarget>(out var target))
                return;

            targets.Add(target);
            ProvideTargets(targets);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!targetLayers.Includes(other.gameObject.layer))
                return;

            if (!other.TryGetComponent<TTarget>(out var target))
                return;

            targets.Remove(target);
            ProvideTargets(targets);
        }

        public abstract void ProvideTargets(IEnumerable<TTarget> targets);
    }
}