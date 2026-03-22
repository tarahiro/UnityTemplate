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
    public class SmaCoreFactory : ISmaCoreFactory
    {
        [Inject] ISubscriber<SmaUnit> _subscriber;

        public ISmaCore Create(GameObject disposables)
        {
            return new SmaCore(_subscriber, disposables);
        }
    }
}