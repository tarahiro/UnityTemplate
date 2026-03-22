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
using gaw241201.View;

namespace Tarahiro.Ui
{
    public class EmbeddedTranslationTextView : MonoBehaviour, ITranslationTextViewCore, IEmbeddedTextView
    {
        public string TextId => Id;
        public string Id;
        protected ITranslationTextViewCore _translationTextViewCore;


        bool _isConstructed = false;


        [Inject]
        public void Construct(IPublisher<IEmbeddedTextView> _publisher)
        {
            _publisher.Publish(this);
        }


        public void CoreCreate(ITranslationTextViewPureFactory factory)
        {
            if (!_isConstructed)
            {
                _translationTextViewCore = factory.Create(GetComponent<TMP_Text>(), GetComponent<TranslationTextView>());
            }
        }

        public virtual void SetTranslatableText(ITranslatableText translatableText) => _translationTextViewCore.SetTranslatableText(translatableText);

    }
}
