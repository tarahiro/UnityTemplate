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
    public class TranslationTextView : MonoBehaviour, ITranslationTextDisplayer, ITranslationTextView
    {
        [SerializeField] TMP_Text tmp;
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
                SetLanguage(_subscriber.GetLanguageIndex());
            }
        }

        public void SetLanguage(int languageIndex)
        {
            try
            {
                tmp.font = font[languageIndex];
                tmp.fontSize = _initialFontSize * fontSizeCoeffFromJp[languageIndex];
            }catch(Exception e)
            {
               Log.DebugAssert($"TranslationTextView:SetLanguage gameObject={this.name}で例外が発生しました。\n{e}");
            }
        }

        public int GetLanguageIndex() => _subscriber.GetLanguageIndex();

    }
}
