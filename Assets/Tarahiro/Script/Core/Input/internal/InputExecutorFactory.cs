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
    public class InputExecutorFactory : IInputExecutorFactory
    {
        [Inject] IInputHundlerCommand _inputHundlerCommand;
        [Inject] IInputHundlerDiscreteDirection _hundler;

        public InputExecutorCommand CreateInputExecutorCommand()
        {
            return new InputExecutorCommand(_inputHundlerCommand);
        }

        public InputExecutorDiscreteDirectionHorizontal CreateInputExecutorDiscreteDirectionHorizontal()
        {
            return new InputExecutorDiscreteDirectionHorizontal(_hundler);
        }
    }
}