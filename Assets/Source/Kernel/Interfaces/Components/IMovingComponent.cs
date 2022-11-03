using UnityEngine;

namespace Source.Kernel.Interfaces.Components
{
    public interface IMovingComponent
    {
        void Move(Vector2 direction);
    }
}