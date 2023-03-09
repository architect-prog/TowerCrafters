using Source.Common.Extensions;
using Source.Core.Components.Abilities.Interfaces;
using Source.Kernel.Contracts;
using Source.Kernel.Data;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Abilities
{
    [RequireComponent(typeof(Collider2D))]
    public class AreaAbility : MonoBehaviour, IAbility
    {
        [SerializeField] private int maxTargetsCount;
        [SerializeField] private LayerMask scanningMask;
        [SerializeField] private Collider2D detectionArea;
        [SerializeField] private DamageAmount damage;
        [SerializeField] private AbilityData data;

        private Collider2D[] detectedTargets;
        private ContactFilter2D contactFilter;
        private float cooldown;

        public AbilityData Data => data;
        public float Cooldown => cooldown;

        private void Start()
        {
            detectedTargets = new Collider2D[maxTargetsCount];
            contactFilter = new ContactFilter2D
            {
                useTriggers = true,
                useLayerMask = true,
                layerMask = scanningMask
            };
        }

        public void Execute()
        {
            var numOfTargets = detectionArea.Overlap(contactFilter, detectedTargets);
            for (var i = 0; i < numOfTargets; i++)
            {
                detectedTargets[i]
                    .gameObject
                    .HandleAction<IHealthComponent>(x => x.ApplyDamage(damage));
            }
        }
    }
}