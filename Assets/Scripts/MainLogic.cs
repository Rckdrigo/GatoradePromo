using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;

public class MainLogic : Singleton<MainLogic> {

	public string emailCredentials = "";
	public string passwordCredential = "";

	public GameObject loading;

	void Start(){
		//bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		//if (!focusModeSet) 
		//	Debug.Log("Failed to set focus mode (unsupported mode).");
	}

	// Use this for initialization
	private void SetEmailData () {
		EmailSenderWP8.SetCredentials(emailCredentials,passwordCredential);
		string from = "contacto@gatoradepromo.com";
		string to = "rckdrigomed@gmail.com";
		string subject = "Solicitante n. " + UserData.Instance.participation;
		string body = "Datos del ganador de $" + UserData.Instance.amount + ".00\n"
				+ "\nUsuario :"+ PlayerPrefs.GetString("usrName") 
				+ "\nCorreo :"+ PlayerPrefs.GetString("email")
				+ "\nN. de Ticket :"+ PlayerPrefs.GetString("nTicket")
				+ "\nTelefono :"+ PlayerPrefs.GetString("phone")
				+ "\nTienda :"+ PlayerPrefs.GetString("store");

		EmailSenderWP8.SetEmailBody(from,to,subject,body);
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
		EmailSenderWP8.sendEmail();//Application.persistentDataPath+"/"+ScreenCapture._FileName);
		ScreenController.Instance.Continue();
	}


}

