using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using LitMotion;
using LitMotion.Extensions;
using System.Threading;
using static UnityEngine.GraphicsBuffer;

namespace Tarahiro.View
{
    public class CameraShaker : MonoBehaviour
    {
        [Inject] ICancellationTokenPure _cancellationToken;


        Vector3 _initialPosition;
        Vector3 _direction = new Vector3(1f, 0.5f, 0f);

        private void Start()
        {
            _initialPosition = transform.position;
            _direction.Normalize();
        }

        public enum ShakeStrength
        {
            Weak,
            Strong,
        }

        public void ShakeOneShot(ShakeStrength strength)
        {
            if (_isShaking)
            {
                _cancellationToken.Cancel();
            }
            _cancellationToken.SetNew();
            ShakeOneShotAsync(strength, _cancellationToken.Token).Forget();

        }

        public void ShakeContinuousStart(ShakeStrength strength)
        {
            if (_isShaking)
            {
                _cancellationToken.Cancel();
            }
            _cancellationToken.SetNew();
            ShakeContinuousAsync(strength, _cancellationToken.Token).Forget();

        }

        public void ShakeContinuousStop()
        {
            if (_isShaking)
            {
                _cancellationToken.Cancel();
            }
        }

        bool _isShaking = false;

        async UniTask ShakeOneShotAsync(ShakeStrength weak, CancellationToken ct)
        {

            _isShaking = true;
            await LMotion.Create(_initialPosition, _initialPosition + _direction * GetShakeStrength(weak), GetShakeDuration(weak)/4)
                .WithEase(Ease.OutElastic)
                .BindToPosition(transform)
                .ToUniTask(ct);
            await LMotion.Create(_initialPosition + _direction * GetShakeStrength(weak), _initialPosition - _direction * GetShakeStrength(weak), GetShakeDuration(weak) / 2)
                .WithEase(Ease.OutElastic)
                .BindToPosition(transform)
                .ToUniTask(ct);
            await LMotion.Create(_initialPosition - _direction * GetShakeStrength(weak), _initialPosition, GetShakeDuration(weak) / 4)
                .WithEase(Ease.OutElastic)
                .BindToPosition(transform)
                .ToUniTask(ct);
            _isShaking = false;
        }

        async UniTask ShakeContinuousAsync(ShakeStrength weak, CancellationToken ct)
        {
            _isShaking = true;
            ct.Register(() => LMotion.Create(transform.position, _initialPosition, GetShakeDuration(weak))
                .BindToPosition(transform)
                .ToUniTask().Forget());
            await LMotion.Create(_initialPosition, _initialPosition + _direction * GetShakeStrength(weak), GetShakeDuration(weak) / 4)
                .WithEase(Ease.OutElastic)
                .BindToPosition(transform)
                .ToUniTask(ct);
            await LMotion.Create(_initialPosition + _direction * GetShakeStrength(weak), _initialPosition - _direction * GetShakeStrength(weak), GetShakeDuration(weak) / 4)
                .WithEase(Ease.OutElastic)
                .WithLoops(-1, LoopType.Yoyo)
                .BindToPosition(transform)
                .ToUniTask(ct);
            _isShaking = false;

        }

        float GetShakeStrength(ShakeStrength shakeStrength)
        {
            switch (shakeStrength)
            {
                case ShakeStrength.Weak:
                    return 0.05f;
                case ShakeStrength.Strong:
                    return 0.1f;
                default:
                    Log.DebugAssert(false, "���Ή���ShakeStrength�ł�");
                    return 0;
            }
        }

        float GetShakeDuration(ShakeStrength shakeStrength)
        {
            switch (shakeStrength)
            {
                case ShakeStrength.Weak:
                    return 0.1f;
                case ShakeStrength.Strong:
                    return 0.1f;
                default:
                    Log.DebugAssert(false, "���Ή���ShakeStrength�ł�");
                    return 0;
            }
        }


    }
}