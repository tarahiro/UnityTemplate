using gaw241201.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public static class WindowSizeGetterStatic
    {
        static IWindowSizeGetter _windowSizeGetter;

        public static Vector2Int Get()
        {
            return _windowSizeGetter.Get();
        }

        public static void Initailize(IWindowSizeGetter windowSizeGetter)
        {
            _windowSizeGetter = windowSizeGetter;
        }
    }
}
