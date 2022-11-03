using System;

namespace Source.Common.DI
{
    public class ServiceDescriptor
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public object Instance { get; }

        public ServiceDescriptor(Type serviceType, Type implementationType, object instance)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Instance = instance;
        }
    }
}