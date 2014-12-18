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

	float amount;

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

		PlayerPrefs.SetString("nTicket",nTicketInput.text);
		PlayerPrefs.SetString("store",storeInput.text);
	
		PlayerPrefs.SetString("usrName",usrNameInput.text);
		PlayerPrefs.SetString("email",emailInput.text);
		PlayerPrefs.SetString("phone",phoneInput.text);

		if(ValidateInputs())
			
//#if UNITY_EDITOR
			//ScreenController.Instance.Continue();
//#else
		StartCoroutine(IsAWinningTicket());
//#endif
		else{
#if !UNITY_EDITOR
			Handheld.Vibrate();
#endif
			InputMessages();
		}
	}

	void InputMessages(){
		if(nTicketInput.text.Equals("")){
			nTicketInput.placeholder.GetComponent<Text>().text = "No. de ticket";
			nTicketInput.placeholder.GetComponent<Text>().color = Color.red;
		}

		if(usrNameInput.text.Equals("")){
			usrNameInput.placeholder.GetComponent<Text>().text = "Nombre";
			usrNameInput.placeholder.GetComponent<Text>().color = Color.red;
		}

		if(emailInput.text.Equals("")){
			emailInput.placeholder.GetComponent<Text>().text = "Email";
			emailInput.placeholder.GetComponent<Text>().color = Color.red;
		}

		if(phoneInput.text.Equals("")){
			phoneInput.placeholder.GetComponent<Text>().text = "Telefono";
			phoneInput.placeholder.GetComponent<Text>().color = Color.red;
		}

		if(storeInput.text.Equals("")){
			storeInput.placeholder.GetComponent<Text>().text = "Tienda";
			storeInput.placeholder.GetComponent<Text>().color = Color.red;
		}
	}

	bool ValidateInputs(){
		if(nTicket.Length == 0 || usrName.Length==0 || email.Length==0 || phone.Length==0 || store.Length==0)
			return false;
		if(nTicket.Length > 0 && (int.Parse(nTicket) > 8500 || int.Parse(nTicket) < 0 ))
			return false;

		return true;
	}

	IEnumerator IsAWinningTicket(){
		if(ConnectionStatus.Instance.connected){
			string phpInstruction = "http://seven-eleven.herokuapp.com/users/submit_with_address.json?ticket="+nTicket;

			yield return StartCoroutine(DataBaseManager.GetStringFromPHP(phpInstruction));

			WWW phpResult = DataBaseManager.result;
			win = CheckStringValue(phpResult.text);

			if(win)
				ScreenController.Instance.Continue();
			else
				ScreenController.Instance.Fail();

		}
		else
			ScreenController.Instance.Fail();
	}


	bool CheckStringValue(string result){
		if(result.Contains("Ticket duplicado"))
			return false;
		if(result.Contains("true")){
			amount = float.Parse(result.Split(':')[3].Replace("}",""));
			ScreenController.Instance.SetAmount(amount);
			return true;
		}
		return false;
	}
}
