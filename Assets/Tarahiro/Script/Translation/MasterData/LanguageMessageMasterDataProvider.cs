using System.Collections;
using UnityEngine;
using Tarahiro.MasterData;

namespace Tarahiro.MasterData
{
    //---プロジェクト作成時にやること---//
    //namespaceの"project"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    public class LanguageMessageMasterDataProvider : MasterDataProvider<LanguageMessageMasterData.Record, IMasterDataRecord<ILanguageMessageMaster>>, ILanguageMessageMasterDataProvider
    {
        public LanguageMessageMasterDataProvider() : base()
        {
            Load(LanguageMessageMasterData.c_DataName);
        }
    }
}