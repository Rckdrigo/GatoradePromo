using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextConfirmation : MonoBehaviour {
	
	// Use this for initialization
	void OnEnable () {
		string text = "Confirma tus datos:\n\n" 
			+ PlayerPrefs.GetString("usrName") + "\n"
			+ PlayerPrefs.GetString("email") + "\n"
			+ PlayerPrefs.GetString("phone") + "\n";
		GetComponent<Text>().text = text;

	}

}
