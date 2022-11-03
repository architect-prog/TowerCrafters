using System;
using System.Linq;
using Source.Core.Components.Waypoints.Abstractions;
using UnityEngine;

namespace Source.Core.Components.Waypoints
{
    public class WaypointCollection : BaseWaypointCollection
    {
        [SerializeField] private Color color;

        private Waypoint[] waypoints;

        public override Waypoint First => waypoints.FirstOrDefault();
        public override Waypoint Last => waypoints.LastOrDefault();

        private void Awake()
        {
            waypoints = GetComponentsInChildren<Waypoint>();
        }

        public override Waypoint GetNextOrDefault(Waypoint current)
        {
            var currentIndex = Array.IndexOf(waypoints, current);
            if (currentIndex + 1 < waypoints.Length)
            {
                var result = waypoints[currentIndex + 1];
                return result;
            }

            return default;
        }

        public override Waypoint GetPreviousOrDefault(Waypoint current)
        {
            var currentIndex = Array.IndexOf(waypoints, current);
            if (currentIndex - 1 < 0)
            {
                var result = waypoints[currentIndex - 1];
                return result;
            }

            return default;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            waypoints = GetComponentsInChildren<Waypoint>();

            foreach (var waypoint in waypoints)
            {
                var currentPosition = waypoint.PointPosition;
                var nextPosition = GetNextOrDefault(waypoint)?.PointPosition;

                if (nextPosition.HasValue)
                {
                    Gizmos.DrawLine(currentPosition, nextPosition.Value);
                }
            }
        }
    }
}