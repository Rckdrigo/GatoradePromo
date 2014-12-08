using UnityEngine;
using System.Collections;

namespace MyUnity.CommonUtilities{
	public static class DataBaseManager{
		public static WWW result;

		public static IEnumerator GetStringFromPHP (string instuction) {
			WWW www;
			yield return www = new WWW(instuction);
			result = www;
		}
	}
}