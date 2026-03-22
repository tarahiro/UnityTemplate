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
    public interface ITranslationTextViewPureFactory
    {
        ITranslationTextViewCore Create(TMP_Text tmp, TranslationTextView textView);
    }
}