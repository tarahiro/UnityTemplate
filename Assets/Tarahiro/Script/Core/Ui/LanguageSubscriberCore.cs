using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class LanguageSubscriberCore : ILanguageSubscribable
    {
        Subject<int> _languageChanged = new Subject<int>();
        public IObservable<int> LanguageChanged => _languageChanged;

        int _language = 0;

        public int GetLanguageIndex()
        {
            return _language;
        }

        //IDisposable‚ðPure‚É‚·‚é‚©‚à
        [Inject] public LanguageSubscriberCore(ISubscriber<int> subscriber,IPublisher<ILanguageSubscribable> publisher)
        {
            subscriber.Subscribe(OnLanguageChanged);
            publisher.Publish(this);
        }

        public void OnLanguageChanged(int languageIndex)
        {
            _language = languageIndex;
            _languageChanged.OnNext(_language);
        }
    }
}