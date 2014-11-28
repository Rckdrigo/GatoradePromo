using UnityEngine;
using System.IO;
using MyUnity.CommonUtilities;
using System.Collections;

public class MainLogic : Singleton<MainLogic> {

	public string emailCredentials = "";
	public string passwordCredential = "";

	public string fileName = "";

	bool lookingForFile;

	// Use this for initialization
	private void Start () {
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
			print (file.FullName);
			if(file.Name.Equals(ScreenCapture._FileName))
			   return true;
		}
		return false;
	}

	IEnumerator TakeScreenShot(){
		bool fileExists;
		ScreenCapture.TakeScreenShot(fileName.Equals("")? "Screenshot.jpg":fileName);
		yield return (fileExists = SearchFile());
		yield return new WaitForSeconds(1);
		if(fileExists)
			ScreenController.Instance.Continue();
		else
			StartCoroutine(TakeScreenShot());
	}

	public void TakeShot(){
		if(!lookingForFile){
			lookingForFile = true;
			StartCoroutine(TakeScreenShot());
		}
	}

	public void SendEmail(){
		StartCoroutine (EmailSender.SendEmail(ScreenCapture.GetLastScreenshotPath()));
		ScreenController.Instance.Continue();
	}
}

