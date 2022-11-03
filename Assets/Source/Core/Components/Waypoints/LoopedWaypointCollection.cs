using System.Linq;
using Source.Common.Utils;
using Source.Core.Components.Waypoints.Abstractions;
using UnityEngine;

namespace Source.Core.Components.Waypoints
{
    public class LoopedWaypointCollection : BaseWaypointCollection
    {
        [SerializeField] private Color color;

        private LoopedCollection<Waypoint> waypoints;

        public override Waypoint First => waypoints.FirstOrDefault();
        public override Waypoint Last => waypoints.LastOrDefault();

        private void Awake()
        {
            waypoints = new LoopedCollection<Waypoint>(GetComponentsInChildren<Waypoint>());
        }

        public override Waypoint GetNextOrDefault(Waypoint current)
        {
            var currentIndex = waypoints.IndexOf(current);
            var result = waypoints[currentIndex + 1];
            return result;
        }

        public override Waypoint GetPreviousOrDefault(Waypoint current)
        {
            var currentIndex = waypoints.IndexOf(current);
            var result = waypoints[currentIndex - 1];
            return result;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = color;

            waypoints = new LoopedCollection<Waypoint>(GetComponentsInChildren<Waypoint>());
            foreach (var waypoint in waypoints)
            {
                var currentPosition = waypoint.PointPosition;
                var nextPosition = GetNextOrDefault(waypoint).PointPosition;
                Gizmos.DrawLine(currentPosition, nextPosition);
            }
        }
    }
}