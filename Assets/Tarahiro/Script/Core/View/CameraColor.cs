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
    public class CameraColor
    {

        public void SetColor(Color c)
        {
            Camera.main.backgroundColor = c;
        }

        public Color GetColor() => Camera.main.backgroundColor;
    }
}