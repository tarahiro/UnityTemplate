using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Tarahiro.View
{
public class SmaLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SmaPublisher>(Lifetime.Singleton).AsSelf();
            builder.Register<SmaCoreFactory>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<SmaImageFactory>(Lifetime.Transient).AsImplementedInterfaces();

            builder.RegisterEntryPoint<SmaTicker>();
        }
    }
}
