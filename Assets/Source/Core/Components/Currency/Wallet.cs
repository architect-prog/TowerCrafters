using Source.Core.Components.Currency.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Currency
{
    public class Wallet<TCurrency> : IWallet<TCurrency> where TCurrency : ICurrency
    {
        private int balance;

        public int Balance => balance;

        public bool TryMakeTransaction(Transaction<TCurrency> transaction)
        {
            var amount = transaction.Amount;
            if (amount < 0 && balance < Mathf.Abs(amount))
                return false;

            balance += amount;
            return true;
        }
    }
}