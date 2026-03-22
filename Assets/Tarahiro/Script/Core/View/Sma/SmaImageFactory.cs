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
    public class SmaImageFactory : ISmaImageFactory, ISmaSpriteRendererFactory
    {
        [Inject] ISmaCoreFactory _coreFactory;

        public ISmaInstance Create(Image image, GameObject gameObject)
        {
            return new SmaImage(_coreFactory.Create(gameObject),new SpriteSettableSmaImage(image), gameObject);
        }

        public ISmaInstance Create(SpriteRenderer sprite, GameObject gameObject)
        {
            return new SmaImage(_coreFactory.Create(gameObject), new SpriteSettableSmaSpriteRenderer(sprite), gameObject);
        }
    }
}