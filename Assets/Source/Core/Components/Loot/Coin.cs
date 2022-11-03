using Source.Core.Components.Currency;
using Source.Core.Components.Currency.Interfaces;
using Source.Core.Components.Loot.Abstractions;
using UnityEngine;

namespace Source.Core.Components.Loot
{
    public class Coin : BaseCollectable, ICurrency
    {
        private readonly IWallet<Coin> coins = ApplicationRoot.Instance.Coins;

        [SerializeField] private int value;

        public int Value => value;

        public override void Collect()
        {
            var transaction = new Transaction<Coin>(this);
            coins.TryMakeTransaction(transaction);
            Destroy(gameObject);
        }
    }
}