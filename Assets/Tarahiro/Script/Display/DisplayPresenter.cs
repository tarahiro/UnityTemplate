using gaw241201.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public class DisplayPresenter : IPostInitializable, IDisposable
    {
        [Inject] IWindowModeSetter _windowModeSetter;
        [Inject] IResolutionSetter _resolutionSetter;
        [Inject] IWindowSizeGetter _windowSizeGetter;
        [Inject] IDisplayUpdater _displayUpdater;
        [Inject] IOnDisplayUpdate _onDisplayUpdate;

        public void PostInitialize()
        {
            _windowModeSetter.OnSet.Subscribe(_ => _displayUpdater.UpdateDisplay());
            _resolutionSetter.OnSet.Subscribe(_ => _displayUpdater.UpdateDisplay());
            _displayUpdater.OnDisplayUpdate.Subscribe(_ => _onDisplayUpdate.OnDisplayUpdate());

            WindowSizeGetterStatic.Initailize(_windowSizeGetter);
        }

        public void Dispose()
        {
        }

    }
}
