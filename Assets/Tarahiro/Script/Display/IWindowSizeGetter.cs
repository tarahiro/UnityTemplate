using Cysharp.Threading.Tasks;
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
    public interface IWindowSizeGetter
    {
        Vector2Int Get();
        Vector2Int GetCurrentScreenSize();
        Vector2Int MaxMonitorResolution { get; }
    }
}