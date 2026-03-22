using VContainer;
using VContainer.Unity;

namespace Tarahiro.Inject
{
    public class DebugLifetimeScope : LifetimeScope
    {
    #if ENABLE_DEBUG
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<DebugManagerCore>();
        }
    #endif
    }
}
