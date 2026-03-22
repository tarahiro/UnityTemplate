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
    public class SmaAnimationControllerSpriteRenderer : SmaControllerPsuedoInterface
    {
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] SmaAnimationState _initialState;

        SmaAnimationControllerCore _core;

        [Inject] public void Construct(ISmaSpriteRendererFactory smaFactory)
        {
            if (_core == null)
            {
                var _smaSpriteRenderer = smaFactory.Create(_spriteRenderer, gameObject);
                _core = gameObject.AddComponent<SmaAnimationControllerCore>();
                _core.Initialize(_smaSpriteRenderer, _initialState);
            }
        }


        public override void SetTrigger(string stateName) => _core.SetTrigger(stateName);
        public override void Pause() => _core.Pause();
        public override void Resume() => _core.Resume();
        public override bool IsTransitioning() => _core.IsTransitioning();
        public override bool IsAnimationEnd() => _core.IsAnimationEnd();
        public override void SetActive(bool isActive) => _core.SetActive(isActive);
    }
}