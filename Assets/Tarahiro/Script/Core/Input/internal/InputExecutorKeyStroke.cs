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
    public class InputExecutorKeyStroke : IInputExecutor
    {
        [Inject] IInputHundlerKeyStroke _hundler;

        Subject<char> _inputted = new Subject<char>();

        public IObservable<char> Inputted => _inputted;

        public void TryExecute()
        {
            for(int i = 0; i < _hundler.StrokedKey().Length; i++)
            {
                _inputted.OnNext(_hundler.StrokedKey()[i]);
            }
        }
    }
}