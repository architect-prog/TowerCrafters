using UnityEngine;

namespace Source.Core.Sandbox
{
    public class LifeTimeRecorder : MonoBehaviour
    {
        private float lifeTimeStart;

        private void Start()
        {
            lifeTimeStart = Time.time;
        }

        private void OnDestroy()
        {
            Debug.Log(Time.time - lifeTimeStart);
        }
    }
}