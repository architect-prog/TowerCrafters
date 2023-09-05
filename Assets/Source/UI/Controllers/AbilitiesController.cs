using Source.Common.Messaging;
using Source.Core.Components.Abilities.Interfaces;
using Source.Core.Constants;
using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Controllers
{
    public class AbilitiesController : MonoBehaviour
    {
        private AbilityView viewPrefab;

        public void InitializeAbilities(IAbility[] abilities)
        {
            MessageBus<IAbility[]>.Broadcast(Messaging.Events.AbilitiesInitialized, abilities);
            foreach (var ability in abilities)
            {
                var view = Instantiate(viewPrefab, transform);
                view.Initialize(ability.Data.Icon, () =>
                {
                    view.StartCooldown(() => ability.Cooldown);
                    MessageBus<IAbility>.Broadcast(Messaging.Events.AbilityExecuted, ability);
                });
            }
        }
    }
}