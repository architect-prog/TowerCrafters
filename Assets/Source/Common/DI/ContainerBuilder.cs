using System;
using System.Collections.Generic;
using Source.Common.Constants;
using Source.Common.DI.Interfaces;

namespace Source.Common.DI
{
    public class ContainerBuilder : IContainerBuilder
    {
        private readonly List<ServiceDescriptor> descriptors = new();

        public IContainerBuilder Register(ServiceDescriptor descriptor)
        {
            if (descriptor is null)
                throw new ArgumentNullException(nameof(descriptor));

            if (descriptor.Instance is null)
            {
                var message = string.Format(ExceptionConstants.CanNotRegisterDescriptor, descriptor.ServiceType);
                throw new InvalidOperationException(message);
            }

            descriptors.Add(descriptor);
            return this;
        }

        public IContainer Build()
        {
            var container = new Container(descriptors);
            return container;
        }
    }
}