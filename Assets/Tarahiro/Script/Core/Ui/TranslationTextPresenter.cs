using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using MessagePipe;
using Tarahiro.Ui;

namespace gaw241201.View
{
    public class TranslationTextPresenter : IPostInitializable
    {
        [Inject] EmbeddedTranslationTextViewFinder _viewManager;
        CompositeDisposable _disposable = new CompositeDisposable();

        [Inject] ILanguageSubscribable _subscriber;



        public void PostInitialize()
        {
            _viewManager.Finded.Subscribe(OnFind).AddTo(_disposable);
            _viewManager.Initialize();
        }

        public void OnFind(ITranslationTextView findedView)
        {
            findedView.Construct(_subscriber);
        }
    }
}