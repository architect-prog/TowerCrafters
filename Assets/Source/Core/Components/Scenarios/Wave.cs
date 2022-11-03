using System;
using UnityEngine;

namespace Source.Core.Components.Scenarios
{
    [Serializable]
    public class Wave
    {
        [SerializeField] private float delayBefore;
        [SerializeField] private float delayAfter;
        [SerializeField] private EnemyWaves[] enemyWaves;

        public float DelayBefore => delayBefore;
        public float DelayAfter => delayAfter;
        public EnemyWaves[] EnemyWaves => enemyWaves;
    }
}