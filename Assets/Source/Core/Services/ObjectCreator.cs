using System.Linq;
using Source.Core.Services.Interfaces;
using UnityEngine;

namespace Source.Core.Services
{
    public class ObjectCreator : IObjectCreator
    {
        public T Create<T>(T original, Vector2 position) where T : Object
        {
            var result = Object.Instantiate(original, position, Quaternion.identity);
            return result;
        }

        public T Create<T>(T original, Vector2 position, Quaternion quaternion) where T : Object
        {
            var result = Object.Instantiate(original, position, quaternion);
            return result;
        }

        public T[] CreateMany<T>(T original, Vector2[] positions) where T : Object
        {
            var result = positions
                .Select(x => Create(original, x))
                .ToArray();

            return result;
        }
    }
}