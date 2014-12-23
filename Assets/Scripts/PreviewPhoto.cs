using UnityEngine;
using UnityEngine.UI;
using MyUnity.CommonUtilities;
using System.Collections;
using System.IO;

public sealed class PreviewPhoto : Singleton<PreviewPhoto> {

	public RawImage image;

	public void ShowPreviewPhoto(){
		image.texture = null;
		LookforFile();
	}

	bool SearchFile(string name, string format){
		DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);	
		foreach(FileInfo file in dir.GetFiles(format))
			if(file.Name  == name)
				return true;
		return false;
	}

	void LookforFile(){
		if(SearchFile("Screenshot.jpg","*.jpg")){
			StartCoroutine(LookForPhoto());
		}
		else {		
			LookforFile();
		}
	}

	IEnumerator LookForPhoto(){
		WWW www;
		string url = "file://"+Application.persistentDataPath+"/"+ScreenCapture._FileName;

		yield return www = new WWW(url);
		if(www.error != null)
			StartCoroutine(LookForPhoto());
		float ratio = (float)www.texture.height / (float)www.texture.width;
		image.GetComponent<RectTransform>().sizeDelta = new Vector2(3f*www.texture.width/4f,3f*www.texture.width/4f*ratio);
		image.texture = www.texture;

	}
}
