using System.Linq;
using System.Collections.Generic;

namespace Tarahiro.Editor
{
	public static class EditorConst
	{
		/// <summary>
		/// Tarahiro拡張メニューのPriority定義
		/// </summary>
		public enum CategoryMenuItemPriority
		{
			Symbols = 0,
			Prefabs = 100,
			Serialize = 200,
			ReferenceChecker = 300,
            Directory = 400,
		}

		public const string c_XmlPathPrefix = "ImportData/";
        public const string c_XmlPathSuffix= ".xml";
        public const string c_SheetName = "Script";
        
		
		readonly static List<string> c_correctString = new List<string>()
		{
			"○",
			"〇",
			"O",
			"o",
			"O",
		};

		public static bool IsCorrect(string ns)
		{
			return c_correctString.Contains(ns);
        }

		static DemoBehaviour _demoBehaviour = null;

		public static bool IsDemo()
		{
			if(_demoBehaviour == null)
			{
                _demoBehaviour = UtilResource.GetResource<DemoBehaviour>("Editor/DemoBehaviour");
            }

			return _demoBehaviour.IsDemo;
        }
    }
}
