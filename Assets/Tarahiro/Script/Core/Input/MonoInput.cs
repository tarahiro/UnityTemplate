using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UnityEngine;

namespace Tarahiro.TInput
{
    public class MonoInput : MonoBehaviour
    {
        private void Update()
        {
            if (IsAnyAvailableInputted)
            {
                Log.DebugLog("IsAnyAvailableInputtedをリセット");
                IsAnyAvailableInputted = false;
            }
        }

        public bool IsAnyAvailableInputted { get; private set; } = false;

        public void AvailableInputted()
        {
                Log.DebugLog("IsAnyAvailableInputtedをセット");
            IsAnyAvailableInputted = true;
        }
    }
}
