using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using MessagePipe;

namespace Tarahiro.Ui
{
    public class EmbeddedTextViewFinder
    {
        Subject<EmbeddedTranslationTextView> _finded = new Subject<EmbeddedTranslationTextView>();

        public IObservable<EmbeddedTranslationTextView> Finded => _finded;

        public void Initialize()
        {
            var array = GameObject.FindObjectsByType<EmbeddedTranslationTextView>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach(var view in array)
            {
                _finded.OnNext(view);
            }
        }
    }
}
