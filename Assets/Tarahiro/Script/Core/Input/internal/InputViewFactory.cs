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
    public class InputViewFactory : IInputViewFactory
    {
        [Inject] IInputLayerSubscriberFactory _factory;
        [Inject] ActiveLayerPublisher _publisher;

        public IInputView CreateWithBlock(IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer)
        {
            var inputView = Create(inputProcessable, layer);
            return new InputViewBlocking(inputView, _publisher, layer);
        }

        public IInputView Create(IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer)
        {
            return new InputView(_factory, inputProcessable, layer);
        }

    }
}