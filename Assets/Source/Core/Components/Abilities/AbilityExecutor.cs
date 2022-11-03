using UnityEngine;

namespace Source.Core.Components.Abilities
{
    public class AbilityExecutor : MonoBehaviour
    {
        public void ExecuteAbility(IAbility ability)
        {
            ability.Execute();
        }
    }
}