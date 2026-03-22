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
    public interface ISmaInstance
    {
        void Initialize(List<Sprite> sprites);
        void SetActive(bool isActive); 
    }
}