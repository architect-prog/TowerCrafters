using System;
using System.Collections.Generic;
using System.Linq;
using Source.Common.Constants;
using Source.Common.DI.Interfaces;

namespace Source.Common.DI
{
    public class Container : IContainer
    {
        private readonly IEnumerable<ServiceDescriptor> descriptors;

        public Container(IEnumerable<ServiceDescriptor> descriptors)
        {
            this.descriptors = descriptors;
        }

        public object Resolve(Type service)
        {
            var descriptor = GetDescriptor(service);
            if (descriptor is null)
                throw new InvalidOperationException(string.Format(ExceptionConstants.CanNotResolveService, service));

            var result = descriptor.Instance;
            return result;
        }

        public void Construct()
        {
            var objects = descriptors.Select(x => x.Instance).ToArray();
            foreach (var obj in objects)
            {
                Construct(obj);
            }
        }

        private void Construct(object obj)
        {
            var constructMethods = obj.GetType().GetMethods()
                .Where(x => Attribute.IsDefined(x, typeof(ConstructAttribute)));

            foreach (var constructMethod in constructMethods)
            {
                var constructArgs = constructMethod.GetParameters();

                var args = constructArgs
                    .Select(x => GetDescriptor(x.ParameterType).Instance)
                    .ToArray();

                constructMethod.Invoke(obj, args);
            }
        }

        private ServiceDescriptor GetDescriptor(Type type)
        {
            var descriptor = descriptors.FirstOrDefault(x => x.ServiceType == type);
            if (descriptor is null)
                throw new InvalidOperationException(string.Format(ExceptionConstants.CanNotResolveDescriptor, type));

            return descriptor;
        }
    }
}