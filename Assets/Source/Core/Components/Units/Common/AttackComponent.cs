using System.Linq;
using Source.Common.DI;
using Source.Core.Constants;
using Source.Kernel.Interfaces.Components;
using Source.Kernel.Interfaces.Providers;
using UnityEngine;

namespace Source.Core.Components.Units.Common
{
    public class AttackComponent : MonoBehaviour, IAttackComponent
    {
        [SerializeField] private LayerMask attackLayers;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange;
        [SerializeField] private float delayBeforeAttack;
        [SerializeField] private int targetLimit;

        private IDamageProvider damage;
        private IAttackFrequencyProvider attackFrequency;
        private ContactFilter2D contactFilter;
        private Collider2D[] targets;

        public float DelayBeforeAttack => delayBeforeAttack;
        public float AttackFrequency => attackFrequency.AttackFrequency;

        [Construct]
        public void Construct(
            IDamageProvider damageProvider,
            IAttackFrequencyProvider attackFrequencyProvider)
        {
            damage = damageProvider;
            attackFrequency = attackFrequencyProvider;

            targets = new Collider2D[targetLimit];
            contactFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = attackLayers
            };
        }

        public void Attack()
        {
            Physics2D.OverlapCircle(attackPoint.position, attackRange, contactFilter, targets);

            var damageableTargets = targets
                .Where(x => x)
                .Select(x => x.GetComponent<IHealthComponent>())
                .ToArray();

            foreach (var target in damageableTargets)
                target.ApplyDamage(damage.TotalDamage);
        }

        private void OnDrawGizmos()
        {
            if (!attackPoint)
                return;

            Gizmos.color = Colors.SemitransparentRed;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}