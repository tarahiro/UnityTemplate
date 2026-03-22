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
    public class SmaControllerPsuedoInterface : MonoBehaviour
    {
        public virtual void SetTrigger(string stateName) { }
        public virtual void Pause() { }
        public virtual void Resume() { }
        public virtual bool IsTransitioning() { return false; }
        public virtual bool IsAnimationEnd() { return false; }
        public virtual void SetActive(bool isActive) { }
    }
}