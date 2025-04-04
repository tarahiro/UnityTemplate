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
    public class AllImporter
    {
        public static void AllImport()
        {
            SeImporter.Import();
            LanguageMessageImporter.Import();
        }
    }
#endif
}
