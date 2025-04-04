using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using TMPro;
using VContainer;
using MessagePipe;

namespace Tarahiro.Ui
{
    public class TranslationTextView : MonoBehaviour, ITranslationTextDisplayer
    {
        [SerializeField] TextMeshProUGUI tmp;
        [SerializeField] List<TMP_FontAsset> font;
        [SerializeField] List<float> fontSizeCoeffFromJp;

        ISubscriber<int> _subscriber;

        int _languageIndex = 0;
        float _initialFontSize;

        bool _isConstructed = false;

        [Inject]
        public void Construct(ISubscriber<int> subscriber)
        {
            if (!_isConstructed)
            {
                _subscriber = subscriber;

                _subscriber.Subscribe(x => SetLanguage(x));
                _initialFontSize = tmp.fontSize;

                _isConstructed = true;
            }
        }

        void Awake()
        {
        }

        public void SetLanguage(int languageIndex)
        {
            _languageIndex = languageIndex;
            tmp.font = font[_languageIndex];
            tmp.fontSize = _initialFontSize * fontSizeCoeffFromJp[_languageIndex];
        }

        public int GetLanguageIndex()
        {
            Log.DebugAssert(_languageIndex >= 0);
            return _languageIndex;
        }

    }
}
