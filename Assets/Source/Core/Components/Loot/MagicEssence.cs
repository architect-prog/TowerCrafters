using Source.Core.Components.Currency;
using Source.Core.Components.Currency.Interfaces;
using Source.Core.Components.Loot.Abstractions;
using UnityEngine;

namespace Source.Core.Components.Loot
{
    public class MagicEssence : BaseCollectable, ICurrency
    {
        private readonly IWallet<MagicEssence> essence = ApplicationRoot.Instance.Essence;

        [SerializeField] private int value;

        public int Value => value;

        public override void Collect()
        {
            var transaction = new Transaction<MagicEssence>(this);
            essence.TryMakeTransaction(transaction);
            Destroy(gameObject);
        }
    }
}