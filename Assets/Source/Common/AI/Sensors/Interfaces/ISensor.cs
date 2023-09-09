using System.Collections.Generic;
using UnityEngine;

namespace Source.Common.AI.Sensors.Interfaces
{
    public interface ISensor
    {
        float Range { get; }
        Collider2D Target { get; }
        IEnumerable<Collider2D> Targets { get; }
    }
}