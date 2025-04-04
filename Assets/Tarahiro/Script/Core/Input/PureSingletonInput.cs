using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;
using VContainer.Unity;

namespace Tarahiro
{
    public class PureSingletonInput
    {

        public bool IsAnyAvailableInputted { get; private set; } = false;

        public void AvailableInputted()
        {
            Log.DebugLog("IsAnyAvailableInputtedをセット");
            IsAnyAvailableInputted = true;
        }

        public void ResetInputtedStatus()
        {
            IsAnyAvailableInputted = false;
        }
    }
}
