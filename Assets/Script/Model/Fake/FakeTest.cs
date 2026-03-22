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
        [Inject] ITemplateMasterDataProvider masterDataProvider;
        public void Start()
        {
            var masterData = masterDataProvider.TryGetFromId("Enter4").GetMaster();
            Debug.Log(masterData.FakeDescription);
        }

    }
    #endif
}