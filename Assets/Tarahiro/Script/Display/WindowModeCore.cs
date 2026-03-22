using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;

namespace Tarahiro
{
    public class WindowModeCore : IWindowModeGetter, IWindowModeSetter
    {
        DisplayConst.WindowMode _windowMode;
        Subject<Unit> _onSet = new Subject<Unit>();
        public IObservable<Unit> OnSet => _onSet;
        public void Set(DisplayConst.WindowMode windowMode)
        {
           _windowMode = windowMode;

            // IObservableでやってもいいかも
            _onSet.OnNext(Unit.Default);
        }

        public DisplayConst.WindowMode Get()
        {
            return _windowMode;
        }

    }
}
