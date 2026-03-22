using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class KeyCodeTextView
    {
        Dictionary<KeyCode, ITranslatableText> _keyCodeMessages;

        public void Initialize(Dictionary<KeyCode, ITranslatableText> keyCodeMessages)
        {
            _keyCodeMessages = keyCodeMessages;
        }   

        public bool TryGetMessage(KeyCode keyCode, out ITranslatableText message)
        {
            return _keyCodeMessages.TryGetValue(keyCode, out message);
        }
    }
}