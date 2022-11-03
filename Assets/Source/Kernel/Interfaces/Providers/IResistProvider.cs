using Source.Kernel.Contracts;

namespace Source.Kernel.Interfaces.Providers
{
    public interface IResistProvider
    {
        ResistAmount TotalResist { get; }
    }
}