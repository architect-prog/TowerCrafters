using Source.Common.DI.Interfaces;

namespace Source.Common.DI.Extensions
{
    public static class ContainerExtensions
    {
        public static TService Resolve<TService>(this IContainer container) where TService : class
        {
            var result = container.Resolve(typeof(TService)) as TService;
            return result;
        }
    }
}