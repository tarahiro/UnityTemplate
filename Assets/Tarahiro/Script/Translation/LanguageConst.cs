using Cysharp.Threading.Tasks;
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
    public static class LanguageConst
    {
        public const int AvailableLanguageNumber = 2;

        [System.Serializable]
        public enum AvailableLanguage
        {
            Japanese,
            English,
        }
    }
}