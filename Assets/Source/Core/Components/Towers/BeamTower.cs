using System.Linq;
using Source.Common.Utils;
using Source.Core.Components.Projectiles;
using Source.Core.Components.Towers.Abstractions;
using Source.Core.Components.Towers.Interfaces;
using Source.Core.Components.Units.Enemies;
using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Towers
{
    public class BeamTower : BaseTower<Enemy>
    {
        [SerializeField] private Beam beamPrefab;

        private ITargetSelector targetSelector;
        private RepeatingTimer timer;
        private Beam beam;

        private Enemy selectedEnemy;

        private void Start()
        {
            if (beamPrefab is null)
            {
                var message = string.Format(LoggingConstants.NotInitialized, nameof(Beam), name);
                logger.LogWarning(nameof(ProjectileTower), message);
                return;
            }

            beam = Instantiate(beamPrefab, transform.position, Quaternion.identity);
            beam.Disable();
        }

        private void OnEnable()
        {
            targetSelector = new SingleNearestTargetSelector(this);
            timer = new RepeatingTimer(this)
                .WithFrequency(Data.AttackRate)
                .WithAction(() => Attack());
        }

        private void OnDisable()
        {
            targetSelector.Dispose();
            timer.Dispose();
        }

        public override void Attack()
        {
            if (!selectedEnemy)
                return;

            selectedEnemy.HealthComponent.ApplyDamage(Data.Damage);
        }

        public override void UpdateTargets(params Enemy[] targets)
        {
            if (!targets.Any())
            {
                TargetOver();
                return;
            }

            var target = targetSelector.GetTarget(targets, selectedEnemy);

            TargetAppear(target);
        }

        private void TargetAppear(Enemy target)
        {
            if (beam is null)
                return;

            selectedEnemy = target;

            beam.Enable();
            beam.SetTarget(target.gameObject);
            timer.Start();
        }

        private void TargetOver()
        {
            if (beam is null)
                return;

            selectedEnemy = null;
            beam.Disable();
            timer.Stop();
        }
    }
}