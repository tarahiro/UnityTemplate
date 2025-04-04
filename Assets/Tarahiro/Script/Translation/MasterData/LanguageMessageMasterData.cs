using Tarahiro;
using Tarahiro.MasterData;
using System;
using System.Collections;
using UnityEngine;

namespace Tarahiro.MasterData
{
    //---プロジェクト作成時にやること---//
    //namespaceの"project"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加
    public class LanguageMessageMasterData : MasterDataOrderedDictionary<LanguageMessageMasterData.Record, IMasterDataRecord<ILanguageMessageMaster>>
    {
        public const string c_DataName = "LanguageMessage";

        [Serializable]
        public class Record : IMasterDataRecord<ILanguageMessageMaster>, ILanguageMessageMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] TranslatableText m_Message;


            public int Index => m_Index;
            public string Id => m_Id;
            public ITranslatableText Message=> m_Message;

            public ILanguageMessageMaster GetMaster() => this;

#if UNITY_EDITOR
            public TranslatableText SettableMessage { set => m_Message = value; }

#endif
        }
    }
}