using Source.Common.DI;
using Source.Core.Components.Defence.Interfaces;
using Source.Kernel.Interfaces.Components;
using Source.Kernel.Interfaces.Providers;
using UnityEngine;

namespace Source.Core.Components.Units.Enemies
{
    [RequireComponent(typeof(Collider2D))]
    public class ProtectorateAttackComponent : MonoBehaviour, IProtectorateAttackComponent
    {
        private IDeathComponent death;
        private IProtectorateDamageProvider damage;

        [Construct]
        public void Construct(
            IDeathComponent deathComponent,
            IProtectorateDamageProvider damageProvider)
        {
            death = deathComponent;
            damage = damageProvider;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<IProtectorate>(out var protectorate))
                return;

            protectorate.Damage(damage.ProtectorateDamage);
            death.Die();
        }
    }
}