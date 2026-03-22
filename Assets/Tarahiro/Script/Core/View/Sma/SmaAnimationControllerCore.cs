using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro.View
{
    public class SmaAnimationControllerCore : SmaControllerPsuedoInterface
    {
        ISmaInstance _smaImage;
        SmaAnimationState _currentState;
        List<SmaAnimationState> _animationStates;
        SmaAnimationState _initialState;

        public void Initialize(ISmaInstance smaImage, SmaAnimationState initialState)
        {
            _animationStates = GetComponentsInChildren<SmaAnimationState>().ToList();
            _smaImage = smaImage;
            _initialState = initialState;
            StartAnimation(_initialState);
        }

        private void Start()
        {
            //StartAnimation(_initialState);
        }

        int _animationIndex = 0;
        float _time = 0f;

        void StartAnimation(SmaAnimationState state)
        {
            if (!_isActive)
            {
                return;
            }


            _currentState = state;
            _animationIndex = 0;
            _time = 0f;

            if (_currentState.FrameProperties.Count == 0)
            {
                return;
            }
            UpdateFrame();
        }

        bool _isPaused = false;
        bool _isActive = true;

        bool _isPlaying()
        {
            if (!_isActive) return false;
            if(_isPaused) return false;
            return true;
        }

        private void Update()
        {
            if (_isPlaying())
            {
                _time += Time.deltaTime;

                if (_time > _currentState.Frame)
                {
                    if (_currentState.FrameProperties.Count > 1)
                    {
                        NextFrame();
                    }
                    _time = 0f;
                }
            }
        }

        void NextFrame()
        {
            _animationIndex++;
            if (_animationIndex >= _currentState.FrameProperties.Count)
            {
                if (_currentState.NextState != null)
                {
                    StartAnimation(_currentState.NextState);
                    return;
                }
                else
                {
                    _animationIndex = 0;
                }
            }
            UpdateFrame();
        }

        void UpdateFrame()
        {
            if (!_isActive)
            {
                Log.DebugAssert("SmaAnimationControllerCore: Attempted to update frame while inactive.");
                return;
            }
            SetBindedPosition();
            NextSmaImage();
            _isPaused = false;
        }

        void SetBindedPosition()
        {
            foreach (var property in _currentState.FrameProperties[_animationIndex].BindProperties)
            {
                property.Binded.position = property.Target.position;
                property.Binded.rotation = property.Target.rotation;
            }
        }

        void NextSmaImage()
        {
            _smaImage.Initialize(_currentState.FrameProperties[_animationIndex].Sprites);
        }

        public override void SetTrigger(string stateName)
        {
            // Transition
            var transitionState = _animationStates.Find(x => (x.NextState != null && x.NextState.StateName == stateName) && (x.PrevState != null && x.PrevState == _currentState));
            if (transitionState != null)
            {
                StartAnimation(transitionState);
                return;
            }

            var targetState = _animationStates.Find(x => x.StateName == stateName);
            if (targetState != null)
            {
                StartAnimation(targetState);
                return;
            }
            else
            {
                Log.DebugAssert($"State {stateName} not found in animation states.");
            }

        }

        public override void Pause()
        {
            _isPaused = true;
        }

        public override void Resume()
        {
            _isPaused = false;
        }

        public override void SetActive(bool isActive)
        {
            Log.DebugLog("SmaAnimationControllerCore: SetActive:" + isActive + ", " + this.name);
            _isActive = isActive;
            _smaImage.SetActive(isActive);
        }

        public override bool IsTransitioning()
        {
            return _currentState.NextState != null;
        }

        public override bool IsAnimationEnd()
        {
            return _currentState.FrameProperties.Count == 0;
        }


    }
}