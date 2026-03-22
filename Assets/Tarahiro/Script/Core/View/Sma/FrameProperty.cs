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
    [System.Serializable]
    public class FrameProperty
    {

        [SerializeField] List<Sprite> _sprites;
        [SerializeField] List<BindProperty> _bindProperties;

        public List<Sprite> Sprites => _sprites;
        public List<BindProperty> BindProperties => _bindProperties;
    }
}