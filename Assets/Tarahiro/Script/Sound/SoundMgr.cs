using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tarahiro;
using Tarahiro.Sound;
using UniRx;
using UnityEngine;
using static ConstSound;

namespace Tarahiro
{

    internal class SoundMgr : SingletonMonoBehaviour<SoundMgr>
    {
        public const float minimumDecibel = -80f;
        public static readonly float[] initialDecibel = new float[5] { -10, 0, -5, -5, 0 };

        //設定数
        public const float shortChangeTime = .3f;
        public const float longChangeTime = 1f;
        public const float correctAbsDecibel = 15f;
        public const float smallNumber = 0.00001f;
        public const float changePitch = .3f;

        public ISeMasterDataProvider seMasterDataProvider;
        const string soundManagerPath = "Assets/SoundManager/";


        protected override void Awake()
        {
            base.Awake();
            gameObject.AddComponent<AudioListener>();

            // Instantiate の呼び出しを安全にするため InstantiatePrefab を使う
            _bgmSourceMain = InstantiatePrefab(UtilResource.GetResourceAddressable<Component>(soundManagerPath + "BGMSource", UtilResource.c_suffixPrefab), transform)?.GetComponent<AudioSource>();
            _sfxSource = InstantiatePrefab(UtilResource.GetResourceAddressable<Component>(soundManagerPath + "BGMSubSource", UtilResource.c_suffixPrefab), transform)?.GetComponent<AudioSource>();

            foreach (MixerExposedParameter mep in Enum.GetValues(typeof(MixerExposedParameter)))
            {
                var instance = InstantiatePrefab(UtilResource.GetResourceAddressable<MixerVolumeClass>(soundManagerPath + "MixerVolumeClass/MixerVolumeClass", UtilResource.c_suffixPrefab), transform)
                    ?.GetComponent<MixerVolumeClass>();
                if (instance == null)
                {
                    Debug.LogError($"SoundMgr.Awake: Failed to instantiate MixerVolumeClass for {mep}");
                    continue;
                }
                instance.Construct(ConstSound.MixerExposedParameterNameDictionary[mep]);
                mixerVolumeClass.Add(mep, instance);
            }
            foreach (MixerExposedParameter mep in Enum.GetValues(typeof(MixerExposedParameter)))
            {
                SetVolume(mep, MixerVolumeClass.INITIAL_LEVEL);
            }
            //VolumeLevel初期化
            for (int i = 0; i < System.Enum.GetNames(typeof(MixerExposedParameter)).Length; i++)
            {
                SetVolume((MixerExposedParameter)i, MixerVolumeClass.INITIAL_LEVEL);
            }

            //SeDictionaryロード
            seMasterDataProvider = new SeMasterDataProvider();
        }

        // 安全な Instantiate ヘルパー: prefab は GameObject か Component のどちらでも受け取る
        GameObject InstantiatePrefab(UnityEngine.Object prefab, Transform parent)
        {
            if (prefab == null)
            {
                Debug.LogError("SoundMgr.InstantiatePrefab: prefab is null. Make sure the addressable/prefab path is correct and the asset exists.");
                return null;
            }

            if (prefab is GameObject goPrefab)
            {
                return Instantiate(goPrefab, parent);
            }
            else if (prefab is Component compPrefab)
            {
                if (compPrefab.gameObject == null)
                {
                    Debug.LogError($"SoundMgr.InstantiatePrefab: Component prefab has no GameObject: {prefab}");
                    return null;
                }
                return Instantiate(compPrefab.gameObject, parent);
            }
            else
            {
                Debug.LogError($"SoundMgr.InstantiatePrefab: Unsupported prefab type {prefab.GetType()}. Expected GameObject or Component.");
                return null;
            }
        }

        List<SeProperty> properties = new List<SeProperty>();

        private void Update()
        {
            if (properties.Count > 0)
            {
                SeProperty property = properties.First();

                for (int i = 1; i < properties.Count; i++)
                {
                    SeProperty removedPropety;

                    if (property.priority <= properties[i].priority)
                    {
                        Log.DebugAssert(!(property.priority == properties[i].priority && property.SeLabel != properties[i].SeLabel),
                            property.SeLabel + "と" + properties[i].SeLabel + "が同じ優先度です");

                        removedPropety = property;
                        property = properties[i];
                    }
                    else
                    {
                        removedPropety = properties[i];
                    }

                    Log.DebugWarning(removedPropety.SeLabel + "は重複のため鳴りません");
                }


                var mixerInstance = UtilResource.GetResourceAddressable<Component>(soundManagerPath + MixerDicionary[property.MixerLabel] + "Source", UtilResource.c_suffixPrefab);
                Log.DebugAssert(mixerInstance != null, "MixerInstance is null for " + property.MixerLabel);

                var clipInstance = UtilResource.GetAudioAddressable(SoundMgr.Instance.seMasterDataProvider.TryGetFromId(property.SeLabel).GetMaster().SePath);
                Log.DebugAssert(clipInstance != null, "ClipInstance is null for " + property.SeLabel);

                dummyInstantiate(mixerInstance, clipInstance, false);

                properties = new List<SeProperty>();
            }

        }

        private AudioSource _bgmSourceMain;
        public AudioSource BGMSourceMain => _bgmSourceMain;

        private AudioSource _sfxSource;
        public AudioSource SfxSource => _sfxSource;

        private List<Component> loopSEList = new List<Component>();
        private List<Component> refleshSEList = new List<Component>();
        static Dictionary<MixerExposedParameter, MixerVolumeClass> mixerVolumeClass = new Dictionary<MixerExposedParameter, MixerVolumeClass>();

        public bool isInterrupted { get; private set; }


        public void PlayBGM(AudioClip audioClip, MixerExposedParameter parameter, bool IsLoop = true, float time = 0)
        {
            ExecuteToPlayBGM(audioClip, parameter, IsLoop, time);
        }

        public void ResetInterruptState()
        {
            isInterrupted = false;
        }

        public bool TryGetPlayTime(MixerExposedParameter parameter, out float time)
        {
            AudioSource t_audioSource;
            switch (parameter)
            {
                case MixerExposedParameter.Bgm:
                    t_audioSource = _bgmSourceMain;
                    break;
                case MixerExposedParameter.Sfx:
                    t_audioSource = _sfxSource;
                    break;
                default:
                    Log.DebugAssert(false, "Invalid MixerExposedParameter for GetPlayTime: " + parameter);
                    t_audioSource = _bgmSourceMain;
                    break;
            }

            if (t_audioSource.clip != null)
            {
                time = t_audioSource.time;
                return true;
            }
            else
            {
                time = 0f;
                return false;
            }
        }

        void ExecuteToPlayBGM(AudioClip audioClip, MixerExposedParameter parameter, bool IsLoop, float time)
        {
            AudioSource t_audioSource;

            switch (parameter)
            {
                case MixerExposedParameter.Bgm:
                    t_audioSource = _bgmSourceMain;
                    break;
                case MixerExposedParameter.Sfx:
                    t_audioSource = _sfxSource;
                    break;
                default:
                    Log.DebugAssert(false, "Invalid MixerExposedParameter for PlayBGM: " + parameter);
                    t_audioSource = _bgmSourceMain;
                    break;
            }
            t_audioSource.clip = audioClip;
            t_audioSource.loop = IsLoop;
            t_audioSource.Play();
            t_audioSource.time = time;
            Mute(parameter, false, 0);
            CompressOrDecompressVolume(parameter, false, 0);
        }

        public Component dummyInstantiateWithChangePitch(Component obj, AudioClip audioClip, bool Isloop, float changePitchRatio)
        {
            Component g = dummyInstantiate(obj, audioClip, Isloop);
            if (g == null) return null;
            AudioSource audioSource = g.GetComponent<AudioSource>();
            audioSource.pitch = UnityEngine.Random.Range(audioSource.pitch * (1 - changePitchRatio), audioSource.pitch * (1 + changePitchRatio));
            return g;
        }

        public Component dummyInstantiate(Component obj, AudioClip audioClip, bool Isloop)
        {
            if (obj == null)
            {
                Debug.LogError("SoundMgr.dummyInstantiate: prefab GameObject is null. Check the addressable key/path and that the prefab exists and is a GameObject.");
                return null;
            }

            var instantiated = Instantiate(obj, transform);
            if (instantiated == null)
            {
                Debug.LogError("SoundMgr.dummyInstantiate: Instantiate returned null for " + obj.name);
                return null;
            }

            SESource seSource = instantiated.GetComponent<SESource>();
            if (seSource == null)
            {
                Debug.LogError($"SoundMgr.dummyInstantiate: Instantiated prefab '{instantiated.name}' does not have SESource component. Add SESource or use a correct prefab.");
                return instantiated;
            }

            Log.DebugAssert(audioClip != null);
            seSource.clip = audioClip;
            seSource.loop = Isloop;
            seSource.Play();
            return seSource;
        }

        public GameObject dummyInstantiateBGM(GameObject obj, AudioClip audioClip, bool Isloop)
        {
            if (obj == null)
            {
                Debug.LogError("SoundMgr.dummyInstantiateBGM: prefab GameObject is null.");
                return null;
            }

            var inst = Instantiate(obj, transform);
            var seSource = inst.GetComponent<AudioSource>();
            if (seSource == null)
            {
                Debug.LogError($"SoundMgr.dummyInstantiateBGM: Instantiated prefab '{inst.name}' does not have AudioSource component.");
                return inst;
            }

            Log.DebugAssert(audioClip != null);
            seSource.clip = audioClip;
            seSource.loop = Isloop;
            seSource.Play();
            return seSource.gameObject;
        }

        public GameObject dummyInstantiateLoopSE(Component obj, AudioClip audioClip)
        {
            return dummyInstantiateWithList(obj, audioClip, true, loopSEList);
        }

        public GameObject dummyInstantiateRefleshSE(Component obj, AudioClip audioClip, bool IsLoop)
        {
            return dummyInstantiateWithList(obj, audioClip, IsLoop, refleshSEList);
        }

        public GameObject dummyInstantiateWithList(Component obj, AudioClip audioClip, bool IsLoop, List<Component> SoundObjectList)
        {
            var created = dummyInstantiate(obj, audioClip, IsLoop);
            if (created != null) SoundObjectList.Add(created);
            return SoundObjectList.Count > 0 ? SoundObjectList[SoundObjectList.Count - 1].gameObject : null;
        }

        public void RefleshLoopSEList()
        {
            for (int i = loopSEList.Count - 1; i >= 0; i--)
            {
                if (loopSEList[i] == null)
                {
                    loopSEList.RemoveAt(i);
                }
            }
        }

        public void StopLoopSE()
        {
            for (int i = loopSEList.Count - 1; i >= 0; i--)
            {
                if (loopSEList[i] != null)
                {
                    Destroy(loopSEList[i].gameObject);
                    loopSEList.RemoveAt(i);
                }
            }
            loopSEList = new List<Component>();
        }

        public void StopRefleshSE()
        {
            for (int i = 0; i < refleshSEList.Count; i++)
            {
                if (refleshSEList[i] != null)
                {
                    Destroy(refleshSEList[i]);
                }
            }
            refleshSEList = new List<Component>();
        }

        public bool IsStoppingBGM()
        {
            return mixerVolumeClass[(int)MixerExposedParameter.BgmRoot].IsChanging && mixerVolumeClass[(int)MixerExposedParameter.BgmRoot].IsMute;
        }

        public void StopBGM(float changeTime, MixerExposedParameter channelId)
        {
            Log.DebugLog("StopBGM");
            mixerVolumeClass[channelId].Mute(true, changeTime);
        }

        public void PauseBGM()
        {
            if (BGMSourceMain != null)
            {
                BGMSourceMain.Pause();
            }
        }

        public void RestartBGM()
        {
            if (BGMSourceMain != null)
            {
                if (!BGMSourceMain.isPlaying)
                {
                    BGMSourceMain.Play();
                }

            }
        }

        public void CompressOrDecompressVolume(MixerExposedParameter mep, bool IsCompress, float changeTime = longChangeTime)
        {
            mixerVolumeClass[mep].Compress(IsCompress, changeTime);
        }


        public void SetOffsetDecibel(MixerExposedParameter mep, float offsetDecibel, float changeTime = longChangeTime)
        {
            mixerVolumeClass[mep].SetOffsetDecibel(offsetDecibel, changeTime);
        }

        public void Mute(MixerExposedParameter mep, bool IsMute, float changeTime = longChangeTime)
        {
            mixerVolumeClass[mep].Mute(IsMute, changeTime);
        }

        public void SetVolume(MixerExposedParameter mep, int _volumeLevel)
        {
            mixerVolumeClass[mep].SetVolumeLevel(_volumeLevel);
        }

        public int VolumeLevel(MixerExposedParameter mep)
        {
            return mixerVolumeClass[mep].volumeLevel;
        }

        public void PauseLoopSE()
        {
            for (int i = 0; i < loopSEList.Count; i++)
            {
                if (loopSEList[i] != null)
                {
                    loopSEList[i].GetComponent<AudioSource>().loop = false;
                }
            }
        }

        public void RestartLoopSE()
        {
            for (int i = 0; i < loopSEList.Count; i++)
            {
                if (loopSEList[i] != null)
                {
                    loopSEList[i].GetComponent<AudioSource>().loop = true;
                }
            }
        }

        public void StopSoloLoopSE(AudioSource audioSource)
        {
            audioSource.loop = false;
        }

        public void GetOrder(string seLabel, MixerLabel MixerName, int priority)
        {
            Log.DebugLog("GetOrder: " + seLabel + " " + MixerName + " " + priority);
            properties.Add(new SeProperty(seLabel, MixerName, priority));
        }

        internal class SeProperty
        {
            internal string SeLabel;
            internal MixerLabel MixerLabel;
            internal int priority;

            internal SeProperty(string seLabel, MixerLabel mixerLabel, int priority)
            {
                SeLabel = seLabel;
                MixerLabel = mixerLabel;
                this.priority = priority;
            }
        }
    }
}