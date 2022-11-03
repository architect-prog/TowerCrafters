using System;
using Source.Common.DI.Interfaces;
using UnityEngine;

namespace Source.Common.DI.Extensions
{
    public static class ContainerBuilderExtensions
    {
        public static IContainerBuilder Register<TService, TImplementation>(
            this IContainerBuilder containerBuilder,
            Func<TImplementation> instanceProvider) where TImplementation : TService
        {
            var descriptor = new ServiceDescriptor(typeof(TService), typeof(TImplementation), instanceProvider());
            return containerBuilder.Register(descriptor);
        }

        public static IContainerBuilder RegisterAsImplementedInterfaces<TImplementation>(
            this IContainerBuilder containerBuilder,
            Func<TImplementation> instanceProvider)
        {
            var instance = instanceProvider();
            var implementationType = instance.GetType();
            var implementedInterfaces = implementationType.GetInterfaces();

            foreach (var implementedInterface in implementedInterfaces)
            {
                var descriptor = new ServiceDescriptor(implementedInterface, implementationType, instance);
                containerBuilder.Register(descriptor);
            }

            return containerBuilder;
        }

        public static IContainerBuilder RegisterComponent<TService, TImplementation>(
            this IContainerBuilder containerBuilder,
            GameObject gameObject) where TImplementation : TService
        {
            if (!gameObject.TryGetComponent<TImplementation>(out var component))
            {
                component = gameObject.GetComponentInChildren<TImplementation>();
            }

            return containerBuilder
                .Register<TService, TImplementation>(() => component);
        }

        public static IContainerBuilder RegisterComponent<TImplementation>(
            this IContainerBuilder containerBuilder,
            GameObject gameObject)
        {
            return containerBuilder
                .RegisterComponent<TImplementation, TImplementation>(gameObject);
        }

        public static IContainerBuilder RegisterBehaviors(
            this IContainerBuilder containerBuilder,
            GameObject gameObject)
        {
            var components = gameObject.GetComponents(typeof(MonoBehaviour));
            foreach (var component in components)
            {
                var descriptor = new ServiceDescriptor(component.GetType(), component.GetType(), component);
                containerBuilder.Register(descriptor);
            }

            return containerBuilder;
        }

        public static IContainerBuilder RegisterBehaviorsAsImplementedInterfaces(
            this IContainerBuilder containerBuilder,
            GameObject gameObject)
        {
            var components = gameObject.GetComponents(typeof(MonoBehaviour));
            foreach (var component in components)
            {
                containerBuilder.RegisterAsImplementedInterfaces(() => component);
            }

            return containerBuilder;
        }
    }
}