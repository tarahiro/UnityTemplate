using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;
using static ConstSound;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.Windows;

namespace Tarahiro.Ui
{
    public class DisplayableTextByCharacter : IDisplayableTextByCharacter
    {

        public async UniTask Enter(CancellationToken ct)
        {
            ct.Register(() => ExitDisplayText());

            if (_isSeRun)
            {
                SoundManagerSe.PlaySEWithLoop(_seLabel);
            }
            float m_Tick = 0;
            int textCount = 0;
            _textMeshProUGUI.text = _text;
            _textMeshProUGUI.maxVisibleCharacters = 0;
            _isEnd = false;

            while (!_isEnd && !ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: ct);

                m_Tick += Time.deltaTime;
                if (m_Tick > _textIntervalTime)
                {
                    m_Tick = 0;
                    textCount++;
                    _textMeshProUGUI.maxVisibleCharacters = textCount;

                    if (textCount >= _textMeshProUGUI.GetParsedText().Length)
                    {
                        _isEnd = true;
                    }
                }
            }

            ExitDisplayText();
        }
        void ExitDisplayText()
        {
            if (_isSeRun)
            {
                SoundManagerSe.StopLoopSE();
            }
            _textMeshProUGUI.maxVisibleCharacters = _textMeshProUGUI.GetParsedText().Length;
            _isEnd = true;

        }

        public void Skip()
        {
            _isEnd = true;
        }


        string _text;
        TextMeshProUGUI _textMeshProUGUI;
        string _seLabel;
        bool _isSeRun;
        float _textIntervalTime;
        bool _isEnd;

        public DisplayableTextByCharacter(string text, TextMeshProUGUI textMeshProUGUI, string SeLabel, bool isSeRun, float textIntervalTime)
        {
            _text = text;
            _textMeshProUGUI = textMeshProUGUI;
            _seLabel = SeLabel;
            _isSeRun = isSeRun;
            _textIntervalTime = textIntervalTime;
        }
    }
}