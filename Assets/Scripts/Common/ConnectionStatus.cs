using UnityEngine;
using System.Collections;

namespace MyUnity.CommonUtilities{
	public class ConnectionStatus : Singleton<ConnectionStatus>{
		[HideInInspector()]
		public bool connected = false;
		[Tooltip("Aprox. times per second")]
		public float frequency = 5;

		void Awake(){
			hideFlags =  HideFlags.HideInInspector;
			StartCoroutine("CheckInternetConection");
		}

		IEnumerator CheckInternetConection () {
			if(Application.internetReachability != NetworkReachability.NotReachable){
				WWW www;
				yield return www = new WWW("https://google.com");
				if(www.error != null)
					connected = false;
				else
					connected = true;
				yield return new WaitForSeconds(1/frequency);
				www.Dispose();
				StartCoroutine(CheckInternetConection ());
			}
			else {
				connected = false;
				yield break;
			}
		}
	}
}