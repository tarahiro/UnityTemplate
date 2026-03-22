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

namespace Tarahiro.View
{
    public class SpriteSettableSmaImage : ISpriteSettableSma
    {
        Image _image;

        public SpriteSettableSmaImage(Image image)
        {
            _image = image;
        }

        public void SetSprite(Sprite sprite)
        {
            if (_image == null)
            {
                Debug.LogError("Image component is not set.");
                return;
            }
            _image.sprite = sprite;
        }
    }
}