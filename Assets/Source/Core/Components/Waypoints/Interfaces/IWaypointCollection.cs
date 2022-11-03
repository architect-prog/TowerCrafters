namespace Source.Core.Components.Waypoints.Interfaces
{
    public interface IWaypointCollection
    {
        Waypoint First { get; }
        Waypoint Last { get; }
        Waypoint GetNextOrDefault(Waypoint current);
        Waypoint GetPreviousOrDefault(Waypoint current);
    }
}