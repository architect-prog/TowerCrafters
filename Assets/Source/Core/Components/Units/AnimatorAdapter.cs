using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Units
{
    public class AnimatorAdapter : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void Idle()
        {
            animator.SetBool(Animations.Enemy.IsMove, false);
        }

        public void Walk()
        {
            animator.SetBool(Animations.Enemy.IsMove, true);
        }

        public void Die()
        {
            animator.Play(Animations.Enemy.Die);
        }

        public void Attack()
        {
            animator.Play(Animations.Enemy.Attack);
        }

        public void Wound()
        {
            animator.Play(Animations.Enemy.Wound);
        }
    }
}