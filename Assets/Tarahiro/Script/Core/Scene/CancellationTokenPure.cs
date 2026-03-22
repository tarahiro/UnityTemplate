using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public class CancellationTokenPure : ICancellationTokenPure
    {
        CancellationTokenSource _cancellationTokenSource = null;
        [Inject] ISubscriber<SceneEndConst.SceneEndOrder, ISceneUnit> _subscriber;

        public void Cancel()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }

        }
        public CancellationToken Token => _cancellationTokenSource.Token;
        public CancellationTokenSource Source => _cancellationTokenSource;

        public CancellationTokenPure(ISubscriber<SceneEndConst.SceneEndOrder, ISceneUnit> subscriber,IDisposablePure disposable)
        {
            _subscriber = subscriber;
            _subscriber.Subscribe(SceneEndConst.SceneEndOrder.Initialize, _ => Cancel()).AddTo(disposable);
        }

        public void SetNew()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public bool IsTokenExist => _cancellationTokenSource != null;
        public bool IsCancellationRequested => _cancellationTokenSource.IsCancellationRequested;

    }
}