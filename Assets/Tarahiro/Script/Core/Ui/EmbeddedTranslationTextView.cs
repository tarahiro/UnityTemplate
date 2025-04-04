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
    public class EmbeddedTranslationTextView : MonoBehaviour
    {
        TextMeshProUGUI tmp;
        TranslationTextView textView;

        ISubscriber<int> _subscriber;
        public string Id;
        ITranslatableText _translatableText = null;

        bool _isConstructed = false;
        [Inject]
        public void Construct(ISubscriber<int> subscriber)
        {
            if (!_isConstructed)
            {
                _subscriber = subscriber;

                tmp = GetComponent<TextMeshProUGUI>();
                textView = GetComponent<TranslationTextView>();
                textView.Construct(_subscriber);

                _subscriber.Subscribe(x => SetLanguage(x));
            }
        }

        public void SetTranslatableText(ITranslatableText translatableText)
        {
            _translatableText = translatableText;
        }

        void SetLanguage(int languageIndex)
        {
            if(_translatableText != null)
            {
                tmp.text = _translatableText.GetTranslatedText(languageIndex);
            }
        }
    }
}
