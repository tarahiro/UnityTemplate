using Cysharp.Threading.Tasks;
using gaw241201.View;
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

namespace Tarahiro.Ui
{
    public class TranslationTextViewPure : ITranslationTextViewCore
    {
        TMP_Text _tmp;
        TranslationTextView _textView;

        ITranslatableText _translatableText = null;
        ILanguageSubscribable _subscriber;


        public TranslationTextViewPure(TMP_Text tmp, TranslationTextView textView, ILanguageSubscribable subscriber)
        {
            _subscriber = subscriber;
            _tmp = tmp;
            _textView = textView;
            _textView.Construct(subscriber);

            subscriber.LanguageChanged.Subscribe(x => SetLanguage(x)).AddTo(_tmp);
        }

        public void SetTranslatableText(ITranslatableText translatableText)
        {
            _translatableText = translatableText;
            SetLanguage(_subscriber.GetLanguageIndex());
        }

        void SetLanguage(int languageIndex)
        {
            if (_translatableText != null)
            {
                _textView.SetLanguage(languageIndex);
                _tmp.text = _translatableText.GetTranslatedText(languageIndex);
            }
        }
    }
}