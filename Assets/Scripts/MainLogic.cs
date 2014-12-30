using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;

public class MainLogic : Singleton<MainLogic> {

	public string emailCredentials = "";
	public string passwordCredential = "";

	public GameObject loading;

	void Start(){
		bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		if (!focusModeSet) 
			Debug.Log("Failed to set focus mode (unsupported mode).");
	}

	// Use this for initialization
	private void SetEmailData () {
		EmailSender.SetCredentials(emailCredentials,passwordCredential);
		string from = "contacto@gatoradepromo.com";
		string to = "gatoradepromonfl2015@gmail.com,contacto@gatoradepromo.com";
		string subject = "Solicitante n. " + UserData.Instance.participation;
		string body = "Datos del ganador de $" + UserData.Instance.amount + ".00\n"
				+ "\nUsuario :"+ PlayerPrefs.GetString("usrName") 
				+ "\nCorreo :"+ PlayerPrefs.GetString("email")
				+ "\nN. de Ticket :"+ PlayerPrefs.GetString("nTicket")
				+ "\nTelefono :"+ PlayerPrefs.GetString("phone")
				+ "\nTienda :"+ PlayerPrefs.GetString("store");

		EmailSender.SetEmailBody(from,to,subject,body);
	}



	public void TakeShot(){	
		ScreenCapture.TakeScreenShot("Screenshot.jpg");	
		Invoke("WaitPhoto",1f);
	}

	void WaitPhoto(){
		PreviewPhoto.Instance.ShowPreviewPhoto();
		ScreenController.Instance.Continue();
	}

	public void SendEmail(){
		loading.SetActive(true);
		Invoke("wait",0.5f);

	}

	void wait(){	
		SetEmailData();
		StartCoroutine (EmailSender.SendEmail(Application.persistentDataPath+"/"+ScreenCapture._FileName));
		ScreenController.Instance.Continue();
	}


}

