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
    public class SoundManagerBgm
    {
        private const string bgmString = "Assets/Sound/BGM/";

        public static async UniTask PreloadAllBgm()
        {
            List<string> bgmNames = new List<string>()
            {
            };
            List<UniTask> preloadTasks = new List<UniTask>();
            foreach (var bgmName in bgmNames)
            {
                preloadTasks.Add(UtilResource.PreloadAudio(bgmString + bgmName));
            }
            await UniTask.WhenAll(preloadTasks);
            Log.DebugLog("All BGM preloaded");
        }

        //BGM
        public static void PlayBGM(string BGMName, bool IsLoop = true, float time = 0)
        {
            SoundMgr.Instance.CompressOrDecompressVolume(ConstSound.MixerExposedParameter.Sfx, true);
            SoundMgr.Instance.PlayBGM(UtilResource.GetAudioAddressable(bgmString + BGMName), ConstSound.MixerExposedParameter.Bgm, IsLoop, time);
        }

        public static void PlaySfx(string SfxName, bool IsLoop = true, float time = 0)
        {
            SoundMgr.Instance.PlayBGM(UtilResource.GetAudioAddressable(bgmString + SfxName), ConstSound.MixerExposedParameter.Sfx, IsLoop,time);
        }

        public static bool TryGetPlayTime(MixerExposedParameter parameter, out float time)
        {
            return SoundMgr.Instance.TryGetPlayTime(parameter, out time);
        }

        public static void PauseBGM()
        {
            SoundMgr.Instance.PauseBGM();
        }

        public static void ResumeBGM()
        {
            SoundMgr.Instance.RestartBGM();
        }

        public static bool IsStopping
        {
            get
            {
                return SoundMgr.Instance.IsStoppingBGM();
            }
        }


        public static void StopBGM(float changeTime = SoundMgr.longChangeTime)
        {
            SoundMgr.Instance.CompressOrDecompressVolume(ConstSound.MixerExposedParameter.Sfx, false);
            SoundMgr.Instance.StopBGM(changeTime, ConstSound.MixerExposedParameter.Bgm);
        }

        public static void StopSfx(float changeTime = SoundMgr.longChangeTime)
        {
            SoundMgr.Instance.StopBGM(changeTime, ConstSound.MixerExposedParameter.Sfx);
        }


        public static void ResetInterruptState()
        {
            SoundMgr.Instance.ResetInterruptState();
        }

        public static bool IsBGMInterrupted()
        {
            return SoundMgr.Instance.isInterrupted;
        }


        public static ResourceRequest PreloadBGM(string BGMName)
        {
            if (Application.isEditor && !System.IO.File.Exists("Assets/Resources/" + bgmString + BGMName + ".wav"))
            {
                Debug.LogError("Audioファイル：" + BGMName + ".wav が存在しません");
                return null;
            }
            else
            {
                return Resources.LoadAsync<AudioClip>(bgmString + BGMName);
            }
        }
    }
}
