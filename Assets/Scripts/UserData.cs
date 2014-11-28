using UnityEngine;
using UnityEngine.UI;
using MyUnity.CommonUtilities;
using System.Collections;

public sealed class UserData : MonoBehaviour {

	public InputField nTicketInput; 
	public InputField usrNameInput; 
	public InputField emailInput; 
	public InputField phoneInput; 
	public InputField storeInput; 

	[HideInInspector()]
	public string nTicket;
	[HideInInspector()]
	public string usrName;
	[HideInInspector()]
	public string email;
	[HideInInspector()]
	public string phone;
	[HideInInspector()]
	public string store;


	void Start(){
		if(PlayerPrefs.HasKey("nTicket")){
			nTicketInput.text = PlayerPrefs.GetString("nTicket"); 
			usrNameInput.text = PlayerPrefs.GetString("usrName"); 
			emailInput.text = PlayerPrefs.GetString("email"); 
			phoneInput.text = PlayerPrefs.GetString("phone"); 
			storeInput.text = PlayerPrefs.GetString("store"); 
		}
	}

	public void SaveData(){
		nTicket = nTicketInput.text;
		usrName = usrNameInput.text;
		email = emailInput.text;	
		phone = phoneInput.text;
		store = storeInput.text;

		PlayerPrefs.SetString("nTicket",nTicketInput.text);
		PlayerPrefs.SetString("usrName",usrNameInput.text);
		PlayerPrefs.SetString("email",emailInput.text);
		PlayerPrefs.SetString("phone",phoneInput.text);
		PlayerPrefs.SetString("store",storeInput.text);

		ScreenController.Instance.Continue();
	}

}
