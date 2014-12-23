using UnityEngine;
using System.IO;
using System.Collections;

namespace MyUnity.CommonUtilities{
	public sealed class ScreenCapture{
		private static string _path = "";
		
		public static string _Path {
			get {
				return _path;
			}
		}
		private static string _fileName = "";
		public static string _FileName {
			get {
				return _fileName;
			}
		}

		public static void TakeScreenShot(string fileName){
			
			_fileName = fileName;
			#if UNITY_EDITOR
			DirectoryInfo dir = new DirectoryInfo(Application.dataPath);	
			#else
			DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);	
			#endif

			foreach(FileInfo file in dir.GetFiles("*.jpg"))
				if(file.Name  == _fileName)
					File.Delete(file.FullName);

			#if UNITY_EDITOR
			_path =  Application.dataPath;
			Application.CaptureScreenshot("Assets/"+_fileName);
			#else
			_path =  Application.persistentDataPath;
			Application.CaptureScreenshot(_fileName);
			#endif
		}
	}
}
