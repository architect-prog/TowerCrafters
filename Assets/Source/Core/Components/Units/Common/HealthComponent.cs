using System;
using Source.Common.DI;
using Source.Common.Editor;
using Source.Core.Extensions;
using Source.Kernel.Contracts;
using Source.Kernel.Interfaces.Components;
using Source.Kernel.Interfaces.Providers;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Core.Components.Units.Common
{
    public class HealthComponent : MonoBehaviour, IHealthComponent
    {
        [ReadOnly] [SerializeField] private bool isDead;
        [ReadOnly] [SerializeField] private float remainHealth;
        [SerializeField] private UnityEvent<float> healthChanged;

        private IResistProvider resist;
        private IHealthProvider health;

        public event Action died;
        public event Action healed;
        public event Action damageTaken;

        public float RemainHealth => remainHealth;
        public float MaxHealth => health.TotalMaxHealth;
        public float RemainHealthPercent => remainHealth / MaxHealth;

        [Construct]
        public void Construct(IHealthProvider healthProvider, IResistProvider resistProvider)
        {
            health = healthProvider;
            resist = resistProvider;
            remainHealth = healthProvider.TotalMaxHealth;
        }

        public void ApplyDamage(DamageAmount damage)
        {
            if (damage is null)
                throw new ArgumentNullException(nameof(damage));

            if (isDead)
                return;

            var resultDamage = damage.CalculateDamage(resist.TotalResist);
            remainHealth -= resultDamage;

            healthChanged?.Invoke(RemainHealthPercent);
            damageTaken?.Invoke();

            if (remainHealth <= 0)
            {
                isDead = true;
                died?.Invoke();
            }
        }

        public void Heal(float heal)
        {
            if (isDead)
                return;

            remainHealth += heal;
            if (remainHealth >= MaxHealth)
            {
                remainHealth = MaxHealth;
            }

            healthChanged?.Invoke(RemainHealthPercent);
            healed?.Invoke();
        }
    }
}