// https://qiita.com/vc_kusuha/items/13a68474edfd78e41b82

using UnityEditor;
using UnityEngine;
using System.IO;

/// <summary>
/// Unityエディタ上からGameビューのスクリーンショットを撮るEditor拡張
/// </summary>
namespace Tarahiro.Editor
{
#if UNITY_EDITOR
	public class CaptureScreenshotFromEditor : UnityEditor.Editor
	{
		/// <summary>
		/// キャプチャを撮る
		/// </summary>
		/// <remarks>
		/// Edit > CaptureScreenshot に追加。
		/// HotKeyは Ctrl + Shift + F12。
		/// </remarks>
		[MenuItem("Tools/CaptureScreenshot #%F12")]
		private static void CaptureScreenshot()
		{
			// 現在時刻からファイル名を決定
			var directoryPath = "Recordings/";
			var filename = directoryPath + "screenshot" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";
			// ディレクトリがなければ作成
			if (!System.IO.Directory.Exists(directoryPath))
			{
				System.IO.Directory.CreateDirectory(directoryPath);
			}

			// キャプチャを撮る
			ScreenCapture.CaptureScreenshot(filename); // ← GameViewにフォーカスがない場合、この時点では撮られない
													   // GameViewを取得してくる
			var assembly = typeof(UnityEditor.EditorWindow).Assembly;
			var type = assembly.GetType("UnityEditor.GameView");
			var gameview = EditorWindow.GetWindow(type);
			// GameViewを再描画
			gameview.Repaint();

			Debug.Log("ScreenShot: " + filename);
		}
	}
#endif
}