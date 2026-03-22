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
    public interface IInputViewFactory
    {
        IInputView CreateWithBlock(IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer);
        IInputView Create(IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer);
    }
}