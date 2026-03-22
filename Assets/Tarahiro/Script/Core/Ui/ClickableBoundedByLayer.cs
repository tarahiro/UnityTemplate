using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public class ClickableBoundedByLayer : Clickable
    {
        [SerializeField] ActiveLayerConst.InputLayer _layer = ActiveLayerConst.InputLayer.None;
        IInputLayerSubscriber subscriber;

        [Inject] public void Construct(IInputLayerSubscriberFactory _factory)
        {
            subscriber = _factory.Create(_layer);
            subscriber.BlockEnabled.Subscribe(OnBlockEnable).AddTo(this);
        }
        protected override void OnClick()
        {
            if (!subscriber.IsBlocked())
            {
                base.OnClick();
            }
        }

        void OnBlockEnable(bool isEnabled)
        {
            _button.GetComponent<UnityEngine.UI.Graphic>().raycastTarget = !isEnabled;
        }
    }
}