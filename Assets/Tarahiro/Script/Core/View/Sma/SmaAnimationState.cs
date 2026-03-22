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
    public class SmaAnimationState : MonoBehaviour
    {
        [SerializeField] string Name;
        [SerializeField] SmaAnimationState _prevState;
        [SerializeField] SmaAnimationState _nextState;
        [SerializeField] List<FrameProperty> _frameProperties;
        [SerializeField] float _frame = 12f / 60f;

        public string StateName => Name;
        public SmaAnimationState PrevState => _prevState;
        public SmaAnimationState NextState => _nextState;
        public List<FrameProperty> FrameProperties => _frameProperties;
        public float Frame => _frame;
    }
}