using Source.Common.Animation;
using Source.Common.Extensions;
using Source.Core.Components.Projectiles.Interfaces;
using Source.Kernel.Contracts;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Projectiles
{
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;
        [SerializeField] private LayerMask damageTarget;

        private DamageAmount damage;
        private bool didAppliedDamage;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        public void MoveTo(Transform target, DamageAmount damageAmount)
        {
            damage = damageAmount;
            ParallelAnimationSequence.Create(this)
                .AddAnimation(new LookToAnimation(transform, target))
                .AddAnimation(new MoveToTransformBySpeedAnimation(transform, target, speed))
                .Execute();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!damageTarget.Includes(other.gameObject.layer))
                return;

            if (didAppliedDamage)
                return;

            other.gameObject.HandleAction<IHealthComponent>(x => x.ApplyDamage(damage));
            didAppliedDamage = true;

            Destroy(gameObject);
        }
    }
}