using UnityEngine;

namespace Source.Core.Sandbox
{
    public class FrameRateLimiter : MonoBehaviour
    {
        [SerializeField] private int frameRateLimit;

        private void Awake()
        {
            Application.targetFrameRate = frameRateLimit;
        }
    }
}