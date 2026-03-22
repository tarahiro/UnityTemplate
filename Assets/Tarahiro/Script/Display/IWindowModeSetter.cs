using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public interface IWindowModeSetter : IDisplayArgsSettable
    {
        /// <summary>
        /// Set the window mode.
        /// </summary>
        /// <param name="windowMode">The window mode to set.</param>
        void Set(DisplayConst.WindowMode windowMode);
        /// <summary>
        /// Observable that emits when the window mode changes.
        /// </summary>
    }
}
