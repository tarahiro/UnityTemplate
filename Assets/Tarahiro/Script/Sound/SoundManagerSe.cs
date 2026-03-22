using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using static ConstSound;

namespace Tarahiro
{
    public class SoundManagerSe
    {
        private const string seString = "Sound/SE/";
        const string soundManagerPath = "Assets/SoundManager/";

        public static async UniTask PreloadAllSe()
        {
            var provider = SoundMgr.Instance.seMasterDataProvider;
            List<UniTask> preloadTasks = new List<UniTask>();
            for (int i = 0; i < provider.Count; i++)
            {
                preloadTasks.Add(UtilResource.PreloadAudio(provider.TryGetFromIndex(i).GetMaster().SePath));
            }
            await UniTask.WhenAll(preloadTasks);
        }

        //SE
        public static void PlaySE(string seLabel,int priority = 0, MixerLabel MixerName = MixerLabel.EventSE, bool IsLoop = false)
        {
            SoundMgr.Instance.GetOrder(seLabel, MixerName, priority);
            //SoundMgr.Instance.dummyInstantiate(UtilResource.GetResource<GameObject>("SoundManager/" + MixerDicionary[MixerName] + "Source"), UtilResource.GetResource<AudioClip>(SoundMgr.Instance.seMasterDataProvider.TryGetFromId(seLabel).GetMaster().SePath), IsLoop);
        }

        public static void PlaySEWithChangePitch(string seLabel, MixerLabel MixerName = MixerLabel.EventSE, bool IsLoop = false, float ChangePitch = SoundMgr.changePitch)
        { 
            SoundMgr.Instance.dummyInstantiateWithChangePitch(UtilResource.GetResourceAddressable<Component>(soundManagerPath + MixerDicionary[MixerName] + "Source", UtilResource.c_suffixPrefab), 
                UtilResource.GetAudioAddressable(SoundMgr.Instance.seMasterDataProvider.TryGetFromId(seLabel).GetMaster().SePath), IsLoop, ChangePitch);
        }

        public static void PlaySEWithLoop(string seLabel, MixerLabel MixerName = MixerLabel.EventSE)
        {
            SoundMgr.Instance.dummyInstantiateLoopSE(
                UtilResource.GetResourceAddressable<Component>(soundManagerPath + MixerDicionary[MixerName] + "Source", UtilResource.c_suffixPrefab), 
                UtilResource.GetAudioAddressable(SoundMgr.Instance.seMasterDataProvider.TryGetFromId(seLabel).GetMaster().SePath));
        }

        public void PlayRefleshSE(string seLabel, MixerLabel MixerName = MixerLabel.EventSE, bool IsLoop = false)
        {
            SoundMgr.Instance.dummyInstantiateRefleshSE(
                UtilResource.GetResourceAddressable<Component>(soundManagerPath + MixerDicionary[MixerName] + "Source", UtilResource.c_suffixPrefab),
                UtilResource.GetAudioAddressable(SoundMgr.Instance.seMasterDataProvider.TryGetFromId(seLabel).GetMaster().SePath), IsLoop);
        }

        public static void StopLoopSE()
        {
            SoundMgr.Instance.StopLoopSE();
        }


        public static void StopRefleshSE()
        {
            SoundMgr.Instance.StopRefleshSE();
        }

        public static void PauseLoopSE()
        {
            SoundMgr.Instance.PauseLoopSE();
        }

        public static void RestartLoopSE()
        {
            SoundMgr.Instance.RestartLoopSE();
        }

        public static void StopSoloLoopSE(AudioSource audioSource)
        {
            SoundMgr.Instance.StopSoloLoopSE(audioSource);
        }

    }
}
