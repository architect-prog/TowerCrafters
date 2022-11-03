namespace Source.Core.Components.Currency.Interfaces
{
    public interface IWallet<TCurrency> where TCurrency : ICurrency
    {
        int Balance { get; }

        bool TryMakeTransaction(Transaction<TCurrency> transaction);
    }
}