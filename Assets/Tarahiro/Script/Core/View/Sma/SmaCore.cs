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

namespace Tarahiro.View
{
    public class SmaCore : ISmaCore
    {
        Subject<Unit> _updated = new Subject<Unit>();
        public IObservable<Unit> Updated => _updated;

         public SmaCore(ISubscriber<SmaUnit> _subscriber, GameObject _disposable)
        {
            _subscriber.Subscribe(_ => _updated.OnNext(Unit.Default)).AddTo(_disposable);
        }
    }
}