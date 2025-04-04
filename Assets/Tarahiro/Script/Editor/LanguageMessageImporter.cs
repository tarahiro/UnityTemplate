using System.Collections;
using UnityEngine;
using Tarahiro;
using Tarahiro.MasterData;
using Tarahiro.Editor.XmlImporter;
using System.Collections.Generic;
using UnityEditor;
using Tarahiro.Editor;

namespace Tarahiro.Editor
{
#if UNITY_EDITOR
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd]とか）
    //アセンブリ構成に応じて、using部分を追加（gaw[yymmdd].model,gaw[yymmdd].Model.MasterData 等 ）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加
    internal sealed class LanguageMessageImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            MessageStart = 2,
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import LanguageMessage", false, 2)]
        static void ImportMenuLanguageMessage()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"LanguageMessage imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.XmlImporter.ImportWorkbook(EditorUtil.XmlPath(LanguageMessageMasterData.c_DataName, LanguageMessageMasterData.c_DataName));

            var LanguageMessageDataList = new List<LanguageMessageMasterData.Record>();

            var sheet = book.TryGetWorksheet(EditorConst.c_SheetName);
            if (sheet == null)
            {
                Log.DebugWarning($"シート: {EditorConst.c_SheetName} が見つかりませんでした。");
            }
            else
            {
                for (int row = 0; row < sheet.Height; ++row)
                {
                    // Indexの欄が有効な数字だったら読み込み
                    if (int.TryParse(sheet[row, (int)Columns.Index].String, out int index))
                    {
                        string id = sheet[row, (int)Columns.Id].String;
                        LanguageMessageDataList.Add(new LanguageMessageMasterData.Record(index, id)
                        {
                            SettableMessage = EditorUtil.GetTranslatableText<LanguageConst.AvailableLanguage>(sheet,row,(int)Columns.MessageStart)
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.XmlImporter.ExportOrderedDictionary<LanguageMessageMasterData, LanguageMessageMasterData.Record, IMasterDataRecord<ILanguageMessageMaster>>(MasterDataConst.DataPath + LanguageMessageMasterData.c_DataName, LanguageMessageDataList);
        }
    }
#endif
}