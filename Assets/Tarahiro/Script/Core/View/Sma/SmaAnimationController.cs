using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;
using System.Linq;
using UnityEngine.Experimental.Rendering;

namespace Tarahiro.View
{
    public class SmaAnimationController : SmaControllerPsuedoInterface
    {
        ISmaImageFactory _smaImageFactory;
        [SerializeField] Image _image;

        [SerializeField] SmaAnimationState _initialState;

        SmaAnimationControllerCore _core;

        SmaAnimationControllerCore GetCore()
        {
            if (_core == null)
            {
                Create();
            }
            return _core;
        }

        [Inject] public void Construct(ISmaImageFactory smaImageFactory)
        {
            _smaImageFactory = smaImageFactory;
        }

        void Start()
        {
            if (_core == null)
            {
                Create();
            }
        }

        void Create()
        {
            Log.DebugAssert(_smaImageFactory != null, "SmaAnimationController: SmaImageFactory is not injected.");
            Log.DebugAssert(_image != null, "SmaAnimationController: Image component is not assigned.");
            var _smaImage = _smaImageFactory.Create(_image, gameObject);
            _core = gameObject.AddComponent<SmaAnimationControllerCore>();
            _core.Initialize(_smaImage, _initialState);
        }
        public override void SetTrigger(string stateName) => GetCore().SetTrigger(stateName);
        public override void Pause() => GetCore().Pause();
        public override void Resume() => GetCore().Resume();
        public override bool IsTransitioning() => GetCore().IsTransitioning();
        public override void SetActive(bool isActive) => GetCore().SetActive(isActive);
    }
}