using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tarahiro
{
    public class ActiveLayerPublisher
    {
        [Inject] IPublisher<ActiveLayerConst.InputLayer> _publisher;

        List<ActiveLayerConst.InputLayer> _layer = new List<ActiveLayerConst.InputLayer>() { ActiveLayerConst.InputLayer.None };

        public void PublishActiveLayer(ActiveLayerConst.InputLayer layer)
        {
            Log.DebugLog("ActiveLayerPublisher: PublishActiveLayer: " + layer);
            if (_layer[_layer.Count - 1] >= layer)
            {
                Log.DebugAssert("����inputLayer�����̂��̈ȉ��ł��B ���F" + _layer[_layer.Count-1] + ",���F" + layer);
            }
            else
            {
                _layer.Add(layer);
                _publisher.Publish(layer);
            }
        }

        public void ResetActiveLayer()
        {
            if (_layer.Count > 0)
            {
                _layer.RemoveAt(_layer.Count - 1);
            }

            if (_layer.Count > 0)
            {
                _publisher.Publish(_layer[_layer.Count - 1]);
            }
            else
            {
                _layer.Add(ActiveLayerConst.InputLayer.None);
                _publisher.Publish(_layer[0]);
            }
        }
    }
}