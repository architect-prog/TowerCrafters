using System.Collections.Generic;
using UnityEngine;

namespace Source.Common.AI.Sensors.Interfaces
{
    public interface ITargetProvider
    {
        GameObject Target { get; }
        IEnumerable<GameObject> Targets { get; }
    }
}