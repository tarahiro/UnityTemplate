using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro.Ui
{
    public interface IEmbeddedTextView
    {
        string TextId { get; }
        void SetTranslatableText(ITranslatableText translatableText);
        void CoreCreate(ITranslationTextViewPureFactory factory);
    }
}