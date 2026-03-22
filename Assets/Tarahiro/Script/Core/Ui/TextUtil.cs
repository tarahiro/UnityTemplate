using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Tarahiro.Ui
{
    public static class TextUtil
    {
        public const float c_defaultTextIntervalTime = .1f;
        public static IDisplayableTextByCharacter CreateDisplayableHundle(string text, TextMeshProUGUI textMeshProUGUI, string SeLabel, bool isSeRun = true, float textIntervalTime = c_defaultTextIntervalTime)
        {
            return new DisplayableTextByCharacter(text, textMeshProUGUI, SeLabel, isSeRun, textIntervalTime);
        }

    }
}