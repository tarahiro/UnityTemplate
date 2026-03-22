using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;

namespace Tarahiro
{
    public class ResolutionCore : IResolutionGetter, IResolutionSetter
    {

        Vector2Int _resolution;

        Subject<Unit> _onSet = new Subject<Unit>();
        public IObservable<Unit> OnSet => _onSet;

        public void Set(Vector2Int resolution)
        {
            _resolution = resolution;
            _onSet.OnNext(Unit.Default);
        }

        public Vector2Int Get() => _resolution;
    }
}
