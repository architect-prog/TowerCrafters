using Source.Common.Messaging;
using Source.Core.Components.Defence.Interfaces;
using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Defence
{
    public class ObservableProtectorate : MonoBehaviour, IProtectorate
    {
        [SerializeField] private Protectorate protectorate = new();

        public int Durability => protectorate.Durability;

        private void Start()
        {
            Repair(protectorate.MaxDurability);
        }

        public void Damage(int damage)
        {
            protectorate.Damage(damage);
            MessageBus<int>.Broadcast(Messaging.Events.ProtectorateDurabilityUpdated, Durability);

            if (Durability < 0)
                MessageBus.Broadcast(Messaging.Events.ProtectorateDestroyed);
        }

        public void Repair(int amount)
        {
            protectorate.Repair(amount);
            MessageBus<int>.Broadcast(Messaging.Events.ProtectorateDurabilityUpdated, Durability);
        }
    }
}