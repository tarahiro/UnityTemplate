using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using Tarahiro.Ui;

namespace gaw241201.View
{
    public class TranslationTextViewFinder
    {
        Subject<TranslationTextView> _finded = new Subject<TranslationTextView>();

        public IObservable<TranslationTextView> Finded => _finded;

        public void Initialize()
        {
            var array = GameObject.FindObjectsByType<TranslationTextView>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var view in array)
            {
                _finded.OnNext(view);
            }
        }
    }
}