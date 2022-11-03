using System;
using UnityEngine;

namespace Source.Core.Components.Abilities
{
    public class SpinningSlash : MonoBehaviour, IAbility
    {
        [SerializeField] private Collider2D detectionArea;

        public void Execute()
        {
            var contactFilter = new ContactFilter2D();
            //contactFilter.

            detectionArea.OverlapCollider(contactFilter, Array.Empty<Collider2D>());
        }
    }
}