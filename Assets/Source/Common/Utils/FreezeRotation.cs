using UnityEngine;

namespace Source.Common.Utils
{
    public class FreezeRotation : MonoBehaviour
    {
        private Quaternion startRotation;

        private void Start()
        {
            startRotation = transform.rotation;
        }

        private void LateUpdate()
        {
            transform.rotation = startRotation;
        }
    }
}