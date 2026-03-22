using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.View.InputConst;

namespace gaw241201.View
{
    public interface IInputHundlerCommand
    {
        bool IsInputtedCommand(Command command);
        void NotifyUse(Command command);
    }
}