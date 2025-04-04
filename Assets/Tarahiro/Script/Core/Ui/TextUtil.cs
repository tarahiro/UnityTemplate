using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;
using static SoundManager;
using Tarahiro.TInput;

namespace Tarahiro.Ui
{
    public static class TextUtil
    {
        public const float c_defaultTextIntervalTime = .1f;
        public static async UniTask DisplayTextByCharacter(string text, TextMeshProUGUI textMeshProUGUI, string SeLabel, KeyCode[] decide, CancellationToken ct, PureSingletonInput input, PureSingletonKey key, bool isSeRun = true, float textIntervalTime = c_defaultTextIntervalTime)
        {
            ct.Register(() => ExitDisplayText(text, textMeshProUGUI, isSeRun));
            await TextCount(text,textMeshProUGUI, SeLabel, decide, ct,input,key, isSeRun, textIntervalTime);
            ExitDisplayText(text, textMeshProUGUI, isSeRun);
        }

        static async UniTask TextCount(string text, TextMeshProUGUI textMeshProUGUI, string seLabel, KeyCode[] decide, CancellationToken ct, PureSingletonInput input, PureSingletonKey key, bool isSeRun, float textIntervalTime)
        {
            if (isSeRun) {
                SoundManager.PlaySEWithLoop(seLabel);
            }
            float m_Tick = 0;
            int textCount = 0;
            textMeshProUGUI.text = text;
            textMeshProUGUI.maxVisibleCharacters = 0;
            bool _isEnd = false;

            while (!_isEnd && ! ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);

                if (decide.Any(x => key.IsKeyDown(x)))
                {
                    input.AvailableInputted();
                    _isEnd = true;
                }

                m_Tick += Time.deltaTime;
                if (m_Tick > textIntervalTime)
                {
                    m_Tick = 0;
                    textCount++;
                    textMeshProUGUI.maxVisibleCharacters = textCount;

                    if (textCount >= textMeshProUGUI.GetParsedText().Length)
                    {
                        _isEnd = true;

                    }
                }
            }



        }

        static void ExitDisplayText(string text, TextMeshProUGUI textMeshProUGUI,bool isSeRun)
        {
            if (isSeRun)
            {
                SoundManager.StopLoopSE();
            }
            textMeshProUGUI.maxVisibleCharacters = textMeshProUGUI.GetParsedText().Length;

        }
    }
}