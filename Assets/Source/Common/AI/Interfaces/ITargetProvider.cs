using System.Collections.Generic;
using UnityEngine;

namespace Source.Common.AI.Interfaces
{
    public interface ITargetProvider
    {
        GameObject Target { get; }
        IEnumerable<GameObject> Targets { get; }
    }

    public interface ITargetProvider<out T> where T : MonoBehaviour
    {
        T Target { get; }
        IEnumerable<T> Targets { get; }
    }
}