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
    public interface IInputHundlerDiscreteDirection
    {
        Vector2Int InputtedDiscreteDirection();

        void NotifyUse(Vector2Int vec);
    }
}