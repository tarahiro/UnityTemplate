using VContainer;
using VContainer.Unity;
using gaw241201.Model.MasterData;
using gaw241201.Model;

namespace gaw241201
{
    public class FakeLifetimeScope : LifetimeScope
    {
#if ENABLE_DEBUG
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TemplateMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<FakeTest>();
        }
    #endif
    }
}
