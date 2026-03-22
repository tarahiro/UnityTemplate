using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Tarahiro;
using Tarahiro.MasterData;
using Tarahiro.Editor.XmlImporter;
using System.Collections.Generic;
using UnityEditor;
using gaw241201;
using gaw241201.Model;
using gaw241201.Model.MasterData;
using Tarahiro.Editor;

namespace gaw241201.Editor
{
#if UNITY_EDITOR
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd]とか）
    //アセンブリ構成に応じて、using部分を追加（gaw[yymmdd].model,gaw[yymmdd].Model.MasterData 等 ）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加

    [InitializeOnLoad]
    internal sealed class TemplateImporter
    {
        enum Columns
        {
            Index = 0,
            Id = 1,
            Description = 2,
        }

        static TemplateImporter()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingEditMode)
            {
                return;
            }

            ForceImportIfXmlIsNewer();
        }

        static void ForceImportIfXmlIsNewer()
        {
            string xmlPath = EditorUtil.XmlPath(TemplateMasterData.c_DataName, TemplateMasterData.c_DataName);
            string assetPath = "Assets/Resources/" + MasterDataConst.DataPath + TemplateMasterData.c_DataName + ".asset";

            if (!File.Exists(xmlPath))
            {
                return;
            }

            DateTime xmlTimeUtc = File.GetLastWriteTimeUtc(xmlPath);
            DateTime assetTimeUtc = File.Exists(assetPath) ? File.GetLastWriteTimeUtc(assetPath) : DateTime.MinValue;

            if (xmlTimeUtc <= assetTimeUtc)
            {
                return;
            }

            Import();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        //--------------------------------------------------------------------
        // 読み込み
        //--------------------------------------------------------------------

        [MenuItem("Assets/Tables/Import Template", false, 2)]
        static void ImportMenuTemplate()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            Import();

            stopwatch.Stop();
            Log.DebugLog($"Template imported in {stopwatch.ElapsedMilliseconds / 1000.0f} seconds.");
        }

        public static void Import()
        {
            var book = XmlImporter.ImportWorkbook(EditorUtil.XmlPath(TemplateMasterData.c_DataName, TemplateMasterData.c_DataName));

            var TemplateDataList = new List<TemplateMasterData.Record>();

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
                        TemplateDataList.Add(new TemplateMasterData.Record(index, id)
                        {
                            SettableDescription = sheet[row, (int)Columns.Description].String,
                        });
                    }
                }
            }

            // データ出力
            XmlImporter.ExportOrderedDictionary<TemplateMasterData, TemplateMasterData.Record, IMasterDataRecord<ITemplateMaster>>(MasterDataConst.DataPath + TemplateMasterData.c_DataName, TemplateDataList);
        }
    }
#endif
}