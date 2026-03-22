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

namespace Tarahiro
{
    public class Clickable : MonoBehaviour, IClickable
    {
        Subject<Unit> _clicked = new Subject<Unit>();
        public IObservable<Unit> Clicked => _clicked;
        [SerializeField] protected Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(() => OnClick());
        }

        protected virtual void OnClick()
        {
            Log.DebugLog("Clickable: " + gameObject.name + " clicked"); 
            _clicked.OnNext(Unit.Default);
        }

    }
}