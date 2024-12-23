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
	}
}
