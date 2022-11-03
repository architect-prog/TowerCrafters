using System;

namespace Source.Common.DI.Interfaces
{
    public interface IContainer
    {
        object Resolve(Type service);
        void Construct();
    }
}