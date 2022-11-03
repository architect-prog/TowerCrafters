using Source.Kernel.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Loot.Abstractions
{
    public abstract class BaseCollectable : MonoBehaviour, ICollectable
    {
        public abstract void Collect();
    }
}