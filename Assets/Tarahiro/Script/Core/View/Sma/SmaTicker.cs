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
    public class SmaTicker : ITickable
    {

        const float c_time = 8f/60f;
        [Inject] SmaPublisher _smaPublisher;

        //c_time��1��ASmaPublisher��Publish����
        float _time = 0f;
        public void Tick()
        {
            _time += Time.deltaTime;
            if (_time >= c_time)
            {
                _smaPublisher.Publish();
                _time = 0f;
            }
        }
    }
}