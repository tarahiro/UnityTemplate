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
    public class InputLayerSubscriberFactory : IInputLayerSubscriberFactory
    {
        [Inject] ISubscriber<ActiveLayerConst.InputLayer> subscriber;
        [Inject] IDisposablePure _disposable;

        public IInputLayerSubscriber Create(ActiveLayerConst.InputLayer inputLayer)
        {
            return new InputLayerSubscriber(subscriber, inputLayer, _disposable);
        }
    }
}