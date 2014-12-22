using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextConfirmation : MonoBehaviour {
	
	// Use this for initialization
	void OnEnable () {
		string text = "CONFIRMA TUS DATOS:\n\n NOMBRE : " + PlayerPrefs.GetString("usrName") 
			+ "\nEMAIL : " + PlayerPrefs.GetString("email")
				+ "\nTELEFONO : " + PlayerPrefs.GetString("phone")
				+ "\nTIENDA : " + PlayerPrefs.GetString("store") 
				+ "\nTICKET : " + PlayerPrefs.GetString("nTicket");
		GetComponent<Text>().text = text;

	}

}
