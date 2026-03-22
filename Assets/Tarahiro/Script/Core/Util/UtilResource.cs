using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Tarahiro
{
    public static class UtilResource
    {
        public const string c_suffixPrefab = ".prefab";

        //将来的にはここでResouces.LoadやAddressableの出し分けをできるようにする

        public static T GetResource<T>(string path) where T : UnityEngine.Object
        {
            T obj = Resources.Load<T>(path);
            Log.DebugAssert(obj != null, path + "にオブジェクトが存在しません");
            return obj;
        }

        static readonly Dictionary<string, GameObject> _resourceCache = new Dictionary<string, GameObject>();

        public static async UniTask Preload(string path, string suffix, CancellationToken ct)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append(suffix);
            var s = sb.ToString();
            if (string.IsNullOrEmpty(s)) return;
            if (_resourceCache.ContainsKey(s)) return;
            var handle = Addressables.LoadAssetAsync<GameObject>(s);
            await handle.ToUniTask(cancellationToken: ct);
            Log.DebugAssert(handle.Status == AsyncOperationStatus.Succeeded, s + "にオブジェクトが存在しません");
            _resourceCache.Add(s, handle.Result);
        }

        public static T GetResourceAddressable<T>(string path, string suffix) where T : UnityEngine.Object
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append(suffix);
            var s = sb.ToString();

            if (string.IsNullOrEmpty(s)) return null;
            if (_resourceCache.TryGetValue(s, out var objCached))
            {
                return objCached.GetComponent<T>();
            }
            var handle = Addressables.LoadAssetAsync<GameObject>(s);
            handle.WaitForCompletion();
            Log.DebugAssert(handle.Status == AsyncOperationStatus.Succeeded, s + "にオブジェクトが存在しません");
            return handle.Result.GetComponent<T>();
        }

        // key -> (clip, handle)
        static readonly Dictionary<string, (AudioClip clip, AsyncOperationHandle<AudioClip> handle)> _addressableAudioCache
            = new Dictionary<string, (AudioClip, AsyncOperationHandle<AudioClip>)>();

        const string c_soundSuffixWav = ".wav";
        // Addressables で AudioClip をロードしてオーディオデータの読み込み完了まで待つ
        public static async UniTask PreloadAudio(string s, CancellationToken ct = default)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            sb.Append(c_soundSuffixWav);
            string path = sb.ToString();

            if (string.IsNullOrEmpty(path)) return;
            if (_addressableAudioCache.ContainsKey(path)) return;

            var handle = Addressables.LoadAssetAsync<AudioClip>(path);
            await handle.ToUniTask(cancellationToken: ct);

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var clip = handle.Result;
                // Audio データが未読み込みなら読み込みを開始して完了まで待つ
                if (clip.loadState != AudioDataLoadState.Loaded)
                {
                    try
                    {
                        clip.LoadAudioData();
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"UtilResource.Preload: LoadAudioData exception for '{path}': {e.Message}");
                    }

                    // 非同期的にロード完了を待つ
                    while (clip.loadState == AudioDataLoadState.Loading)
                    {
                        ct.ThrowIfCancellationRequested();
                        await UniTask.Yield(PlayerLoopTiming.Update, ct);
                    }
                }

                _addressableAudioCache[path] = (clip, handle);
            }
            else
            {
                Debug.LogWarning($"UtilResource: Failed to preload '{path}' (Status={handle.Status})");
                Addressables.Release(handle);
            }
        }


        public static AudioClip GetAudioAddressable(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(s);
            sb.Append(c_soundSuffixWav);
            string path = sb.ToString();
            if (string.IsNullOrEmpty(path)) return null;

            if (_addressableAudioCache.TryGetValue(path, out var tuple))
            {

                var clipCached = tuple.clip;
                if (clipCached.loadState != AudioDataLoadState.Loaded)
                {
                    try
                    {
                        clipCached.LoadAudioData();
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"UtilResource.GetResourceAddressable: LoadAudioData exception for cached '{path}': {e.Message}");
                    }
                }
                return clipCached;
            }

            // キャッシュしていない場合は同期ロード（注意：ブロッキング）
            var clip = Addressables.LoadAssetAsync<AudioClip>(path).WaitForCompletion();
            Log.DebugAssert(clip != null, path + "にオブジェクトが存在しません");

            if (clip.loadState != AudioDataLoadState.Loaded)
            {
                try
                {
                    clip.LoadAudioData();
                    var start = Time.realtimeSinceStartup;
                    while (clip.loadState == AudioDataLoadState.Loading && Time.realtimeSinceStartup - start < 1.0f)
                    {
                        System.Threading.Thread.Sleep(10);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"UtilResource.GetResourceAddressable: LoadAudioData exception for '{path}': {e.Message}");
                }
            }

            return clip;
        }

        public static bool IsExist(string path)
        {
            return Resources.Load<GameObject>(path) != null;
        }

        public static string ResourcePath()
        {
            return "Assets/Resources/";
        }


    }
}