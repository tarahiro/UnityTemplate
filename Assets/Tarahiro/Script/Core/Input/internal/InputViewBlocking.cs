using Cysharp.Threading.Tasks;
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
    public class InputViewBlocking : IInputView
    {
        IInputView _inputView;
        ActiveLayerPublisher _publisher;
        ActiveLayerConst.InputLayer _layer;
        public IObservable<bool> BlockEnabled => _inputView.BlockEnabled;

        [Inject]
        public InputViewBlocking(IInputView inputView, ActiveLayerPublisher publisher, ActiveLayerConst.InputLayer layer)
        {
            _inputView = inputView;
            _publisher = publisher;
            _layer = layer;
        }

        public async UniTask Enter(CancellationToken ct)
        {
            _publisher.PublishActiveLayer(_layer);
            await _inputView.Enter(ct);
        }

        public void Exit()
        {
            _inputView.Exit();
            _publisher.ResetActiveLayer();
        }
    }
}