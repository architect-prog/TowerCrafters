using Source.Core.Components.Projectiles.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Projectiles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleEffect : MonoBehaviour, IEffect
    {
        private ParticleSystem particles;

        private void Start()
        {
            particles = GetComponent<ParticleSystem>();
        }

        public void SetRadius(float radius)
        {
            particles ??= GetComponent<ParticleSystem>();

            var shape = particles.shape;
            shape.radius = radius;
        }

        public void Show()
        {
            particles.Play();
        }
    }
}