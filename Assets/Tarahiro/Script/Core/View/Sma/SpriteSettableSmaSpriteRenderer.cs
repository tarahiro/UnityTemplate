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
    public class SpriteSettableSmaSpriteRenderer : ISpriteSettableSma
    {
        SpriteRenderer _spriteRenderer;

        public SpriteSettableSmaSpriteRenderer(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        public void SetSprite(Sprite sprite)
        {
            if (_spriteRenderer == null)
            {
                Debug.LogError("Image component is not set.");
                return;
            }
            _spriteRenderer.sprite = sprite;
        }
    }
}