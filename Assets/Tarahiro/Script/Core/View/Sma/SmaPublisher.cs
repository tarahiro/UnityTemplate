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
    public class SmaPublisher
    {
        [Inject] IPublisher<SmaUnit> _publisher;

        public void Publish()
        {
            _publisher.Publish(new SmaUnit());
        }
    }
}