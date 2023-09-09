using Source.Core.Components.Loot.Abstractions;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Characters
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class ItemCollectionComponent : MonoBehaviour, IItemCollectionComponent
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<BaseCollectable>(out var collectable))
                return;

            collectable.Collect();
        }
    }
}