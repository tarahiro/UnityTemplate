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
    public class SmaImageMonoBehaviour : MonoBehaviour
    {
        [Inject] ISmaImageFactory _smaImageFactory;
        [SerializeField] List<Sprite> _spriteList;

        private void Start()
        {
            var smaImage = _smaImageFactory.Create(GetComponent<Image>(), gameObject);
            smaImage.Initialize(_spriteList);
        }
    }
}