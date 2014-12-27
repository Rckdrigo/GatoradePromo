using UnityEngine;
using UnityEngine.UI;
using MyUnity.CommonUtilities;
using System.Collections;

[RequireComponent(typeof(ConnectionStatus))]
public sealed class UserData : Singleton<UserData> {

	public InputField nTicketInput; 
	public InputField usrNameInput; 
	public InputField emailInput; 
	public InputField phoneInput; 
	public InputField storeInput; 

	[HideInInspector()]
	public float amount;
	[HideInInspector()]
	public string participation;

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

	void OnEnable(){
		participation = "";
		if(PlayerPrefs.HasKey("nTicket")){
			usrNameInput.text = PlayerPrefs.GetString("usrName"); 
			emailInput.text = PlayerPrefs.GetString("email"); 
			phoneInput.text = PlayerPrefs.GetString("phone"); 
		}
	}

	public void CheckIfWin(){
#if !UNITY_EDITOR
		StartCoroutine(IsAWinningTicket());
#else
		ScreenController.Instance.SetAmount(500);
		ScreenController.Instance.Continue();
#endif
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
			ScreenController.Instance.Continue();
		else{
			Handheld.Vibrate();
			InputMessages();
		}
	}

	void InputMessages(){
		if(nTicketInput.text.Length < 35){
			nTicketInput.text = "";
			if(nTicketInput.placeholder.GetComponent<Text>().text!="N. DE TICKET NO VÁLIDO")
				nTicketInput.placeholder.GetComponent<Text>().text = "INGRESA LOS 35 DIGITOS";
			nTicketInput.placeholder.GetComponent<Text>().color = Color.yellow;
		}
		
		if(!(nTicket.ToCharArray()[8] == '2' && nTicket.ToCharArray()[9] == '0' && nTicket.ToCharArray()[10] == '1' && nTicket.ToCharArray()[11] == '5')){
			nTicketInput.text = "";
			nTicketInput.placeholder.GetComponent<Text>().text = "N. DE TICKET NO VÁLIDO";
			nTicketInput.placeholder.GetComponent<Text>().color = Color.yellow;
		}
			
		if(usrNameInput.text.Equals("")){
			usrNameInput.placeholder.GetComponent<Text>().text = "NOMBRE";
			usrNameInput.placeholder.GetComponent<Text>().color = Color.yellow;
		}

		if(emailInput.text.Equals("")){
			emailInput.placeholder.GetComponent<Text>().text = "EMAIL";
			emailInput.placeholder.GetComponent<Text>().color = Color.yellow;
		}

		if(phoneInput.text.Length < 8){
			phoneInput.text = "";
			phoneInput.placeholder.GetComponent<Text>().text = "TELEFONO";
			phoneInput.placeholder.GetComponent<Text>().color = Color.yellow;
		}

		if(storeInput.text.Equals("")){
			storeInput.placeholder.GetComponent<Text>().text = "TIENDA";
			storeInput.placeholder.GetComponent<Text>().color = Color.yellow;
		}
	}

	bool ValidateInputs(){
		if(nTicket.Length < 35 || usrName.Length==0 || email.Length==0 || phone.Length==0 || store.Length==0)
			return false;
		if(!(nTicket.ToCharArray()[8] == '2' && nTicket.ToCharArray()[9] == '0' && nTicket.ToCharArray()[10] == '1' && nTicket.ToCharArray()[11] == '5'))
			return false;
			
		return true;
	}

	IEnumerator IsAWinningTicket(){
		if(ConnectionStatus.Instance.connected){

			string phpInstruction = "http://seven-eleven.herokuapp.com/users/submit_with_address.json?ticket="+nTicket
				+ "&email=" + email;
			 
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
		if(result.Contains("TICKET DUPLICADO"))
			return false;
		participation = result.Split(':')[1].Replace(",\"winner\"","");
		if(result.Contains("true")){
			amount = float.Parse(result.Split(':')[3].Replace("}",""));
			ScreenController.Instance.SetAmount(amount);
			return true;
		}		
		return false;
	}
}
