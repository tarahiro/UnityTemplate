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
    public class TranslationImageView : MonoBehaviour
    {
        [SerializeField] List<Sprite> _sprites;
        [SerializeField] UnityEngine.UI.Image _image;
        ILanguageSubscribable _subscriber;

        [Inject]
        public void Construct(ILanguageSubscribable subscriber)
        {
            _subscriber = subscriber;
            _subscriber.LanguageChanged.Subscribe(x => SetLanguage(x)).AddTo(gameObject);
        }

        void SetLanguage(int languageIndex)
        {
            _image.sprite = _sprites[languageIndex];
        }

        private void Awake()
        {
            SetLanguage(_subscriber.GetLanguageIndex());
        }
    }
}