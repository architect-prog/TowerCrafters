using Source.Common.Utils;
using UnityEngine;

namespace Source.Core.Sandbox
{
    public class AreaSpawner : MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private float delay;
        [SerializeField] private float areaRadius;
        [SerializeField] private GameObject prefab;

        private void Start()
        {
            new Timer(this)
                .WithDelay(delay)
                .WithAction(() =>
                {
                    for (var i = 0; i < count; i++)
                    {
                        var position = RandomUtils.InsideCircle(transform.position, areaRadius);
                        ApplicationRoot.Instance.ObjectCreator.Create(prefab, position);
                    }
                })
                .Start();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, areaRadius);
        }
    }
}