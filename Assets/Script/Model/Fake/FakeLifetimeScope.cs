using VContainer;
using VContainer.Unity;
using gaw241201.Model.MasterData;

namespace gaw241201.Model
{
    public class FakeLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TemplateMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<FakeTest>();
        }
    }
}
