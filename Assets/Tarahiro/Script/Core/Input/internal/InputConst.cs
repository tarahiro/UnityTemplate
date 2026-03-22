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
    public static class InputConst
    {
        public enum Command
        {
            None,
            Decide,
            Back,
            Escape,
            Development,
            OnlyEnter, //Enterキーのみを使用する
            Click,
            ClickRight,
            Delete,
            ShortCutCut,
            ControlC,
            Yes,
            No,
        }
    }
}