using UnityEngine;
using UnityEngine.UI;
using MyUnity.CommonUtilities;
using System.Collections;

[RequireComponent(typeof(ConnectionStatus))]
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

	bool win;

	void Start(){
		if(PlayerPrefs.HasKey("nTicket")){
			usrNameInput.text = PlayerPrefs.GetString("usrName"); 
			emailInput.text = PlayerPrefs.GetString("email"); 
			phoneInput.text = PlayerPrefs.GetString("phone"); 
		}
	}

	public void SaveData(){
		nTicket = nTicketInput.text;
		usrName = usrNameInput.text;
		email = emailInput.text;	
		phone = phoneInput.text;
		store = storeInput.text;
		
		PlayerPrefs.SetString("usrName",usrNameInput.text);
		PlayerPrefs.SetString("email",emailInput.text);
		PlayerPrefs.SetString("phone",phoneInput.text);

		if(ValidateInputs())
			StartCoroutine(IsAWinningTicket());
		else
			InputMessages();
			//print ("error");
	}

	void InputMessages(){
		if(nTicketInput.text.Equals(""))
			nTicketInput.placeholder.GetComponent<Text>().text = "Completa los datos";

		if(usrNameInput.text.Equals(""))
			usrNameInput.placeholder.GetComponent<Text>().text = "Completa los datos";

		if(emailInput.text.Equals(""))
			emailInput.placeholder.GetComponent<Text>().text = "Completa los datos";

		if(phoneInput.text.Equals(""))
			nTicketInput.placeholder.GetComponent<Text>().text = "Completa los datos";

		if(storeInput.text.Equals(""))
			storeInput.placeholder.GetComponent<Text>().text = "Completa los datos";
	}

	bool ValidateInputs(){
		if(nTicket.Length == 0 || usrName.Length==0 || email.Length==0 || phone.Length==0 || store.Length==0)
			return false;
		if(nTicket.Length > 0 && (int.Parse(nTicket) > 100 || int.Parse(nTicket) < 0 ))
			return false;

		return true;
	}

	IEnumerator IsAWinningTicket(){
		if(ConnectionStatus.Instance.connected){
			string phpInstruction = "http://seven-eleven.herokuapp.com/numbers/register_winner.json?number="+nTicket;

			yield return StartCoroutine(DataBaseManager.GetStringFromPHP(phpInstruction));

			WWW phpResult = DataBaseManager.result;

			if (phpResult == null){
				print ("No conection");
				ScreenController.Instance.Disconnected();
			}
			else{
				win = CheckStringValue(phpResult.text);//Random.value < 0.5f ? true : false;

				if(win)
					ScreenController.Instance.Continue();
				else
					ScreenController.Instance.Fail();
			}
		}
		else
			ScreenController.Instance.Fail();
	}


	bool CheckStringValue(string result){
		if(result.Equals("{\"retry\":true}"))
			return false;
		if(result.Contains("true"))
			return true;
		return false;
	}
}
