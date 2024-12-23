using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using Tarahiro.Editor.XmlImporter;

namespace Tarahiro.Editor
{
    public static class EditorUtil
    {
        public static string XmlPath(string directoryName, string fileName)
        {
            return EditorConst.c_XmlPathPrefix + directoryName + "/" + fileName + EditorConst.c_XmlPathSuffix;
        }

        public static string[] GetStringArray(TableCell cell)
        {
            if (cell.IsEmpty)
            {
                return new string[0];
            }
            else
            {
                return cell.String.Split(',');
            }
        }

        public static string[] GetStringArrayFromCells(IWorksheet sheet, int row, int startColumn, int columnInterval)
        {
            List<string> returnable = new List<string>();

            for(int i = 0; startColumn + i * columnInterval < sheet.Width && !sheet[row,startColumn + i * columnInterval].IsEmpty; i++)
            {
                returnable.Add(sheet[row, startColumn + i * columnInterval].String);
            }

            return returnable.ToArray();
        }


        [System.Serializable]
        public class ListWrapper<T>
        {
            public List<T> List;
        }
    }
}