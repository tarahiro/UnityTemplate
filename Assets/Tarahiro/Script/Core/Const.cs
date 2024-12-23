using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public static class Const
    {
        public const string c_true = "True";
        public const string c_false = "False";

        public enum OrderOnMonoCanvas
        {
            Fader = 10,
#if ENABLE_VIRTUAL_CURSOR
            Cursor = 100
#endif
        }

        public enum Platform
        {
            Windows,
            Mac,
        }

        public static Vector2 Resolution = new Vector2(1920f, 1080f);
    }
}
