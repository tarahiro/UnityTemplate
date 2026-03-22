using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class LanguageCodeGettable : ILanguageCodeGettable
    {
        public string Get(LanguageConst.AvailableLanguage language)
        {
            switch (language)
            {
                case LanguageConst.AvailableLanguage.Japanese:
                    return "ja";
                case LanguageConst.AvailableLanguage.English:
                    return "en";
                case LanguageConst.AvailableLanguage.SimplifiedChinese:
                    return "zh-CN";
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }
    }
}