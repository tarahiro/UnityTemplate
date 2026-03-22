using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UnityEngine.UI;

namespace Tarahiro.View
{
    public class SmaImage : ISmaInstance
    {
        ISpriteSettableSma _spriteSettable;

        public SmaImage(ISmaCore smaCore, ISpriteSettableSma spriteSettable, GameObject gameObject)
        {
            _spriteSettable = spriteSettable;
            smaCore.Updated.Subscribe(_ => Sma()).AddTo(gameObject);
        }


        int _spriteIndex = 0;
        List<Sprite> _sprites = new List<Sprite>();
        public void Initialize(List<Sprite> sprite)
        {
            _spriteIndex = 0;
            _sprites = sprite;
            _spriteSettable.SetSprite(_sprites[_spriteIndex]);
        }

        bool _isActive = true;
        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        void Sma()
        {
            if (!_isActive)
            {
                return;
            }

            if (_sprites.Count == 0)
            {
                return;
            }
            _spriteIndex++;
            if (_spriteIndex >= _sprites.Count)
            {
                _spriteIndex = 0;
            }
            _spriteSettable.SetSprite(_sprites[_spriteIndex]);
        }

    }
}