using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.Ui;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TranslationTextView3D : MonoBehaviour, ITranslationTextDisplayer, ITranslationTextView
    {
        [SerializeField] TextMeshPro tmp;
        [SerializeField] List<TMP_FontAsset> font;
        [SerializeField] List<float> fontSizeCoeffFromJp;


        float _initialFontSize;
        ILanguageSubscribable _subscriber;
        bool _isConstructed = false;

        [Inject]
        public void Construct(ILanguageSubscribable subscriber)
        {
            if (!_isConstructed)
            {
                _subscriber = subscriber;
                subscriber.LanguageChanged.Subscribe(x => SetLanguage(x)).AddTo(gameObject);
                _initialFontSize = tmp.fontSize;

                _isConstructed = true;
            }
        }

        public void SetLanguage(int languageIndex)
        {
            tmp.font = font[languageIndex];
            tmp.fontSize = _initialFontSize * fontSizeCoeffFromJp[languageIndex];
        }

        public int GetLanguageIndex() => _subscriber.GetLanguageIndex();

    }
}