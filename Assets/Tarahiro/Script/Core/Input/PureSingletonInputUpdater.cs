using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public class PureSingletonInputUpdater : ITickable
    {
        [Inject] PureSingletonInput _input;

        public void Tick()
        {
            if (_input.IsAnyAvailableInputted)
            {
                _input.ResetInputtedStatus();
            }
        }

    }
}
