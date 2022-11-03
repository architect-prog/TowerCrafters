using System.Collections;
using Source.Common.AI.Interfaces;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.States
{
    public class DieState : IState
    {
        private readonly AnimatorAdapter animatorAdapter;
        private readonly IDeathComponent deathComponent;
        private readonly ILootBag lootBag;

        private bool isDead;

        public DieState(
            AnimatorAdapter animatorAdapter,
            IDeathComponent deathComponent,
            ILootBag lootBag)
        {
            this.lootBag = lootBag;
            this.deathComponent = deathComponent;
            this.animatorAdapter = animatorAdapter;
        }

        public void Enter()
        {
            lootBag.DropLoot();
            animatorAdapter.Die();
        }

        public IEnumerator Update()
        {
            yield return new WaitForSeconds(0.4f);
            deathComponent.Die();
        }

        public void Exit()
        {
        }
    }
}