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
    public enum BlendMode
    {
        通常, //0番
        乗算, //1番
        加算, //2番(以下略)
        除算,
        スクリーン,
        オーバーレイ,
        ハードライト,
        ソフトライト,
        覆い焼きカラー,
        焼き込みリニア,
        差の絶対値,
        比較暗,
        比較明,
        色相,
        彩度,
        カラー,
        輝度
    }
}