using Source.Core.Components.Projectiles.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Projectiles
{
    public class Beam : MonoBehaviour, IBeam
    {
        [SerializeField] private LineRenderer lineEffect;
        [SerializeField] private GameObject endOfBeam;

        private GameObject selectedTarget;
        private Vector2 pointTarget;

        private void Update()
        {
            if (!selectedTarget)
            {
                if (pointTarget == Vector2.zero)
                    return;

                endOfBeam.transform.position = pointTarget;
                endOfBeam.GetComponent<ParticleSystem>()?.Play();

                lineEffect.SetPosition(0, transform.position);
                lineEffect.SetPosition(1, pointTarget);
                return;
            }

            var targetPosition = selectedTarget.transform.position;
            endOfBeam.transform.position = targetPosition;

            lineEffect.SetPosition(0, transform.position);
            lineEffect.SetPosition(1, targetPosition);
        }

        public void SetTarget(GameObject target)
        {
            selectedTarget = target;
        }

        public void SetTarget(Vector2 target)
        {
            pointTarget = target;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}