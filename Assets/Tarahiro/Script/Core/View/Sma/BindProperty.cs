using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro.View
{
    [System.Serializable]
    public class BindProperty
    {
        [SerializeField] Transform _binded;
        [SerializeField] Transform _target;

        public Transform Binded => _binded;
        public Transform Target => _target;

    }
}