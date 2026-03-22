using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    public interface IDisplayUpdater
    {
        void UpdateDisplay();
        IObservable<Unit> OnDisplayUpdate { get; }
    }
}
