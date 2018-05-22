using System;
using Beehouse.FrameworkStandard.Services;
using Microsoft.Practices.Unity;

namespace Beehouse.FrameworkStandard
{
    public static class PrismUnityExtensions
    {
        public static IUnityContainer UseBeehouse(this IUnityContainer container)
        {
            container.UseBeehouse(null);
            return container;
        }

        public static IUnityContainer UseBeehouse(this IUnityContainer container, Action<IUnityContainer> options)
        {
            options?.Invoke(container);
            Defaults().Invoke(container);
            return container;
        }

        private static Action<IUnityContainer> Defaults()
        {
            return c =>
            {
                c.RegisterType(typeof(IStandardService<>), typeof(StandardService<>));
            };
        }
    }
}