using UnityEngine;

namespace Source.Core.Services.Interfaces
{
    public interface IObjectCreator
    {
        T Create<T>(T original, Vector2 position) where T : Object;
        T Create<T>(T original, Vector2 position, Quaternion quaternion) where T : Object;
        T[] CreateMany<T>(T original, Vector2[] positions) where T : Object;
    }
}