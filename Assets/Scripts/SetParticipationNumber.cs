using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetParticipationNumber : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		if (!UserData.Instance.participation.Equals(""))
			GetComponent<Text>().text = "TU NÚMERO DE PARTICIÓN FUE: " + UserData.Instance.participation;
		else
			GetComponent<Text>().text = "TICKET REPETIDO";
	} 

}
