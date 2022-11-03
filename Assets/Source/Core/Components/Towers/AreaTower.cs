using System.Linq;
using Source.Common.Utils;
using Source.Core.Components.Projectiles;
using Source.Core.Components.StatusEffects.Common;
using Source.Core.Components.Towers.Abstractions;
using Source.Core.Components.Units.Enemies;
using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Towers
{
    public class AreaTower : BaseTower<Enemy>
    {
        [SerializeField] private ParticleEffect effect;

        private RepeatingTimer timer;

        private Enemy[] selectedEnemies;

        private void Start()
        {
            if (effect is null)
            {
                var message = string.Format(LoggingConstants.NotInitialized, nameof(ParticleEffect), name);
                logger.LogWarning(nameof(ProjectileTower), message);
                return;
            }

            effect.SetRadius(Data.AttackRange);
        }

        private void OnEnable()
        {
            timer = new RepeatingTimer(this)
                .WithFrequency(Data.AttackRate)
                .WithAction(() => Attack())
                .InvokeImmediately(true);
        }

        private void OnDisable()
        {
            timer.Dispose();
        }

        public override void Attack()
        {
            if (effect is null)
                return;

            effect.Show();
            foreach (var enemy in selectedEnemies)
            {
                enemy.HealthComponent.ApplyDamage(Data.Damage);
                enemy.ApplyEffect(new TemporaryStatusEffect(0.8f, new SpeedStatusEffect(-1, enemy.Data.Speed)));
            }
        }

        public override void UpdateTargets(params Enemy[] targets)
        {
            if (!targets.Any())
            {
                timer.Stop();
                return;
            }

            selectedEnemies = targets;
            timer.Start();
        }
    }
}