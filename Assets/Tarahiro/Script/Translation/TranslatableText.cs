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

       static TranslatableText _dummyText = null;
        const string c_dummyString = "***";
        public static TranslatableText GetDummyText()
        {

            if(_dummyText == null)
            {
                var list = new List<string>();
                for (int i = 0; i < LanguageConst.AvailableLanguageNumber; i++)
                {
                    list.Add(c_dummyString);
                }
                _dummyText = new TranslatableText(list.ToArray());
            }

            return _dummyText;

        }
    }
}
