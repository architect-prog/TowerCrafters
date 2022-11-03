namespace Source.Common.DI.Interfaces
{
    public interface IContainerBuilder
    {
        IContainerBuilder Register(ServiceDescriptor descriptor);
        IContainer Build();
    }
}