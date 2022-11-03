using System.Collections;
using System.Linq;
using Source.Common.Extensions;
using Source.Common.Utils;
using Source.Core.Components.Units.Enemies;
using Source.Core.Components.Waypoints.Abstractions;
using Source.Core.Services.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Scenarios
{
    [RequireComponent(typeof(Scenario))]
    public class Spawner : MonoBehaviour
    {
        private readonly IObjectCreator creator = ApplicationRoot.Instance.ObjectCreator;

        private Scenario scenario;
        private Timer timer;

        private void Start()
        {
            scenario = GetComponent<Scenario>();
            StartCoroutine(StartWaves(scenario.Waves));
        }

        private IEnumerator StartWaves(Wave[] waves)
        {
            foreach (var wave in waves)
            {
                yield return new WaitForSeconds(wave.DelayBefore);
                yield return StartCoroutine(SpawnEnemyWaves(wave.EnemyWaves));
                yield return new WaitForSeconds(wave.DelayAfter);
            }
        }

        private IEnumerator SpawnEnemyWaves(EnemyWaves[] enemyWaves)
        {
            foreach (var enemyWave in enemyWaves)
            {
                StartCoroutine(SpawnEnemyGroups(enemyWave.EnemyGroups, enemyWave.GroupsPath));
            }

            var maxWaveTime = enemyWaves.Max(x => x.maxTotalTime);
            yield return new WaitForSeconds(maxWaveTime);
        }

        private IEnumerator SpawnEnemyGroups(EnemyGroup[] enemyGroups, BaseWaypointCollection path)
        {
            foreach (var group in enemyGroups)
            {
                yield return StartCoroutine(SpawnEnemyGroup(group, path));
            }
        }

        private IEnumerator SpawnEnemyGroup(EnemyGroup group, BaseWaypointCollection path)
        {
            var startPoint = path.First;
            for (var i = 0; i < group.Count; i++)
            {
                yield return new WaitForSeconds(group.SpawnDelay);

                var enemy = creator.Create(group.EnemyPrefab, startPoint.PointPosition);
                enemy.gameObject.HandleAction<WaypointMovingComponent>(x => x.UpdatePath(path));
            }

            yield return new WaitForSeconds(group.AfterGroupDelay);
        }
    }
}