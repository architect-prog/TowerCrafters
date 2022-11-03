namespace Source.Core.Components.Defence.Interfaces
{
    public interface IProtectorate
    {
        int Durability { get; }
        void Damage(int damage);
        void Repair(int amount);
    }
}