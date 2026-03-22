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
    public class InputExecutorDiscreteDirectionHorizontal : IInputExecutor
    {
        IInputHundlerDiscreteDirection _hundler;

        Subject<int> _inputted = new Subject<int>();

        public IObservable<int> Inputted => _inputted;


        [Inject]
        public InputExecutorDiscreteDirectionHorizontal(IInputHundlerDiscreteDirection hundler)
        {
            _hundler = hundler;
        }

        public void TryExecute()
        {
            Vector2Int vec = _hundler.InputtedDiscreteDirection();

            if (vec.x != 0)
            {
                _inputted.OnNext(vec.x);

                _hundler.NotifyUse(Vector2Int.right * vec.x);
            }
        }

      
    }
}