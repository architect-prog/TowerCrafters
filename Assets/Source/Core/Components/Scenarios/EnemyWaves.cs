using System;
using System.Linq;
using Source.Core.Components.Waypoints.Abstractions;
using UnityEngine;

namespace Source.Core.Components.Scenarios
{
    [Serializable]
    public class EnemyWaves
    {
        [SerializeField] private EnemyGroup[] enemyGroups;
        [SerializeField] private BaseWaypointCollection groupsPath;

        public EnemyGroup[] EnemyGroups => enemyGroups;
        public BaseWaypointCollection GroupsPath => groupsPath;
        public float maxTotalTime => enemyGroups.Max(x => x.TotalTime);
    }
}