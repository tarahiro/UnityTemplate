using Cysharp.Threading.Tasks;
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
    public interface ICancellationTokenPure
    {
        void Cancel();
        void SetNew();
        CancellationToken Token { get; }

        CancellationTokenSource Source { get; }

        bool IsTokenExist { get; }
        bool IsCancellationRequested { get; }
    }
}