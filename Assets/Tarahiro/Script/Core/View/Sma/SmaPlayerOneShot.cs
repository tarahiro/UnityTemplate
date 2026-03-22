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
    public class SmaPlayerOneShot : MonoBehaviour
    {
        Subject<Unit> _onEnd = new Subject<Unit>();
        public IObservable<Unit> OnEnd => _onEnd;


        SmaControllerPsuedoInterface _animationController;
        private void Awake()
        {
            _animationController = GetComponent<SmaControllerPsuedoInterface>();
        }

        private void Update()
        {
            if(_animationController.IsAnimationEnd())
            {
                End();
                return;
            }
        }

        void End()
        {
            _onEnd.OnNext(Unit.Default);
            Destroy(gameObject);
        }
    }
}