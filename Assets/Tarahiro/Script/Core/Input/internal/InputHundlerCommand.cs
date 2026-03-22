using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Tarahiro.TInput;
using static gaw241201.View.InputConst;
using UnityEngine.Windows;

namespace gaw241201.View
{
    public class InputHundlerCommand : IInputHundlerCommand
    {
        [Inject] PureSingletonInput _input;
        [Inject] PureSingletonKey _key;



        public bool IsInputtedCommand(Command command)
        {
            switch (command)
            {
                case Command.Decide:
                    return _key.IsKeyDown(KeyCode.Return) || _key.IsKeyDown(KeyCode.Space);

                case Command.Back:
                    return _key.IsKeyDown(KeyCode.Backspace);

                case Command.Escape:
                    return _key.IsKeyDown(KeyCode.Escape);

                case Command.Development:
                    return _key.IsKeyDown(KeyCode.Home);

                case Command.OnlyEnter:
                    return _key.IsKeyDown(KeyCode.Return);

                case Command.Click:
                    return _key.IsKeyDown(KeyCode.Mouse0);

                case Command.ClickRight:
                    return _key.IsKeyDown(KeyCode.Mouse1);

                case Command.Delete:
                    return _key.IsKeyDown(KeyCode.Delete);

                case Command.ShortCutCut:
                    return isPressedControl() && _key.IsKeyDown(KeyCode.X);
                case Command.ControlC:
                    return isPressedControl() && _key.IsKeyDown(KeyCode.C);

                case Command.Yes:
                    return _key.IsKeyDown(KeyCode.Y);
                case Command.No:
                    return _key.IsKeyDown(KeyCode.N);

                default:
                    return false;
            }
        }

        public void NotifyUse(Command command)
        {
            _input.AvailableInputted();
        }

        bool isPressedControl()
        {
                       return _key.IsKey(KeyCode.LeftControl) || _key.IsKey(KeyCode.RightControl);
        }


    }
}