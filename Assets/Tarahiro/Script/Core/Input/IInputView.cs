using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Threading;

namespace gaw241201.View
{
    public interface IInputView
    {
        UniTask Enter(CancellationToken ct);
        void Exit();
        IObservable<bool> BlockEnabled { get; }
    }
}