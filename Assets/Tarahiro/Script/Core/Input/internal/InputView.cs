using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class InputView : IInputView
    {
        [Inject] IInputLayerSubscriber _subscriber;
        [Inject] IInputProcessable _inputProcessable;

        ActiveLayerConst.InputLayer _layer;
        public IObservable<bool> BlockEnabled => _subscriber.BlockEnabled;

        bool _isEnable = false;


        [Inject]
        public InputView(IInputLayerSubscriberFactory factory, IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer)
        {
            _inputProcessable = inputProcessable;
            _layer = layer;
            _subscriber = factory.Create(_layer);
        }

        public async UniTask Enter(CancellationToken ct)
        {
            _isEnable = true;
            while (_isEnable)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: ct);

                if (!_isBlocked())
                {
                    _inputProcessable.ProcessInput();
                }
            }
        }

        public void Exit()
        {
            _isEnable = false;
        }

        bool _isBlocked() => _subscriber.IsBlocked();

    }
}