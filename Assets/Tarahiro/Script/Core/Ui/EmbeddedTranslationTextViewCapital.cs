using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.Ui;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class EmbeddedTranslationTextViewCapital : EmbeddedTranslationTextView
    {
        public override void SetTranslatableText(ITranslatableText translatableText)
        {
            var list = new List<string>();

            foreach (LanguageConst.AvailableLanguage lang in Enum.GetValues(typeof(LanguageConst.AvailableLanguage)))
            {
                list.Add(translatableText.GetTranslatedText((int)lang).ToUpper());
            }

            _translationTextViewCore.SetTranslatableText(new TranslatableText(list.ToArray()));
        }
    }
}