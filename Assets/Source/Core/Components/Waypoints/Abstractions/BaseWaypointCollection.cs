using Source.Core.Components.Waypoints.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Waypoints.Abstractions
{
    public abstract class BaseWaypointCollection : MonoBehaviour, IWaypointCollection
    {
        public abstract Waypoint First { get; }
        public abstract Waypoint Last { get; }
        
        public abstract Waypoint GetNextOrDefault(Waypoint current);
        
        public abstract Waypoint GetPreviousOrDefault(Waypoint current);
    }
}