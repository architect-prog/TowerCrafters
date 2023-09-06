using System;
using UnityEngine;

namespace Source.UI.Providers.Interfaces
{
    public interface IInputProvider : IDisposable
    {
        Vector2 GetMovementDirection();
        Vector2 GetMouseClickPosition();
    }
}