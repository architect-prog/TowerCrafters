using System;
using Source.Core.Components.Units.Enemies;
using UnityEngine;

namespace Source.Core.Components.Scenarios
{
    [Serializable]
    public class EnemyGroup
    {
        [SerializeField] private int count;
        [SerializeField] private float spawnDelay;
        [SerializeField] private float afterGroupDelay;
        [SerializeField] private Enemy enemyPrefab;

        public int Count => count;
        public float SpawnDelay => spawnDelay;
        public float AfterGroupDelay => afterGroupDelay;
        public Enemy EnemyPrefab => enemyPrefab;

        public float TotalTime => count * spawnDelay + afterGroupDelay;
    }
}