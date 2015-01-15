using UnityEngine;
using MyUnity.CommonUtilities;
using System.Collections;

public class MainLogic : Singleton<MainLogic> {
	
	public GameObject loading;

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
		StartCoroutine(sendingEmail());
		ScreenController.Instance.Continue();
	}
	
	IEnumerator sendingEmail(){
		WWWForm postForm = new WWWForm();
		WWW image;
		yield return image = new WWW("file://"+Application.dataPath+"/"+ScreenCapture._FileName);
		byte[] bytes = image.texture.EncodeToJPG();
		postForm.AddField("number",UserData.Instance.participation);
		postForm.AddField("award",UserData.Instance.amount.ToString());
		postForm.AddField("name",PlayerPrefs.GetString("usrName"));
		postForm.AddField("email",PlayerPrefs.GetString("email"));
		postForm.AddField("ticket",PlayerPrefs.GetString("nTicket"));
		postForm.AddField("phone",PlayerPrefs.GetString("phone"));
		postForm.AddField("store",PlayerPrefs.GetString("store"));
		postForm.AddBinaryData("screenshot",bytes,"screenshot.jpg","image/jpeg");

		WWW upload = new WWW("http://www.rckdrgo.sodvi.com/setEmailInfo.php",postForm);        
		yield return upload;

		if (upload.error == null)
			Debug.Log("upload done :" + upload.text);
		else
			Debug.Log("Error during upload: " + upload.error);
	}

}

