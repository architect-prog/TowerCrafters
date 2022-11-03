using System;
using Source.Common.Animation;
using Source.Common.Extensions;
using Source.Kernel.Interfaces.Components;
using Source.Kernel.Interfaces.Providers;
using UnityEngine;

namespace Source.Core.Components.Units.Characters
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class ItemAttractionComponent : MonoBehaviour, IItemAttractionComponent
    {
        [SerializeField] private float attractionTime;
        [SerializeField] private LayerMask attractedItems;

        private CircleCollider2D attractionArea;

        public void Construct(ICollectingRangeProvider collectingRangeProvider)
        {
            if (collectingRangeProvider == null)
                throw new ArgumentNullException(nameof(collectingRangeProvider));

            attractionArea = GetComponent<CircleCollider2D>();

            attractionArea.radius = collectingRangeProvider.CollectingRange;
        }

        public void Attract(GameObject other)
        {
            AnimationSequence.Create(this)
                .AddAnimation(new MoveToTransformAnimation(other.transform, transform, attractionTime))
                .Execute();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!attractedItems.Includes(other.gameObject.layer))
                return;

            Attract(other.gameObject);
        }
    }
}