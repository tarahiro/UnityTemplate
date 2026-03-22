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
    public class InputHundlerKeyStroke : IInputHundlerKeyStroke
    {
        [Inject] PureSingletonKey _key;

        public string StrokedKey()
        {
            return _key.GetStrokedKey();
        }
        
        //NotifyUseはいらないかも？
        //かなり広範な入力を有効として取るので、他のキー入力を阻害しそう
    }
}