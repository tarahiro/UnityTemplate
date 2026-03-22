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

namespace gaw241201.View
{
    public class InputLayerSubscriber : IInputLayerSubscriber
    {
        ActiveLayerConst.InputLayer _layer;
        ActiveLayerConst.InputLayer _activeLayer = ActiveLayerConst.InputLayer.None;

        Subject<bool> _blockEnabled = new Subject<bool>();
        public IObservable<bool> BlockEnabled => _blockEnabled;

        public InputLayerSubscriber(ISubscriber<ActiveLayerConst.InputLayer> subscriber, ActiveLayerConst.InputLayer layer, IDisposablePure _disposable)
        {
            _layer = layer;
            subscriber.Subscribe(OnActiveLayerChanged).AddTo(_disposable);
        }

        public bool IsBlocked()
        {
            return _layer < _activeLayer;
        }

        void OnActiveLayerChanged(ActiveLayerConst.InputLayer layer)
        {

            if (_activeLayer <= _layer && layer > _layer)
            {
                _blockEnabled.OnNext(true);
            }

            if (_activeLayer > _layer && layer <= _layer)
            {
                _blockEnabled.OnNext(false);
            }

            _activeLayer = layer;

        }
    }
}