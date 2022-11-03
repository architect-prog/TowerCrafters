using Source.Common.Messaging;
using Source.Core.Components.Currency.Interfaces;
using Source.Core.Components.Loot;
using Source.Core.Constants;

namespace Source.Core.Components.Currency
{
    public class ObservableCoinWallet : IWallet<Coin>
    {
        private readonly IWallet<Coin> wallet;

        public int Balance => wallet.Balance;

        public ObservableCoinWallet(IWallet<Coin> wallet)
        {
            this.wallet = wallet;
        }

        public bool TryMakeTransaction(Transaction<Coin> transaction)
        {
            var isSuccess = wallet.TryMakeTransaction(transaction);
            if (isSuccess)
                MessageBus<int>.Broadcast(Messaging.Events.CoinTransactionCompleted, Balance);

            return isSuccess;
        }
    }
}