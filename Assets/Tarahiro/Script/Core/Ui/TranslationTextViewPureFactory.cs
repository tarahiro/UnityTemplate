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
    public class TranslationTextViewPureFactory : ITranslationTextViewPureFactory
    {
        [Inject] IObjectResolver _objectResolver;
        public ITranslationTextViewCore Create(TMP_Text tmp, TranslationTextView textView)
        {
            var translationTextViewPure = new TranslationTextViewPure(tmp, textView, _objectResolver.Resolve<ILanguageSubscribable>());
            return translationTextViewPure;
        }
    }
}