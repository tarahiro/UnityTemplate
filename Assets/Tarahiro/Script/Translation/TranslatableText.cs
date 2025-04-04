using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;

namespace Tarahiro
{
    [System.Serializable]
    public class TranslatableText : ITranslatableText
    {
        [SerializeField] List<string> translatableTextList;

        public string GetTranslatedText(int languageIndex)
        {
            Log.DebugAssert(languageIndex < translatableTextList.Count);
            return translatableTextList[languageIndex];
        }

        public TranslatableText(params string[] text)
        {
            translatableTextList = new List<string>();

            for (int i = 0; i < text.Length; i++)
            {
                translatableTextList.Add(text[i]);
            }
        }
    }
}
