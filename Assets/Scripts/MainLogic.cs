using UnityEngine;
using System.IO;
using MyUnity.CommonUtilities;
using System.Collections;

public class MainLogic : Singleton<MainLogic> {

	public string emailCredentials = "";
	public string passwordCredential = "";

	public string fileName = "";
	//public GameObject loadingCircle;
	bool lookingForFile;

	// Use this for initialization
	private void Start () {
#if !UNITY_EDITOR
		bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		if (!focusModeSet) 
			Debug.Log("Failed to set focus mode (unsupported mode).");
#endif
		EmailSender.SetCredentials(emailCredentials,passwordCredential);
		string from = "prueba@prueba.com";
		string to = "rckdrigomed@gmail.com";
		string subject = "Prueba Unity " + Application.platform;
		string body = "Este es un correo de prueba. "
				+ "\nUsuario :"+ PlayerPrefs.GetString("usrName") 
				+ "\nCorreo :"+ PlayerPrefs.GetString("email")
				+ "\nN. de Ticket :"+ PlayerPrefs.GetString("nTicket")
				+ "\nTelefono :"+ PlayerPrefs.GetString("phone")
				+ "\nTienda :"+ PlayerPrefs.GetString("store");

		EmailSender.SetEmailBody(from,to,subject,body);
	}

	bool SearchFile(){
		DirectoryInfo dir = new DirectoryInfo(ScreenCapture._Path);
		foreach(FileInfo file in dir.GetFiles("*.jpg")){
			if(file.Name.Equals(ScreenCapture._FileName))
			   return true;
		}
		return false;
	}

	IEnumerator LookforFile(){
		bool fileExists;
		yield return new WaitForSeconds(1);
		yield return (fileExists = SearchFile());
		if(fileExists){
			ScreenController.Instance.Continue();
			PreviewPhoto.Instance.ShowPreviewPhoto();
			yield break;
		}
		else {		
			ScreenCapture.TakeScreenShot(fileName.Equals("")? "Screenshot.jpg":fileName);	
			StartCoroutine(LookforFile());
		}

	}

	public void TakeShot(){	
		if(!lookingForFile){
			lookingForFile = true;
			ScreenCapture.TakeScreenShot(fileName.Equals("")? "Screenshot.jpg":fileName);		
			StartCoroutine(LookforFile());
		}
	}

	public void SendEmail(){
		//loadingCircle.SetActive(true);
		StartCoroutine (EmailSender.SendEmail(ScreenCapture.GetLastScreenshotPath()));
		ScreenController.Instance.Continue();
	}

	public void CamReset(){
		lookingForFile = false;
	}

	public void Update(){
		print (ConnectionStatus.Instance.connected);
	}
}

