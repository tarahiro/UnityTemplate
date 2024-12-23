using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public class PlatformInfoProvider
    {
        public Const.Platform GetPlatform()
        {
           // Fake 今はWindowsしか返さない
            return Const.Platform.Windows;
        }
    }
}
