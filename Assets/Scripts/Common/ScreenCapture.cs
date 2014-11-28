﻿using UnityEngine;
using System.Collections;

namespace MyUnity.CommonUtilities{
	public sealed class ScreenCapture{

		private static string _fileName = "";

		public static string _FileName {
			get {
				return _fileName;
			}
		}

		private static string _path = "";

		public static string _Path {
			get {
				return _path;
			}
		}

		public static string GetLastScreenshotPath(){
			return _path+"/"+_fileName;
		}

		public static void TakeScreenShot(string path,string fileName){
			_fileName = fileName;
			_path = path;
			Application.CaptureScreenshot(_path+"/"+_fileName);
		}

		public static void TakeScreenShot(string fileName){
			#if UNITY_EDITOR
			_path =  Application.dataPath;
			#else
			_path =  Application.persistentDataPath;
			#endif
			_fileName = fileName;
			Application.CaptureScreenshot(_path+"/"+_fileName);

		}
	}
}