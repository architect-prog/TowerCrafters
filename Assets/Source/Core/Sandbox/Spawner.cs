using Source.Common.Utils;
using UnityEngine;

namespace Source.Core.Sandbox
{
    public class Spawner : MonoBehaviour
    {
        public int Count;
        public float AreaRadius;
        public GameObject Prefab;
        public float delay;

        private void Start()
        {
            new Timer(this)
                .WithDelay(delay)
                .WithAction(() =>
                {
                    for (var i = 0; i < Count; i++)
                    {
                        var position = RandomUtils.InsideCircle(transform.position, AreaRadius);
                        ApplicationRoot.Instance.ObjectCreator.Create(Prefab, position);
                    }
                })
                .Start();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, AreaRadius);
        }
    }
}