using Source.Core.Components.Currency.Interfaces;

namespace Source.Core.Components.Currency
{
    public class Transaction<TCurrency> where TCurrency : ICurrency
    {
        private int amount;

        public int Amount => amount;

        public Transaction(TCurrency currency)
        {
            amount = currency.Value;
        }
    }
}