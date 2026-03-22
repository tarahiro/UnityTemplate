using Tarahiro.Sound;
using Tarahiro;
using gaw241201.Model;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    #if ENABLE_DEBUG
    public class FakeTest : IStartable
    {
        [Inject] ITemplateMasterDataProvider seMasterDataProvider;
        public void Start()
        {
            var seMasterData = seMasterDataProvider.TryGetFromId("Enter3").GetMaster();
            Debug.Log(seMasterData.FakeDescription);
        }

    }
    #endif
}